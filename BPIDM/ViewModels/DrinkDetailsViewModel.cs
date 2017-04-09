using BPIDM.Events;
using BPIDM.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BPIDM.ViewModels
{
    class DrinkDetailsViewModel : Screen, IHandle<SendBillInformationEvent>
    {
        private double InitialPrice;
        public DrinkDetailsViewModel(IEventAggregator events, BPOrderItemViewModel ci)
        {
            this.DisplayName = "DrinkDetailsViewModel";
            _events = events;
            Amount = 1;            
            item = ci;
            Price = item.price;
            InitialPrice = item.price;
            _events.Subscribe(this);
            _events.PublishOnBackgroundThread(new GetBillInformationEvent());

            Bills = new BindableCollection<BillCheck>();
            BillColors = new List<string>();
        }

        private readonly IEventAggregator _events;
        private BPOrderItemViewModel _item;
        public BPOrderItemViewModel item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => item);
            }
        }

        private int _Amount;
        public int Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }

        private double _Price;
        public static List<string> BillColors;
        private BindableCollection<BillCheck> _bills;
        public BindableCollection<BillCheck> Bills
        {
            get { return _bills; }
            set
            {
                _bills = value;
                NotifyOfPropertyChange(() => Bills);
            }
        }
        public double Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                NotifyOfPropertyChange(() => Price);
            }
        }

        public void ChangePrice(int amt, double InitialPrice)
        {
            Price = amt * InitialPrice;
        }

        public void AmountMinus()
        {
            if(Amount != 1)
            {
                Amount -= 1;
                ChangePrice(Amount, InitialPrice);
            }            
        }
        public void AmountPlus()
        {
            Amount += 1;
            ChangePrice(Amount, InitialPrice);
        }

        public bool CanAddBill
        {
            get
            {
                return (Bills.Count < 15) ? true : false;
            }
        }

        public void AddBill()
        {
            if (Bills.Count >= 15) return;
            _events.PublishOnBackgroundThread(new AddBillEvent());
            Bills.Add(new BillCheck("Bill " + (Bills.Count + 1)));
            NotifyOfPropertyChange(() => CanAddBill);
        }

        public void CloseDetails()
        {
            TryClose();
        }

        public void ConfirmSelection()
        {
            int i = Amount;
            while (i > 0)
            {
                _events.PublishOnBackgroundThread(new ItemConfirmedEvent(item));
                i--;
            }
            TryClose();            
        }

        public void SetBills(RoutedEventArgs val)
        {
            CheckBox c = (CheckBox)val.Source;
            int index = Int32.Parse(c.Content.ToString().Split(' ')[1]);

            string actualColor = ((SolidColorBrush)Application.Current.Resources["BillForeground" + BillColors[index - 1]]).Color.ToString();
            if (c.IsChecked.Value)
            {
                item.BillsSelected[actualColor] = c.Content.ToString();
            }
            else
                item.BillsSelected.Remove(actualColor);
        }

        public void Handle(SendBillInformationEvent message)
        {
            if (Bills.Count != 0) return;
            for (int i = 0; i < message.TotalBills; i++)
            {
                BillCheck temp = new BillCheck("Bill " + (i + 1));
                if (i == 0)
                    temp.Checked = true;
                Bills.Add(temp);
            }
            NotifyOfPropertyChange(() => CanAddBill);
            BillColors = message.BillColors;
            // first check box needs to be checked to begin with
            string actualColor = ((SolidColorBrush)Application.Current.Resources["BillForeground" + BillColors[0]]).Color.ToString();
            item.BillsSelected[actualColor] = "Bill 1";
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            _events.Unsubscribe(this);
        }
    }
}
