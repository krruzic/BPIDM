using BPIDM.Events;
using BPIDM.Views;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;

namespace BPIDM.ViewModels
{
    class MainViewModel : Conductor<IScreen>, IHandle<TestEvent>, IHandle<DishDetailEvent>, IHandle<ShowHelpEvent>
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
        public BPDialogViewModel dialog { get; private set; }
        private BPDialogView dialogView;
        public MainViewModel(HeaderViewModel Header, FooterViewModel Footer, MainMenuViewModel MenuPane, BPDialogViewModel dialog, IEventAggregator events)
        {
            this.Header = Header;
            this.Footer = Footer;
            this._events = events;
            DisplayName = "BPIDM";
            events.Subscribe(this);
            this.MenuPane = MenuPane;
            this.dialog = dialog;
            this.dialogView = new BPDialogView();
            ViewModelBinder.Bind(dialog, dialogView, null);
            this.ActivateItem(MenuPane);
        }

        public void Handle(TestEvent message)
        {
            if (message.res != "BILL")
                ActivateItem(MenuPane);
            else
                ActivateItem(new BillSplittingViewModel(_events));
        }

        public void Handle(DishDetailEvent message)
        {
            this.ActivateItem(new DishDetailsViewModel(_events, message.item));
        }

        public void Handle(ShowHelpEvent message)
        {
            System.Console.WriteLine(this.ActiveItem);
            dialog.setPage(this.ActiveItem.DisplayName);
            DialogHost.Show(dialogView);
        }
    }
}
