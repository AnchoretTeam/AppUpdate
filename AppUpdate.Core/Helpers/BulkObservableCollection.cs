using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Threading;

namespace AppUpdate.Core.Helpers
{
    public class BulkObservableCollection<T> : ObservableCollection<T>
    {
        private delegate void AddRangeCallback(IList<T> items);
        private delegate void SetItemCallback(int index, T item);
        private delegate void RemoveItemCallback(int index);
        private delegate void RemoveRangeCallback(int baseIndex, int count);
        private delegate void ClearItemsCallback();
        private delegate void InsertItemCallback(int index, T item);
        private delegate void MoveItemCallback(int oldIndex, int newIndex);
        private volatile int _rangeOperationCount;
        private volatile bool _collectionChangedDuringRangeOperation;
        private readonly Dispatcher _dispatcher;
        private ReadOnlyObservableCollection<T> _readOnlyAccessor;
        public BulkObservableCollection()
        {
            //_dispatcher = Dispatcher.CurrentDispatcher;
            _dispatcher = Application.Current.Dispatcher;
        }
        public void AddRange(IEnumerable<T> items)
        {
            if (items == null)
            {
                return;
            }
            if (_dispatcher.CheckAccess())
            {
                try
                {
                    BeginBulkOperation();
                    foreach (T current in items)
                    {
                        Add(current);
                    }
                }
                finally
                {
                    EndBulkOperation();
                }
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return;
            }
            _dispatcher.BeginInvoke(DispatcherPriority.Send, new AddRangeCallback(AddRange), items);
        }
        public void BeginBulkOperation()
        {
            _rangeOperationCount++;
        }
        public void EndBulkOperation()
        {
            if (_rangeOperationCount > 0 && --_rangeOperationCount == 0 && _collectionChangedDuringRangeOperation)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                _collectionChangedDuringRangeOperation = false;
            }
        }
        public ReadOnlyObservableCollection<T> AsReadOnly()
        {
            if (_readOnlyAccessor == null)
            {
                _readOnlyAccessor = new ReadOnlyObservableCollection<T>(this);
            }
            return _readOnlyAccessor;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (_rangeOperationCount == 0)
            {
                if (_dispatcher.CheckAccess())
                {
                    base.OnCollectionChanged(e);
                }
                else
                {
                    _dispatcher.Invoke(DispatcherPriority.Render,
                        new Action(() => base.OnCollectionChanged(e)));
                }
                return;
            }
            _collectionChangedDuringRangeOperation = true;
        }
        protected override void SetItem(int index, T item)
        {
            if (_dispatcher.CheckAccess())
            {
                base.SetItem(index, item);
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new SetItemCallback(SetItem), index, item);
        }
        protected override void InsertItem(int index, T item)
        {
            if (_dispatcher.CheckAccess())
            {
                base.InsertItem(index, item);
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new InsertItemCallback(InsertItem), index, item);
        }
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            if (_dispatcher.CheckAccess())
            {
                base.MoveItem(oldIndex, newIndex);
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new MoveItemCallback(MoveItem), oldIndex, newIndex);
        }
        protected override void RemoveItem(int index)
        {
            if (_dispatcher.CheckAccess())
            {
                base.RemoveItem(index);
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new RemoveItemCallback(RemoveItem), index);
        }
        public void RemoveRange(int baseIndex, int count)
        {
            if (count < 1 || baseIndex < 0 || baseIndex + count >= Count)
            {
                return;
            }
            if (_dispatcher.CheckAccess())
            {
                try
                {
                    BeginBulkOperation();
                    for (var i = 0; i < count; i++)
                    {
                        RemoveAt(baseIndex + i);
                    }
                }
                finally
                {
                    EndBulkOperation();
                }
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new RemoveRangeCallback(RemoveRange), baseIndex, count);
        }

        protected override void ClearItems()
        {
            if (_dispatcher.CheckAccess())
            {
                base.ClearItems();
                return;
            }
            _dispatcher.Invoke(DispatcherPriority.Send, new ClearItemsCallback(ClearItems));
        }
    }
}
