using BPIDM.Events;
using Caliburn.Micro;
using System;
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
            // remove this later, but images aren't being freed!
            // see http://stackoverflow.com/a/11203193
            GC.Collect();
            this.ActivateItem(MenuPane);
        }

        public void Handle(DishDetailEvent message)
        {
            this.ActivateItem(new DishDetailsViewModel(_events, message.item));
            GC.Collect();
        }
    }
}
