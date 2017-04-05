using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace BPIDM.ViewModels
{
    class DrinkDetailsViewModel : Screen
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

        public void CloseDetails()
        {
            TryClose();
        }

        public void ConfirmSelection()
        {
            _events.PublishOnBackgroundThread(new ItemConfirmedEvent(item));
            TryClose();            
        }
    }
}
