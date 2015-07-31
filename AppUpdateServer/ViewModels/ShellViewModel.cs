using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Models;
using AppUpdateServer.Properties;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdateServer.ViewModels
{
    internal sealed class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoAcceptor _acceptor;

        public ShellViewModel()
        {
            //获取事件聚合器  
            _aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                InitializeFromDatabase();

                //获取Socket
                _acceptor = ServiceLocator.Current.GetInstance<IoAcceptor>();
            }
        }

        private void InitializeFromDatabaseSync()
        {
            var sqlConStr = new SQLiteConnectionStringBuilder
            {
                DataSource = Settings.Default.Database,
                //Password = Settings.Default.DatabasePwd,
                DateTimeKind = DateTimeKind.Utc,
                ForeignKeys = true
            };

            var sqlCon = new SQLiteConnection(sqlConStr.ToString());
            sqlCon.Open();
            if (sqlCon.State != ConnectionState.Open)
            {
                return;
            }
            var sqlCmd = new SQLiteCommand("SELECT * FROM [AppSeriesTable] ORDER BY AppSeriesID ASC", sqlCon);
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                //AppSeries.BeginBulkOperation();
                while (sqlReader.Read())
                {
                    AppSeries.Add(new AppSeries
                    {
                        AppSeriesID = sqlReader.GetInt32(0),
                        AppSeriesName = sqlReader.GetString(1),
                        AppSeriesFriendlyDescription = sqlReader.GetString(2)
                    });
                }
                //AppSeries.EndBulkOperation();
            }
            sqlCmd = new SQLiteCommand("SELECT * FROM [AppBranchesTable] ORDER BY AppBranchID ASC", sqlCon);
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                AppSeries series = null;
                //AppBranches.BeginBulkOperation();
                while (sqlReader.Read())
                {
                    var seriesId = sqlReader.GetInt32(0);
                    if (series == null || series.AppSeriesID != seriesId)
                    {
                        series = AppSeries.FirstOrDefault(s => s.AppSeriesID == seriesId);
                    }
                    var branch = new AppBranch
                    {
                        AppSeries = series,
                        AppBranchID = sqlReader.GetInt32(1),
                        AppBranchName = sqlReader.GetString(2),
                        AppBranchFriendlyDescription = sqlReader.GetString(3)
                    };
                    AppBranches.Add(branch);
                    series?.ChildBranches.Add(branch);
                }
                //AppBranches.EndBulkOperation();
            }
            sqlCmd = new SQLiteCommand("SELECT * FROM [AppUpdateClientTable] ORDER BY AppBrancheID ASC", sqlCon);
            using (var sqlReader = sqlCmd.ExecuteReader())
            {
                AppBranch branch = null;
                //ClientInfos.BeginBulkOperation();
                while (sqlReader.Read())
                {
                    var branchId = sqlReader.GetInt32(2);
                    if (branch == null || branch.AppBranchID != branchId)
                    {
                        branch = AppBranches.FirstOrDefault(b => b.AppBranchID == branchId);
                    }

                    IPAddress ipAddress;
                    if (sqlReader.IsDBNull(3) || !IPAddress.TryParse(sqlReader.GetString(3), out ipAddress))
                    {
                        ipAddress = IPAddress.None;
                    }
                    var client = new ClientInfo
                    {
                        MachineID = sqlReader.GetString(0),
                        Company = sqlReader.GetString(1),
                        AppBranch = branch,
                        IPAddress = ipAddress,
                        RsaPrivateKey = sqlReader.GetString(4),
                        Expiration = sqlReader.GetDateTime(5),
                        Serial = sqlReader.GetString(6)
                    };
                    ClientInfos.Add(client);
                    branch?.ChildClients.Add(client);
                }
                //ClientInfos.BeginBulkOperation();
            }
            sqlCon.Close();
        }

        private async void InitializeFromDatabase()
        {
            var sqlConStr = new SQLiteConnectionStringBuilder
            {
                DataSource = Settings.Default.Database,
                //Password = Settings.Default.DatabasePwd,
                DateTimeKind = DateTimeKind.Utc,
                ForeignKeys = true
            };

            var sqlCon = new SQLiteConnection(sqlConStr.ToString());
            await sqlCon.OpenAsync().ContinueWith(t =>
            {
                if (!t.IsCompleted)
                {
                    return;
                }
                var sqlCmd = new SQLiteCommand("SELECT * FROM [AppSeriesTable] ORDER BY AppSeriesID ASC", sqlCon);
                using (var sqlReader = sqlCmd.ExecuteReader())
                {
                    AppSeries.BeginBulkOperation();
                    while (sqlReader.Read())
                    {
                        AppSeries.Add(new AppSeries
                        {
                            AppSeriesID = sqlReader.GetInt32(0),
                            AppSeriesName = sqlReader.GetString(1),
                            AppSeriesFriendlyDescription = sqlReader.GetString(2)
                        });
                    }
                    AppSeries.EndBulkOperation();
                }
                sqlCmd = new SQLiteCommand("SELECT * FROM [AppBranchesTable] ORDER BY AppBranchID ASC", sqlCon);
                using (var sqlReader = sqlCmd.ExecuteReader())
                {
                    AppSeries series = null;
                    AppBranches.BeginBulkOperation();
                    while (sqlReader.Read())
                    {
                        var seriesId = sqlReader.GetInt32(0);
                        if (series == null || series.AppSeriesID != seriesId)
                        {
                            series = AppSeries.FirstOrDefault(s => s.AppSeriesID == seriesId);
                        }
                        var branch = new AppBranch
                        {
                            AppSeries = series,
                            AppBranchID = sqlReader.GetInt32(1),
                            AppBranchName = sqlReader.GetString(2),
                            AppBranchFriendlyDescription = sqlReader.GetString(3)
                        };
                        AppBranches.Add(branch);
                        series?.ChildBranches.Add(branch);
                    }
                    AppBranches.EndBulkOperation();
                }
                sqlCmd = new SQLiteCommand("SELECT * FROM [AppUpdateClientTable] ORDER BY AppBrancheID ASC", sqlCon);
                using (var sqlReader = sqlCmd.ExecuteReader())
                {
                    AppBranch branch = null;
                    ClientInfos.BeginBulkOperation();
                    while (sqlReader.Read())
                    {
                        var branchId = sqlReader.GetInt32(3);
                        if (branch == null || branch.AppBranchID != branchId)
                        {
                            branch = AppBranches.FirstOrDefault(b => b.AppBranchID == branchId);
                        }

                        IPAddress ipAddress;
                        if (sqlReader.IsDBNull(4) || !IPAddress.TryParse(sqlReader.GetString(4), out ipAddress))
                        {
                            ipAddress = IPAddress.None;
                        }
                        var client = new ClientInfo
                        {
                            MachineID = sqlReader.GetString(0),
                            ClientName = sqlReader.GetString(1),
                            Company = sqlReader.GetString(2),
                            AppBranch = branch,
                            IPAddress = ipAddress,
                            RsaPrivateKey = sqlReader.GetString(5),
                            Expiration = sqlReader.GetDateTime(6),
                            Serial = sqlReader.GetString(7),
                            SetupLocation = sqlReader.GetString(8)
                        };
                        ClientInfos.Add(client);
                        branch?.ChildClients.Add(client);
                    }
                    ClientInfos.BeginBulkOperation();
                }
            }).ContinueWith(t => sqlCon.Close());
        }

        public BulkObservableCollection<IClientInfoBindable> ClientInfos { get; } = new BulkObservableCollection<IClientInfoBindable>();
        public BulkObservableCollection<AppBranch> AppBranches { get; } = new BulkObservableCollection<AppBranch>();

        public BulkObservableCollection<AppSeries> AppSeries { get; } = new BulkObservableCollection<AppSeries>();


    }
}