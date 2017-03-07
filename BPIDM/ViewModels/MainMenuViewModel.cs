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
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.ObjectModel;

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    [Export(typeof(MainMenuViewModel))]
    public partial class MainMenuViewModel : Screen, IHandle<FilterEvent>
    {

        public ICollectionView MenuCollection { get; set; }
        private ObservableCollection<BPMenuViewModel> _MenuList;
        public ObservableCollection<BPMenuViewModel> MenuList
        {
            get { return _MenuList; }
            set
            {
                _MenuList = value;
                NotifyOfPropertyChange(() => MenuList);
            }
        }

        private ObservableCollection<BPCategoryViewModel> _MenuJumperList;
        public ObservableCollection<BPCategoryViewModel> MenuJumperList
        {
            get { return _MenuJumperList; }
            set
            {
                _MenuJumperList = value;
                NotifyOfPropertyChange(() => MenuJumperList);
            }
        }

        public List<BPMenuViewModel> MenuContent { get; private set; }
        public List<BPCategoryViewModel> JumperContent { get; private set; }

        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public MainMenuViewModel(IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            MenuContent = new List<BPMenuViewModel>();
            JumperContent = new List<BPCategoryViewModel>();
        }

        private string filterText;
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                NotifyOfPropertyChange(() => FilterText);
                Search();
            }
        }

        private BindableCollection<string> filterButtonText;
        public BindableCollection<string> FilterButtonText
        {
            get { return filterButtonText; }
            set
            {
                filterButtonText = value;
                NotifyOfPropertyChange(() => FilterButtonText);
            }
        }


        // if you click the search button
        public void Search()
        {
            if (FilterText == null)
                return;
            foreach (BPMenuViewModel i in MenuList)
            {
                if (i.ToString().ToUpper().Contains(FilterText.ToUpper()) || String.IsNullOrEmpty(FilterText))
                    i.isVisible = true;
                else
                    i.isVisible = false;
            }
        }

        private async Task<List<Tuple<BPCategoryViewModel, BPMenuViewModel>>> FillMenu()
        {
            List<Tuple<BPCategoryViewModel, BPMenuViewModel>> res = new List<Tuple<BPCategoryViewModel, BPMenuViewModel>>();
            await Task.Run(() =>
            {
                RootMenuObject jsonObj = getJsonFromFile((string)Application.Current.Properties["menuJSON"]);
                foreach (dynamic cat in jsonObj.Menu)
                {
                    BPCategoryViewModel cur = new BPCategoryViewModel(cat);
                    cur.Image = cat.Content[0].image;
                    foreach (dynamic item in cat.Content)
                    {
                        item.category = cat.CategoryName;
                        BPMenuViewModel curM = new BPMenuViewModel(item, _events);
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
            MenuList = new ObservableCollection<BPMenuViewModel>();
            MenuJumperList = new BindableCollection<BPCategoryViewModel>();

            MenuCollection = CollectionViewSource.GetDefaultView(MenuList);
            MenuCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));
            //FilterButtonText = new BindableCollection<string>();
            List<Tuple<BPCategoryViewModel, BPMenuViewModel>> menus = await FillMenu();
            // each menu item has a category item because its a tuple... 
            //this gets only distinct categories from the tuple
            var cats = menus.GroupBy(t => t.Item1.CategoryName)
                                        .Select(g => g.First().Item1).ToList();
            var items = menus.Select(t => t.Item2).ToList();

            IObservable<BPCategoryViewModel> catsToLoad = cats.ToObservable<BPCategoryViewModel>();
            catsToLoad.Subscribe<BPCategoryViewModel>(c =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action<BPCategoryViewModel>(AddCategoryToCollection), c);
            }, () =>
            {
            }
            );

            IObservable<BPMenuViewModel> itemsToLoad = items.ToObservable<BPMenuViewModel>();
            itemsToLoad.Subscribe<BPMenuViewModel>(i =>
            {
                Dispatcher.CurrentDispatcher.Invoke(DispatcherPriority.Background, new Action<BPMenuViewModel>(AddMItemToCollection), i);
            }, () =>
                {
                }
            );
        }

        private void AddCategoryToCollection(BPCategoryViewModel cat)
        {
            this.MenuJumperList.Add(cat);
        }

        private void AddMItemToCollection(BPMenuViewModel item)
        {
            this.MenuList.Add(item);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
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