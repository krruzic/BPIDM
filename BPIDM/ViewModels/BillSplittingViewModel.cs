using BPIDM.Events;
using BPIDM.Models;
using BPIDM.Utils;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace BPIDM.ViewModels
{
    class BillSplittingViewModel : Screen, IHandle<AddItemToBillEvent>
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
        private BindableCollection<BPOrderItemViewModel> _OrderList;
        public BindableCollection<BPOrderItemViewModel> OrderList
        {
            get { return _OrderList; }
            set
            {
                _OrderList = value;
                NotifyOfPropertyChange(() => OrderList);
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
                new Bill(BillColors[0]), new Bill(BillColors[1]),
                new Bill(BillColors[2]), new Bill(BillColors[3])
            };
            OrderList = new BindableCollection<BPOrderItemViewModel>();
            OrderCollection = CollectionViewSource.GetDefaultView(OrderList);
            events.Subscribe(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void Pay()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }

        public void AddBill()
        {
            BillList.Add(new Bill(BillColors[BillList.Count]));
        }

        public void Handle(AddItemToBillEvent message)
        {
            System.Console.WriteLine("Event Handled!");
            OrderList.Add(message.item);
        }
    }
}
