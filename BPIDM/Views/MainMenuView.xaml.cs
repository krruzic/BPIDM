using System;
using System.Windows.Controls;
using System.Windows;

using Xceed.Wpf.Toolkit.Core.Utilities;
using BPIDM.ViewModels;

namespace BPIDM.Views
{
    public partial class MainMenuView
    {
        public MainMenuView()
        {
            InitializeComponent();
            DataContext = new MainMenuViewModel();
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
            Console.WriteLine("ITEM SELECTED");
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                BPCategoryViewModel cur = (BPCategoryViewModel)item;
                int index = findFirstInCategory(cur.CategoryName);
                ScrollViewer sv = VisualTreeHelperEx.FindDescendantByType<ScrollViewer>(MenuList);
                if (index != -1)
                {
                    sv.ScrollToBottom();
                    MenuList.ScrollIntoView(MenuList.Items[index]);
                }
                Console.WriteLine(cur.CategoryName);
            }
        }
    }
}
