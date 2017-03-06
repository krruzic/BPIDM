using BPIDM.Events;
using Caliburn.Micro;
using System.ComponentModel.Composition;
namespace BPIDM.ViewModels
{
    [Export(typeof(MainViewModel))]
    class MainViewModel : Conductor<IScreen>, IHandle<TestEvent>, IHandle<DishDetailEvent>
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

        public void Handle(TestEvent message)
        {
            if (message.res != "BILL")
                this.ActivateItem(MenuPane);
            else
                this.ActivateItem(new BillSplittingViewModel());
        }

        public void Handle(DishDetailEvent message)
        {
            this.ActivateItem(new DishDetailsViewModel(_events, message.item));
        }

        public void FilterButtonPressed(string fs)
        {
            _events.PublishOnUIThread(new FilterEvent(fs));
        }
    }
}
