using BPIDM.Events;
using BPIDM.Views;
using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System.Dynamic;
using System.Windows;
using System.Windows.Controls.Primitives;

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


        private readonly IWindowManager _windowManager;       

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
            this._windowManager = new WindowManager();
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
            if (message.item.category == "Beverages & Sides".ToUpper())
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.Manual;
                settings.StaysOpen = false;
                settings.Height = 300;
                settings.Width = 400;
                settings.SizeToContent = SizeToContent.WidthAndHeight;
                settings.Placement = PlacementMode.Center;
                settings.PlacementTarget = GetView(null);                
                _windowManager.ShowPopup(new DrinkDetailsViewModel(_events, message.item), null, settings);                
            }
            else
            {
                this.ActivateItem(new DishDetailsViewModel(_events, message.item));
            }
            
        }

        public void Handle(ShowHelpEvent message)
        {
            if (message.Param.Equals("OrderConfirm"))
                Dialog.SetPage("OrderConfirm");
            else 
                Dialog.SetPage(this.ActiveItem.DisplayName);
            DialogHost.Show(dialogView);
        }
    }
}
