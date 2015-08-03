using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppUpdateServer.AttachedProperties
{
    public static class BindableSelectedItemHelper
    {
        #region Properties

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(BindableSelectedItemHelper),
            new FrameworkPropertyMetadata(null, OnSelectedItemPropertyChanged));

        public static readonly DependencyProperty AttachProperty = DependencyProperty.RegisterAttached("Attach", typeof(bool), typeof(BindableSelectedItemHelper), new PropertyMetadata(false, Attach));

        private static readonly DependencyProperty IsUpdatingProperty = DependencyProperty.RegisterAttached("IsUpdating", typeof(bool), typeof(BindableSelectedItemHelper));

        #endregion

        #region Implementation

        public static void SetAttach(DependencyObject dp, bool value)
        {
            dp.SetValue(AttachProperty, value);
        }

        public static bool GetAttach(DependencyObject dp)
        {
            return (bool)dp.GetValue(AttachProperty);
        }

        public static string GetSelectedItem(DependencyObject dp)
        {
            return (string)dp.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(DependencyObject dp, object value)
        {
            dp.SetValue(SelectedItemProperty, value);
        }

        private static bool GetIsUpdating(DependencyObject dp)
        {
            return (bool)dp.GetValue(IsUpdatingProperty);
        }

        private static void SetIsUpdating(DependencyObject dp, bool value)
        {
            dp.SetValue(IsUpdatingProperty, value);
        }

        private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var treeListView = sender as TreeView;
            if (treeListView != null)
            {
                if ((bool)e.OldValue)
                    treeListView.SelectedItemChanged -= SelectedItemChanged;

                if ((bool)e.NewValue)
                    treeListView.SelectedItemChanged += SelectedItemChanged;
            }
        }

        private static void OnSelectedItemPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var treeListView = sender as TreeView;
            if (treeListView != null)
            {
                treeListView.SelectedItemChanged -= SelectedItemChanged;

                if (!GetIsUpdating(treeListView))
                {
                    var treeViewItem = e.NewValue as TreeViewItem;
                    if (treeViewItem != null)
                    {
                        treeViewItem.SetValue(TreeViewItem.IsSelectedProperty, true);
                        treeViewItem.Focus();
                    }
                    else
                    {
                        var itemsHostProperty = treeListView.GetType().GetProperty("ItemsHost", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                        var itemsHost = itemsHostProperty?.GetValue(treeListView, null) as Panel;

                        if (itemsHost == null)
                        {
                            return;
                        }

                        foreach (var item in itemsHost.Children.OfType<TreeViewItem>())
                        {
                            if (WalkTreeViewItem(treeListView, item, e.NewValue))
                            {
                                break;
                            }
                        }
                    }

                }

                treeListView.SelectedItemChanged += SelectedItemChanged;
            }
        }
        public static bool WalkTreeViewItem(TreeView tree, TreeViewItem treeViewItem, object selectedValue)
        {
            if (treeViewItem.DataContext == selectedValue)
            {
                treeViewItem.SetValue(TreeViewItem.IsSelectedProperty, true);
                treeViewItem.Focus();
                treeViewItem.BringIntoView();
                return true;
            }
            var itemsHostProperty = treeViewItem.GetType().GetProperty("ItemsHost", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var itemsHost = itemsHostProperty?.GetValue(treeViewItem, null) as Panel;

            if (itemsHost == null)
            {
                return false;
            }

            foreach (var item in itemsHost.Children.OfType<TreeViewItem>())
            {
                var oldExpanded = item.IsExpanded;
                item.IsExpanded = true;
                item.UpdateLayout();
                if (WalkTreeViewItem(tree, item, selectedValue))
                {
                    return true;
                }
                item.IsExpanded = oldExpanded;
            }

            return false;
        }

        private static void SelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var treeListView = sender as TreeView;
            if (treeListView != null)
            {
                SetIsUpdating(treeListView, true);
                SetSelectedItem(treeListView, treeListView.SelectedItem);
                SetIsUpdating(treeListView, false);
            }
        }
        #endregion
    }
}
