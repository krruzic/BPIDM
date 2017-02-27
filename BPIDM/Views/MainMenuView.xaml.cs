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
                    sv.ScrollToTop();
                    MenuList.ScrollIntoView(MenuList.Items[index]);
                }
                Console.WriteLine(cur.CategoryName);
            }
        }

        private void listViewDishClicked(object sender, RoutedEventArgs e)
        {
            _NavigationFrame.Navigate(new DishDetailsView());
            this._NavigationFrame.NavigationService.Navigate(new Uri("Views/DishDetailsView.xaml", UriKind.Relative));
            this.MenuJumperList.Visibility = Visibility.Hidden;
            this.MenuList.Visibility = Visibility.Hidden;
            this.searchFilter.Visibility = Visibility.Hidden;
        }
    }
}
