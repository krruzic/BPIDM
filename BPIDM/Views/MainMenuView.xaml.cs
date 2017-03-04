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

        private void listViewDishClicked(object sender, RoutedEventArgs e)
        {
            //_NavigationFrame.Navigate(new DishDetailsView());
            //this._NavigationFrame.NavigationService.Navigate(new Uri("Views/DishDetailsView.xaml", UriKind.Relative));
            //this._NavigationFrame.NavigationService.Navigate(new Uri("Views/DrinkDetails.xaml", UriKind.Relative));

            Window window = new Window
            {
                Title = "Drink Details",
                Content = new DrinkDetails()
            };

            window.ShowDialog();
            this.MenuJumperList.Visibility = Visibility.Hidden;
            this.MenuList.Visibility = Visibility.Hidden;
            this.searchFilter.Visibility = Visibility.Hidden;
        }
    }
}
