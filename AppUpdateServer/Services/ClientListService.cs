using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Models;
using AppUpdateServer.Properties;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Services
{
    public sealed class ClientListService : BindableBase, IClientListService
    {
        public ClientListService()
        {
            InitializeFromDatabase();
        }

        ~ClientListService()
        {
            Dispose();
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
                    IAppSeries series = null;
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
                    IAppBranch branch = null;
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

        public BulkObservableCollection<IAppBranch> AppBranches { get; } = new BulkObservableCollection<IAppBranch>();

        public BulkObservableCollection<IAppSeries> AppSeries { get; } = new BulkObservableCollection<IAppSeries>();

        #region SelectClientInfo

        /// <summary>
        /// 获取或设置 SelectClientInfo 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public IClientInfoBindable SelectedClientInfo
        {
            get { return SelectedItem as IClientInfoBindable; }
        }

        #endregion

        #region SelectedAppBranch

        /// <summary>
        /// 获取或设置 SelectedAppBranch 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public IAppBranch SelectedAppBranch
        {
            get { return SelectedItem as IAppBranch; }
        }

        #endregion

        #region SelectedAppSeries

        /// <summary>
        /// 获取或设置 SelectedAppSeries 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public IAppSeries SelectedAppSeries
        {
            get { return SelectedItem as IAppSeries; }
        }

        #endregion

        #region SelectedItem

        private object _selectedItem;

        /// <summary>
        /// 获取或设置 SelectedItem 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public object SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                SetProperty(ref _selectedItem, value);
                OnPropertyChanged("SelectedClientInfo");
                OnPropertyChanged("SelectedAppBranch");
                OnPropertyChanged("SelectedAppSeries");
            }
        }

        #endregion

        #region IDisposable

        private bool _disposed;

        public void Dispose()
        {
            if (!_disposed)
            {
                //UnBindItemsControl();
                _disposed = true;
            }
        }

        #endregion
    }
}
