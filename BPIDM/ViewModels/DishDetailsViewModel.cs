using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BPIDM.ViewModels
{
    class DishDetailsViewModel : Screen
    {
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

        public DishDetailsViewModel(IEventAggregator events, BPMenuItemViewModel ci)
        {
            this.DisplayName = "DishDetailsViewModel";
            _events = events;
            item = ci;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void SetSideHeader(RoutedEventArgs val)
        {
            RadioButton r = (RadioButton) val.Source;
            SidesHeader = "Sides - " + r.Content.ToString() + " Selected";
        }

        public void SetExtrasHeader(RoutedEventArgs val)
        {
            RadioButton r = (RadioButton)val.Source;
            ExtrasHeader = "Extras - " + r.Content.ToString() + " Selected";
        }

        // Method to set the Header of the corresponding expander
        public void setAppropriateLabel(String labelTitle, String sectionHeader)
        {
            if(sectionHeader + "Header" == "SidesHeader")
            {
                SidesHeader = sectionHeader + labelTitle;
            }

            if(sectionHeader + "Header" == "ExtrasHeader")
            {
                ExtrasHeader = sectionHeader + labelTitle;
            }

            if(sectionHeader + "Header" == "AllergiesHeader")
            {
                AllergiesHeader = sectionHeader + labelTitle;
            }

            if (sectionHeader + "Header" == "BillHeader")
            {
                BillHeader = sectionHeader + labelTitle;
            }

        }

        private string _SidesHeader;
        public string SidesHeader
        {
            get { return _SidesHeader; }
            set
            {
                _SidesHeader = value;
                NotifyOfPropertyChange(() => SidesHeader);
            }
        }

        private string _ExtrasHeader;
        public string ExtrasHeader
        {
            get { return _ExtrasHeader; }
            set
            {
                _ExtrasHeader = value;
                NotifyOfPropertyChange(() => ExtrasHeader);
            }
        }

        private string _AllergiesHeader;
        public string AllergiesHeader
        {
            get { return _AllergiesHeader; }
            set
            {
                _AllergiesHeader = value;
                NotifyOfPropertyChange(() => AllergiesHeader);
            }
        }

        private string _BillHeader;
        public string BillHeader
        {
            get { return _BillHeader; }
            set
            {
                _BillHeader = value;
                NotifyOfPropertyChange(() => BillHeader);
            }
        }

        private string _SpecialText;
        public string SpecialText
        {
            get { return _SpecialText; }
            set
            {
                _SpecialText = value;
                NotifyOfPropertyChange(() => SpecialText);
            }
        }

        public void closeDetails()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }

        public void ConfirmSelection()
        {
            _events.PublishOnBackgroundThread(new ItemConfirmedEvent(item));
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
