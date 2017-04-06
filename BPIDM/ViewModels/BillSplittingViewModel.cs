using BPIDM.Events;
using BPIDM.Models;
using BPIDM.Utils;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System;
using System.Linq;

namespace BPIDM.ViewModels
{
    class BillSplittingViewModel : Screen,
        IHandle<AddItemToBillEvent>, IHandle<GetBillInformationEvent>, IHandle<AddBillEvent>
    {
        // all possible bill colors, randomly shuffled so it isn't a gradient
        public static List<string> BillColors;

        // start with four Bills
        public ICollectionView BillCollection { get; set; }
        private BindableCollection<Bill> _BillList;
        public BindableCollection<Bill> BillList
        {
            get { return _BillList; }
            set
            {
                _BillList = value;
                NotifyOfPropertyChange(() => BillList);
            }
        }

        public ICollectionView OrderCollection { get; set; }
        public BindableCollection<BPOrderItemViewModel> OrderContent { get; private set; }

        private BPOrderItemViewModel selectedModel;
        public BPOrderItemViewModel SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                NotifyOfPropertyChange(() => SelectedModel);
            }
        }

        private IEventAggregator _events;
        public BillSplittingViewModel(IEventAggregator events)
        {
            this.DisplayName = "BillSplittingViewModel";
            this._events = events;

            // missing: Red (app color), Indigo (app color), Grey (weird bug?)
            BillColors = new List<string>{
                "Pink", "Purple", "DeepPurple", "Blue", "LightBlue",
                "Cyan", "Teal", "Green", "LightGreen", "Lime", "Yellow",
                "Amber", "Orange", "DeepOrange", "Brown", "BlueGrey"
            }.Shuffle();
            BillList = new BindableCollection<Bill>{
                new Bill(BillColors[0], "Bill 1"), new Bill(BillColors[1], "Bill 2"),
                new Bill(BillColors[2], "Bill 3"), new Bill(BillColors[3], "Bill 4")
            };
            OrderContent = new BindableCollection<BPOrderItemViewModel>();
            OrderCollection = CollectionViewSource.GetDefaultView(OrderContent);
            events.Subscribe(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void Cancel()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }

        public void Pay()
        {
            //Check if the user has any items in their Current Order, if so, show a dialog informing them
            //that they cannot submit their bill until the Current Order list is empty
            if (FooterViewModel.OrderContent.Count > 0)
            {
                _events.PublishOnUIThread(new ShowHelpEvent("CannotPay"));
            }
            else
            {
                _events.PublishOnUIThread(new TestEvent("BACK"));
            }
        }

        public void AddBill()
        {
            if (BillList.Count >= 15) return;
            BillList.Add(new Bill(BillColors[BillList.Count], "Bill " + (BillList.Count + 1)));
        }

        public void Handle(AddItemToBillEvent message)
        {
            OrderContent.Add(message.item);
            foreach (string b in message.item.BillsSelected.Values)
            {
                Bill temp = BillList.First(s => s.BillName == b);
                temp.Items.Add(message.item);
                temp.AddToCost(message.item.price / message.item.BillsSelected.Count);
            }
        }

        public void Handle(GetBillInformationEvent message)
        {
            _events.PublishOnUIThread(new SendBillInformationEvent(BillList.Count, BillColors));
        }

        public void Handle(AddBillEvent message)
        {
            AddBill();
        }
    }
}
