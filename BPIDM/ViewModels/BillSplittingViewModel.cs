using BPIDM.Events;
using BPIDM.Models;
using BPIDM.Utils;
using Caliburn.Micro;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace BPIDM.ViewModels
{
    class BillSplittingViewModel : Screen,
        IHandle<AddItemToBillEvent>, IHandle<GetBillInformationEvent>, IHandle<AddBillEvent>
    {
        // all possible bill colors, randomly shuffled so it isn't a gradient
        public List<string> BillColors;

        // add/remove bills on select in the view
        public Dictionary<string, string> SelectedBills;

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

            SelectedBills = new Dictionary<string, string>();

            OrderContent = new BindableCollection<BPOrderItemViewModel>();
            OrderCollection = CollectionViewSource.GetDefaultView(OrderContent);
            OrderCollection.GroupDescriptions.Add(new PropertyGroupDescription("category"));
            events.Subscribe(this);
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void Cancel()
        {
            SelectedBills.Clear();
            _events.PublishOnUIThread(new NavigationEvent("BACK"));
        }

        public bool CanPay
        {
            get
            {
                return (OrderContent.Count > 0) ? true : false;
            }
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
                OrderContent.Clear();
                _events.PublishOnUIThread(new NavigationEvent("BACK"));
            }
        }

        public bool CanAddBill
        {
            get
            {
                return (BillList.Count < 15) ? true : false;
            }
        }

        public void AddBill()
        {
            BillList.Add(new Bill(BillColors[BillList.Count], "Bill " + (BillList.Count + 1)));
            NotifyOfPropertyChange(() => CanAddBill);
        }

        public void Handle(GetBillInformationEvent message)
        {
            _events.PublishOnUIThread(new SendBillInformationEvent(BillList.Count, BillColors));
        }

        public void Handle(AddBillEvent message)
        {
            AddBill();
        }

        public void Handle(AddItemToBillEvent message)
        {
            message.item.widthPercent = 0.22;
            message.item.heightPercent = 0.7;
            OrderContent.Add(message.item);
            NotifyOfPropertyChange(() => CanPay);
            AddItemToBills(message.item);
        }

        private void AddItemToBills(BPOrderItemViewModel item)
        {
            foreach (string b in item.BillsSelected.Values)
            {
                Bill temp = BillList.First(s => s.BillName == b);
                temp.AddItem(item, item.BillsSelected.Count);
            }
        }

        private void RemoveItemFromBill(BPOrderItemViewModel selectedModel, Bill bill)
        {
            bill.RemoveItem(selectedModel);
        }

        public void BillSelect(RoutedEventArgs val)
        {
            ToggleButton t = (ToggleButton)val.Source;
            string keyToFind = ((SolidColorBrush)t.Foreground).Color.ToString();
            if ((bool)t.IsChecked)
                SelectedBills.Add(keyToFind, (string)t.Tag);
            else
                SelectedBills.Remove(keyToFind);
        }

        public void ModifyBills()
        {
            int originalNumBillAttached = SelectedModel.BillsSelected.Count;
            foreach (KeyValuePair<string, string> entry in SelectedBills)
            {
                if (!SelectedModel.BillsSelected.ContainsKey(entry.Key))
                    SelectedModel.BillsSelected.Add(entry.Key, entry.Value);
                else
                {
                    SelectedModel.BillsSelected.Remove(entry.Key);
                    RemoveItemFromBill(SelectedModel, BillList.First(s => s.BillName == entry.Value));
                }
            }
            AddItemToBills(SelectedModel);
        }
    }
}
