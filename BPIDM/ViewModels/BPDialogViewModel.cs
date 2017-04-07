using BPIDM.Events;
using Caliburn.Micro;

namespace BPIDM.ViewModels
{
    class BPDialogViewModel : PropertyChangedBase
    {
        internal struct HelpStruct
        {
            private string title;
            public string Title
            {
                get { return title; }
                set
                {
                    title = value;
                }
            }

            private string helpText;
            public string HelpText
            {
                get { return helpText; }
                set { helpText = value; }
            }


            public HelpStruct(string t, string h)
            {
                title = t;
                helpText = h;
            }
        }

        private HelpStruct active;
        public HelpStruct Active
        {
            get { return active; }
            set
            {
                active = value;
                NotifyOfPropertyChange(() => Active);
            }
        }

        private string rightButtonText = "Got it!";
        public string RightButtonText
        {
            get { return rightButtonText; }
            set
            {
                rightButtonText = value;
                NotifyOfPropertyChange(() => RightButtonText);
            }
        }

        private bool showCancel = false;
        public bool ShowCancel
        {
            get { return showCancel; }
            set
            {
                showCancel = value;
                NotifyOfPropertyChange(() => ShowCancel);
            }
        }

        private HelpStruct menuPane = new HelpStruct(
            "Menu Overview Help",
            @"Try clicking the circle button with the price on a menu item to add it to your order!");

        private HelpStruct dishDetails = new HelpStruct(
            "Dish Building Help",
            @"Click the expanders and build your meal! When ready, hit confirm and it will be added to your order");

        private HelpStruct billSplitting = new HelpStruct(
            "Bill Overview Help",
            @"Here you can split your bill, or just confirm you're ready to pay. To split your bill, tap the circle that you want to represent your bill and then select items to add them to your tab");

        private HelpStruct OrderConfirm = new HelpStruct(
            "Confirm Order Submit",
            @"You're about to send your order to the kitchen! Press cancel to go back and review it, or confirm to continue");

        private HelpStruct CannotPay = new HelpStruct(
            "Cannot Submit Bill for Payment Yet",
            @"You still have items in your Current Order that have not yet been sent to the kitchen. Your bill cannot be processed for payment until all of your Current Order items have been sent to the kitchen. Press either Submit Order, or remove all your items from your Current Order first before pressing Ready to Pay.");

        private HelpStruct NothingToSubmit = new HelpStruct(
            "No Items to Submit!",
            @"There are no items in the Current Order to submit. Please select some dishes first.");

        private IEventAggregator events;
        public BPDialogViewModel(IEventAggregator _events)
        {
            this.events = _events;
            events.Subscribe(this);
        }

        public void SetPage(string activeItem)
        {
            switch (activeItem)
            {
                case "MainMenuViewModel":
                    this.Active = this.menuPane;
                    RightButtonText = "Got it";
                    ShowCancel = false;
                    break;
                case "DishDetailsViewModel":
                    this.Active = this.dishDetails;
                    RightButtonText = "Got it";
                    ShowCancel = false;
                    break;
                case "BillSplittingViewModel":
                    this.Active = this.billSplitting;
                    RightButtonText = "Got it";
                    ShowCancel = false;
                    break;
                case "OrderConfirm":
                    this.Active = this.OrderConfirm;
                    RightButtonText = "Confirm";
                    ShowCancel = true;
                    break;
                case "CannotPay":
                    this.Active = this.CannotPay;
                    RightButtonText = "Got it";
                    ShowCancel = false;
                    break;
                case "NothingToSubmit":
                    this.Active = this.NothingToSubmit;
                    RightButtonText = "Got it";
                    ShowCancel = false;
                    break;
                default:
                    this.Active = this.menuPane;
                    break;
            }
        }

        public void ConfirmDialog()
        {
            if (RightButtonText == "Confirm")
            {
                events.PublishOnBackgroundThread(new OrderConfirmedEvent());
            }           
        }
    }
}
