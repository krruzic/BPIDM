using Caliburn.Micro;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenuViewModel : Screen
    {

        public ICollectionView MenuCollection { get; set; }
        private BindableCollection<BPMenuItemViewModel> _MenuList;
        public BindableCollection<BPMenuItemViewModel> MenuList
        {
            get { return _MenuList; }
            set
            {
                _MenuList = value;
                NotifyOfPropertyChange(() => MenuList);
            }
        }

        private BindableCollection<BPCategoryItemViewModel> _MenuJumperList;
        public BindableCollection<BPCategoryItemViewModel> MenuJumperList
        {
            get { return _MenuJumperList; }
            set
            {
                _MenuJumperList = value;
                NotifyOfPropertyChange(() => MenuJumperList);
            }
        }

        private readonly IEventAggregator _events;
        public MainMenuViewModel(IEventAggregator events)
        {
            this.DisplayName = "MainMenuViewModel";
            _events = events;
            events.Subscribe(this);
            FilterButtonList = new List<string>();
            filterText = "";
            SearchIcon = "Magnify";
            IsLoaded = true;
        }

        private bool isLoaded;
        public bool IsLoaded
        {
            get { return isLoaded; }
            set {
                isLoaded = value;
                NotifyOfPropertyChange(() => IsLoaded);
            }
        }

        private string filterText;
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                if (value == "") SearchIcon = "Magnify";
                else SearchIcon = "Close";
                NotifyOfPropertyChange(() => FilterText);
                Search();
            }
        }

        private List<string> filterButtonList;
        public List<string> FilterButtonList
        {
            get { return filterButtonList; }
            set
            {
                filterButtonList = value;
                NotifyOfPropertyChange(() => FilterButtonList);
            }
        }

        private string searchIcon;
        public string SearchIcon
        {
            get { return searchIcon; }
            set
            {
                searchIcon = value;
                NotifyOfPropertyChange(() => SearchIcon);
            }
        }

        // if you click the search button
        public void Search()
        {
            if (FilterText == null)
                return;
            foreach (BPMenuItemViewModel i in MenuList)
            {
                if (i.ToString().ToUpper().Contains(FilterText.ToUpper()) || String.IsNullOrEmpty(FilterText))
                    i.isVisible = true;
                else
                    i.isVisible = false;
            }
        }

        public void SearchButton()
        {
            if (SearchIcon == "Close")
                FilterText = "";
        }

        public void FilterButton(string f)
        {
            if (FilterButtonList.Contains(f))
                FilterButtonList.Remove(f);
            else FilterButtonList.Add(f);
            FilterByButtons();
        }

        private void FilterByButtons()
        {
            foreach (BPMenuItemViewModel i in MenuList)
            {
                if (FilterButtonList.Count == 0 || !FilterButtonList.Except(i.tags).Any())
                    i.isVisible = true;
                else
                    i.isVisible = false;
            }
        }

        private void FilterOneItem(BPMenuItemViewModel i)
        {
            if (FilterButtonList.Count == 0 || !FilterButtonList.Except(i.tags).Any())
                if (i.ToString().ToUpper().Contains(FilterText.ToUpper()) || String.IsNullOrEmpty(FilterText))
                    i.isVisible = true;
                else
                    i.isVisible = false;
            else
                i.isVisible = false;
        }


        private async Task<List<Tuple<BPCategoryItemViewModel, BPMenuItemViewModel>>> FillMenu()
        {
            List<Tuple<BPCategoryItemViewModel, BPMenuItemViewModel>> res = new List<Tuple<BPCategoryItemViewModel, BPMenuItemViewModel>>();
            await Task.Run(() =>
            {
                RootMenuObject jsonObj = getJsonFromFile((string)Application.Current.Properties["menuJSON"]);
                foreach (dynamic cat in jsonObj.Menu)
                {
                    BPCategoryItemViewModel cur = new BPCategoryItemViewModel(cat);
                    cur.Image = cat.Content[0].image;
                    foreach (dynamic item in cat.Content)
                    {
                        item.category = cat.CategoryName;
                        BPMenuItemViewModel curM = new BPMenuItemViewModel(item, _events);
                        res.Add(Tuple.Create(cur, curM));
                    }
                }
            });
            return res;
        }

        private RootMenuObject getJsonFromFile(string path)
        {
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer s = new JsonSerializer();
                return (RootMenuObject)s.Deserialize(file, typeof(RootMenuObject));
            }
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            MenuList = new BindableCollection<BPMenuItemViewModel>();
            MenuJumperList = new BindableCollection<BPCategoryItemViewModel>();

            MenuCollection = CollectionViewSource.GetDefaultView(MenuList);
            MenuCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));
            List<Tuple<BPCategoryItemViewModel, BPMenuItemViewModel>> menus = await FillMenu();
            // each menu item has a category item because its a tuple... 
            //this gets only distinct categories from the tuple
            var cats = menus.GroupBy(t => t.Item1.CategoryName)
                                        .Select(g => g.First().Item1).ToList();
            var items = menus.Select(t => t.Item2).ToList();

            IObservable<BPCategoryItemViewModel> catsToLoad = cats.ToObservable<BPCategoryItemViewModel>();
            catsToLoad.Subscribe<BPCategoryItemViewModel>(c =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action<BPCategoryItemViewModel>(AddCategoryToCollection), c);
            }, () =>
            {
            }
            );

            IObservable<BPMenuItemViewModel> itemsToLoad = items.ToObservable<BPMenuItemViewModel>();
            itemsToLoad.Subscribe<BPMenuItemViewModel>(i =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action<BPMenuItemViewModel>(AddMItemToCollection), i);
            }, () =>
                {
                    IsLoaded = true;
                }
            );
        }

        private void AddCategoryToCollection(BPCategoryItemViewModel cat)
        {
            this.MenuJumperList.Add(cat);
        }

        private void AddMItemToCollection(BPMenuItemViewModel item)
        {
            FilterOneItem(item);
            this.MenuList.Add(item);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            filterText = "";
            FilterButtonList.Clear();
            IsLoaded = true;
        }
    }
}