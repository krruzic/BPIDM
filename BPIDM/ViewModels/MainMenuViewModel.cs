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

namespace BPIDM.ViewModels
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    [Export(typeof(MainMenuViewModel))]
    public partial class MainMenuViewModel : Screen, IHandle<FilterEvent>
    {

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
            FilterButtonText = new BindableCollection<string>();
            MenuCollection = CollectionViewSource.GetDefaultView(MenuList);
            MenuCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));
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

        private async Task FillMenu()
        {
            RootMenuObject jsonObj = getJsonFromFile((string)Application.Current.Properties["menuJSON"]);
            foreach (dynamic cat in jsonObj.Menu)
            {
                BPCategoryViewModel cur = new BPCategoryViewModel(cat);
                cur.Image = cat.Content[0].image;
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    MenuJumperList.Add(cur);
                }, DispatcherPriority.ContextIdle);
                foreach (dynamic item in cat.Content)
                {
                    item.category = cat.CategoryName;
                    BPMenuViewModel curM = new BPMenuViewModel(item, _events);
                    await Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        MenuList.Add(curM);
                    }, DispatcherPriority.ContextIdle);
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

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            await FillMenu();
        }

        protected override async void OnActivate()
        {
            System.Console.WriteLine("Starting a task!");
            await Task.Run(() => base.OnActivate());
            System.Console.WriteLine("activated!");
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