using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

using Newtonsoft.Json;
using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuViewModel : Screen
    {
        private string filterText;

        public bool IsLoading;

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

            IsLoading = true;
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
            e.Accepted = e.Item.ToString().ToUpper().Contains(FilterText.ToUpper());
        }

        private void OnFilterChanged()
        {
            MenuCollection.View.Refresh();
        }

        private async Task FillMenu()
        {
            if ((string) Application.Current.Properties["menuJSON"] == "")
                Application.Current.Properties["menuJSON"] = LoadJson();
            dynamic jsonObj = JsonConvert.DeserializeObject<RootMenuObject>((string) Application.Current.Properties["menuJSON"]);
            BPCategoryViewModel temp = jsonObj.Menu[0];
            foreach (BPCategoryViewModel cat in jsonObj.Menu)
            {
                foreach (BPMenuViewModel item in cat.Content)
                {
                    cat.Image = item.image;
                    item.category = cat.CategoryName;
                    MenuContent.Add(item);
                    await Task.Delay(1);
                }
                JumperContent.Add(cat);
            }
            IsLoading = false;
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