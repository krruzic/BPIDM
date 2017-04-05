using BPIDM.Events;
using BPIDM.Views;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;

namespace BPIDM.ViewModels
{
    class MainViewModel : Conductor<IScreen>, IHandle<TestEvent>, IHandle<DishDetailEvent>, IHandle<ShowHelpEvent>
    {
        private readonly IEventAggregator _events;

        public HeaderViewModel Header { get; private set; }
        public FooterViewModel Footer { get; private set; }
        public BillSplittingViewModel Bill { get; private set; }
        public MainMenuViewModel MenuPane { get; private set; }
        public BPDialogViewModel Dialog { get; private set; }

        private BPDialogView dialogView;
        public MainViewModel(HeaderViewModel Header, FooterViewModel Footer, 
            MainMenuViewModel MenuPane, BPDialogViewModel Dialog, BillSplittingViewModel Bill,
            IEventAggregator events)
        {
            this.Header = Header;
            this.Footer = Footer;
            this.Bill = Bill;
            this._events = events;
            DisplayName = "BPIDM";
            events.Subscribe(this);
            this.MenuPane = MenuPane;
            this.Dialog = Dialog;
            this.dialogView = new BPDialogView();
            ViewModelBinder.Bind(Dialog, dialogView, null);
            this.ActivateItem(MenuPane);
        }

        public void Handle(TestEvent message)
        {
            if (message.res != "BILL")
                ActivateItem(MenuPane);
            else
                ActivateItem(Bill);
        }

        public void Handle(DishDetailEvent message)
        {
            this.ActivateItem(new DishDetailsViewModel(_events, message.item));
        }

        public void Handle(ShowHelpEvent message)
        {
            System.Console.WriteLine(this.ActiveItem);
            Dialog.SetPage(this.ActiveItem.DisplayName);
            DialogHost.Show(dialogView);
        }
    }
}
