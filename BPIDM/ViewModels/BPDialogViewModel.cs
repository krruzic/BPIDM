using System;
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

        private HelpStruct menuPane = new HelpStruct(
            "Menu Overview Help",
            "Try clicking the circle button with the price on a menu item to add it to your order!");

        private HelpStruct dishDetails = new HelpStruct(
            "Dish Building Help",
            "Click the expanders and build your meal! When ready, hit confirm and it will be added to your order");

        private HelpStruct billSplitting = new HelpStruct(
            "Bill Overview Help",
            "Here you can split your bill, or just confirm you're ready to pay. To split your bill, tap the circle that you want to represent your bill and then select items to add them to your tab");

        private IEventAggregator events;
        public BPDialogViewModel(IEventAggregator _events)
        {
            this.events = _events;
            events.Subscribe(this);
        }

        public void setPage(string activeItem)
        {
            switch (activeItem)
            {
                case "MainMenuViewModel":
                    this.Active = this.menuPane;
                    break;
                case "DishDetailsViewModel":
                    this.Active = this.dishDetails;
                    break;
                case "BillSplittingViewModel":
                    this.Active = this.billSplitting;
                    break;
                default:
                    this.Active = this.menuPane;
                    break;
            }
        }
    }
}
