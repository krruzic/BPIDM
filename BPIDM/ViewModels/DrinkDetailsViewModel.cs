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
        public DrinkDetailsViewModel(IEventAggregator events, BPMenuItemViewModel ci)
        {
            this.DisplayName = "DrinkDetailsViewModel";
            _events = events;
            item = ci;
        }

        private readonly IEventAggregator _events;
        private BPMenuItemViewModel _item;
        public BPMenuItemViewModel item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => item);
            }
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
