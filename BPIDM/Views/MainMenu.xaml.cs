using System;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Data;
using System.Windows;

using Newtonsoft.Json;

using BPIDM.Controls;
using BPIDM.Utils;
using System.ComponentModel;

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

        public ObservableCollection<BPMenuItem> MenuContent { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainMenu()
        {
            InitializeComponent();
            MenuContent = new ObservableCollection<BPMenuItem>();
            FillList();

            menuCollection = new CollectionViewSource();
            ICollectionView SourceCollection = CollectionViewSource.GetDefaultView(MenuList.ItemsSource);
            menuCollection.Source = MenuContent;
            this.DataContext = this;
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
            BPMenuItem item = e.Item as BPMenuItem;
            if (item.DisplayedName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void SearchBox_Search(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            BPMenuItem item = e.Item as BPMenuItem;
            if (item.DisplayedName.ToUpper().Contains(FilterText.ToUpper()))
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

        private void FillList()
        {
            string json = LoadJson();
            dynamic jsonObj = JsonConvert.DeserializeObject<RootMenuObject>(json);
            int i = 0;
            foreach (var cat in jsonObj.Menu)
            {
                foreach (var item in cat.Content)
                {
                    BPMenuItem bpitem = new BPMenuItem
                    { 
                        DisplayedName = item.title,
                        DisplayedPrice = item.retail_pricing,
                        DisplayedImage = (ImageSource)new ImageSourceConverter().ConvertFromString(@"C:\Users\krruz\Pictures\smartcrop\65-spaghetti.png"),
                        DisplayedCategory = cat.CategoryName,
                    };
                    MenuContent.Add(bpitem);
                    i++;
                }
            }
            Console.WriteLine(i);
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
