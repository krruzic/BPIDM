using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

using Newtonsoft.Json;
using Caliburn.Micro;
using System;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuViewModel : Screen
    {
        private string filterText;
        public CollectionViewSource MenuCollection { get; set; }

        public ObservableCollection<BPMenuViewModel> MenuContent { get; private set; }
        public ObservableCollection<BPCategoryViewModel> JumperContent { get; private set; }

        public MainMenuViewModel()
        {
            MenuContent = new ObservableCollection<BPMenuViewModel>();
            JumperContent = new ObservableCollection<BPCategoryViewModel>();

            MenuCollection = new CollectionViewSource();
            MenuCollection.Source = this.MenuContent;
            MenuCollection.Filter += Filter;
            MenuCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));
            FillMenu();
        }

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnFilterChanged();
            }
        }

        private void Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            BPMenuViewModel item = e.Item as BPMenuViewModel;
            if (string.IsNullOrWhiteSpace(this.FilterText) || this.FilterText.Length == 0)
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = item.title.ToUpper().Contains(FilterText.ToUpper());
            }
        }

        private void OnFilterChanged()
        {
            MenuCollection.View.Refresh();
        }

        private void FillMenu()
        {
            string json = LoadJson();
            dynamic jsonObj = JsonConvert.DeserializeObject<RootMenuObject>(json);
            BPCategoryViewModel temp = jsonObj.Menu[0];
            foreach (BPCategoryViewModel cat in jsonObj.Menu)
            {
                JumperContent.Add(cat);
                foreach (BPMenuViewModel item in cat.Content)
                {
                    item.category = cat.CategoryName;
                    MenuContent.Add(item);
                }
            }
        }

        private string LoadJson()
        {
            using (StreamReader r = new StreamReader(@"Data/menu.json"))
            {
                return r.ReadToEnd();
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}