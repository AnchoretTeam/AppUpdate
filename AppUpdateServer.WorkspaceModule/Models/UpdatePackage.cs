using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUpdate.Core.Models;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Models
{
    internal abstract class UpdatePackageBindableBase : List<string>, IUpdateFileCollection, INotifyPropertyChanged
    {
        public abstract DateTime ReleaseTime { get; set; }
        public abstract string Description { get; set; }
        public abstract UpdateType UpdateType { get; set; }
        public abstract event PropertyChangedEventHandler PropertyChanged;
    }
    public sealed class UpdatePackage : BindableBase
    {
        #region ReleaseTime

        private DateTime _releaseTime;

        /// <summary>
        /// 获取或设置 ReleaseTime 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public DateTime ReleaseTime
        {
            get { return _releaseTime; }
            set { SetProperty(ref _releaseTime, value); }
        }

        #endregion

        #region Description

        private string _description = string.Empty;

        /// <summary>
        /// 获取或设置 Description 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        #endregion

        #region UpdateType

        private UpdateType _updateType = UpdateType.Update;

        /// <summary>
        /// 获取或设置 UpdateType 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public UpdateType UpdateType
        {
            get { return _updateType; }
            set { SetProperty(ref _updateType, value); }
        }

        #endregion
    }
}
