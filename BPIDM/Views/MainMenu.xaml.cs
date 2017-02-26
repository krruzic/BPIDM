using System;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows;

using Newtonsoft.Json;
using Xceed.Wpf.Toolkit.Core.Utilities;


namespace BPIDM.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable, INotifyPropertyChanged
    {
        private string filterText;
        public CollectionViewSource menuCollection;
        public ICollectionView SourceCollection;

        public ObservableCollection<BPMenu> MenuContent { get; private set; }
        public ObservableCollection<BPCategoryItem> JumperContent { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMenu()
        {
            InitializeComponent();
            MenuContent = new ObservableCollection<BPMenu>();
            JumperContent = new ObservableCollection<BPCategoryItem>();
            menuCollection = new CollectionViewSource();
            ICollectionView SourceCollection = CollectionViewSource.GetDefaultView(MenuList.ItemsSource);
            menuCollection.Source = MenuContent;
            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(ComponentLoaded);
        }

        private void ComponentLoaded(object sender, RoutedEventArgs e)
        {
            FillMenu();
        }

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                CollectionViewSource.GetDefaultView(MenuList.ItemsSource).Refresh();
                RaisePropertyChanged("FilterText");
            }
        }

        private void menuCollection_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }
            BPMenu item = e.Item as BPMenu;
            if (item.title.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int findFirstInCategory(string str)
        {
            for (int i = 0; i < MenuList.Items.Count; i++)
            {
                BPMenu item = (BPMenu)MenuList.Items[i];
                if (item.category.Equals(str))
                {
                    return i;
                }
            }
            return -1;
        }

        private void FillMenu()
        {
            string json = LoadJson();
            dynamic jsonObj = JsonConvert.DeserializeObject<RootMenuObject>(json);
            int i = 0;
            foreach (BPCategoryItem cat in jsonObj.Menu)
            {
                JumperContent.Add(cat);
                foreach (BPMenu item in cat.Content)
                {
                    item.category = cat.CategoryName;
                    MenuContent.Add(item);
                }
            }
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ITEM SELECTED");
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                BPCategoryItem cur = (BPCategoryItem) item;
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

        private string LoadJson()
        {
            using (StreamReader r = new StreamReader(@"Data/menu.json"))
            {
                return r.ReadToEnd();
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
    }
}
