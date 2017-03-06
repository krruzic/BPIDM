using BPIDM.Events;
using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace BPIDM.ViewModels
{
    [Export(typeof(MainViewModel))]
    class MainViewModel : Conductor<IScreen>, IHandleWithTask<TestEvent>, IHandle<DishDetailEvent>
    {
        private readonly IEventAggregator _events;
        private HeaderViewModel _Header;
        public HeaderViewModel Header
        {
            get { return _Header; }
            set
            {
                _Header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        private FooterViewModel _Footer;
        public FooterViewModel Footer
        {
            get { return _Footer; }
            set
            {
                _Footer = value;
                NotifyOfPropertyChange(() => Footer);
            }
        }

        public MainMenuViewModel MenuPane { get; private set; }

        [ImportingConstructor]
        public MainViewModel(HeaderViewModel Header, FooterViewModel Footer, MainMenuViewModel MenuPane, IEventAggregator events)
        {
            this.Header = Header;
            this.Footer = Footer;
            this.MenuPane = MenuPane;
            this._events = events;
            DisplayName = "BPIDM";
            events.Subscribe(this);
            this.ActivateItem(MenuPane);
        }

        public async Task Handle(TestEvent message)
        {
            //MenuPane = null;
            if (message.res != "BILL")
                ActivateItem(MenuPane);
            else
                ActivateItem(new BillSplittingViewModel());
        }

        public void Handle(DishDetailEvent message)
        {
            this.ActivateItem(new DishDetailsViewModel(_events, message.item));
        }

        public async Task FilterButtonPressed(string fs)
        {
            _events.PublishOnBackgroundThread(new FilterEvent(fs));
            await Task.Delay(1);
        }
    }
}
