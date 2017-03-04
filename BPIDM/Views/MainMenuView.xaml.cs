using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;

using Xceed.Wpf.Toolkit.Core.Utilities;
using BPIDM.ViewModels;

namespace BPIDM.Views
{
    public partial class MainMenuView
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private int findFirstInCategory(string str)
        {
            for (int i = 0; i < MenuList.Items.Count; i++)
            {
                BPMenuViewModel item = (BPMenuViewModel)MenuList.Items[i];
                if (item.category.Equals(str))
                {
                    return i;
                }
            }
            return -1;
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                int index = findFirstInCategory(item.ToString());
                ScrollViewer sv = VisualTreeHelperEx.FindDescendantByType<ScrollViewer>(MenuList);
                if (index != -1)
                {
                    sv.ScrollToTop();
                    MenuList.ScrollIntoView(MenuList.Items[index]);
                }
            }
        }
    }
}
