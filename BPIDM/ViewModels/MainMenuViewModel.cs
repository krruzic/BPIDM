using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

using Newtonsoft.Json;
using Caliburn.Micro;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using BPIDM.Views;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    [Export(typeof(MainMenuViewModel))]
    public partial class MainMenuViewModel : Screen, IDeactivate
    {
        private string filterText;
        
        public bool IsLoading;

        public CollectionViewSource MenuCollection { get; set; }
        public BindableCollection<BPMenuViewModel> MenuContent { get; private set; }
        public BindableCollection<BPCategoryViewModel> JumperContent { get; private set; }
        private readonly IEventAggregator _events;

        [ImportingConstructor]
        public MainMenuViewModel(IEventAggregator events)
        {
            _events = events;
            MenuContent = new BindableCollection<BPMenuViewModel>();
            JumperContent = new BindableCollection<BPCategoryViewModel>();

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
            RootMenuObject jsonObj = getJsonFromFile((string)Application.Current.Properties["menuJSON"]);
            foreach (dynamic cat in jsonObj.Menu)
            {
                cat.Image = cat.Content[0].image;
                BPCategoryViewModel cur = new BPCategoryViewModel(cat);
                JumperContent.Add(cur);
                foreach (dynamic item in cat.Content)
                {
                    item.category = cat.CategoryName;
                    BPMenuViewModel curM = new BPMenuViewModel(item, _events);
                    cur.Image = item.image;
                    MenuContent.Add(curM);
                    await Task.Delay(1);
                }
            }
            IsLoading = false;
        }

        private RootMenuObject getJsonFromFile(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer s = new JsonSerializer();
                return (RootMenuObject)s.Deserialize(file, typeof(RootMenuObject));
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }
    }
}