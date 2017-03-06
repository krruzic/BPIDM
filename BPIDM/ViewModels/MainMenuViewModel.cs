using Caliburn.Micro;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System;
using BPIDM.Events;
using System.Collections.Generic;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    [Export(typeof(MainMenuViewModel))]
    public partial class MainMenuViewModel : Screen, IHandle<FilterEvent>
    {
        private string filterText;
        private BindableCollection<string> filterButtonText = new BindableCollection<string>();

        public ICollectionView MenuCollection { get; set; }
        private BindableCollection<BPMenuViewModel> _MenuList;
        public BindableCollection<BPMenuViewModel> MenuList
        {
            get { return _MenuList; }
            set
            {
                _MenuList = value;
                NotifyOfPropertyChange(() => MenuList);
            }
        }

        public ICollectionView JumperCollection { get; set; }
        private BindableCollection<BPCategoryViewModel> _MenuJumperList;
        public BindableCollection<BPCategoryViewModel> MenuJumperList
        {
            get { return _MenuJumperList; }
            set
            {
                _MenuJumperList = value;
                NotifyOfPropertyChange(() => MenuJumperList);
            }
        }

        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public MainMenuViewModel(IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            MenuList = new BindableCollection<BPMenuViewModel>();
            MenuJumperList = new BindableCollection<BPCategoryViewModel>();

            MenuCollection = CollectionViewSource.GetDefaultView(MenuList);
            MenuCollection.Filter += Filter;
            MenuCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));

            JumperCollection = CollectionViewSource.GetDefaultView(MenuJumperList);
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

        public BindableCollection<string> FilterButtonText
        {
            get { return filterButtonText; }
            set
            {
                filterButtonText = value;
                NotifyOfPropertyChange(() => FilterButtonText);
            }
        }

        private bool Filter(object value)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                return true;
            }
            return value.ToString().ToUpper().Contains(FilterText.ToUpper());
        }

        private bool FilterButton(object value)
        {
            return true;
            //if (string.IsNullOrEmpty(FilterButtonText))
            //{
            //    return true;
            //}
            //return value.ToString().ToUpper().Contains(FilterButtonText.ToUpper());
        }

        private void OnFilterChanged()
        {
            MenuCollection.Refresh();
        }

        private void OnFilterButtonTextChanged()
        {
            MenuCollection.Refresh();
        }

        private async Task FillMenu()
        {
            RootMenuObject jsonObj = getJsonFromFile((string)Application.Current.Properties["menuJSON"]);
            foreach (dynamic cat in jsonObj.Menu)
            {
                BPCategoryViewModel cur = new BPCategoryViewModel(cat);
                cur.Image = cat.Content[0].image;
                MenuJumperList.Add(cur);
                foreach (dynamic item in cat.Content)
                {
                    item.category = cat.CategoryName;
                    BPMenuViewModel curM = new BPMenuViewModel(item, _events);
                    MenuList.Add(curM);
                    await Task.Delay(1);
                }
            }
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

        public void Handle(FilterEvent message)
        {
            if (FilterButtonText.Contains(message.filterstring))
                FilterButtonText.Remove(message.filterstring);
            else
                FilterButtonText.Add(message.filterstring);
        }
    }
}