using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

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



        //public ICommand rbnChecked
        //{
        //    get;
        //    set;
        //}

        //public enum Options
        //{
        //    Fries
        //   ,Yam_Fries
        //   ,Cactus_Cut_Potatoes
        //   ,Caesar_Salad
        //   ,House_Salad
        //   ,Mediterranean_Salad
        //   ,Spinach_Salad
        //   ,Soup_of_the_Day
        //   ,Broccoli_Cheese
        //   ,French_Onion
        //   ,Potato_Bacon
        //   ,None

        //}

        //private Options options = Options.Fries;

        //public Options Options
        //{
        //    get { return options; }
        //    set { options = value; NotifyPropertyChanged("Options"); }
        //}

        //[ValueConversion(typeof(Enum), typeof(bool))]
        //public class EnumToBoolConverter : IValueConverter
        //{
        //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if (value == null || parameter == null) return false;
        //        string enumValue = value.ToString();
        //        string targetValue = parameter.ToString();
        //        bool outputValue = enumValue.Equals(targetValue, StringComparison.InvariantCultureIgnoreCase);
        //        return outputValue;
        //    }

        //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        //    {
        //        if (value == null || parameter == null) return null;
        //        bool useValue = (bool)value;
        //        string targetValue = parameter.ToString();
        //        if (useValue) return Enum.Parse(targetType, targetValue);
        //        return null;
        //    }
        //}

        public void fries_rbn(object context)
        {
            setAppropriateLabel(" - (Fries Selected)", "Sides");
        }

        public void yam_fries_rbn(object context)
        {
            setAppropriateLabel(" - (Yam Fries Selected)", "Sides");
        }

        public void cactus_cuts_rbn(object context)
        {
            setAppropriateLabel(" - (Cactus Cut Potatoes Selected)", "Sides");
        }

        public void salad_caesar_rbn(object context)
        {
            setAppropriateLabel(" - (Caesar Salad Selected)", "Sides");
        }

        public void salad_house_rbn(object context)
        {
            setAppropriateLabel(" - (House Salad Selected)", "Sides");
        }

        public void salad_med_rbn(object context)
        {
            setAppropriateLabel(" - (Mediterranean Salad Selected)", "Sides");
        }

        public void salad_spinach_rbn(object context)
        {
            setAppropriateLabel(" - (Spinach Salad Selected)", "Sides");
        }

        public void soup_rbn(object context)
        {
            setAppropriateLabel(" - (Soup of the Day Selected)", "Sides");
        }

        public void soup_bc_rbn(object context)
        {
            setAppropriateLabel(" - (Broccoli Cheese Selected)", "Sides");
        }

        public void soup_fo_rbn(object context)
        {
            setAppropriateLabel(" - (French Onion Selected)", "Sides");
        }

        public void soup_pb_rbn(object context)
        {
            setAppropriateLabel(" - (Potato Bacon Selected)", "Sides");
        }

        public void none_rbn(object context)
        {
            setAppropriateLabel(" - (None Selected)", "Sides");
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

            if (sectionHeader + "Header" == "SOIHeader")
            {
                SOIHeader = sectionHeader + labelTitle;
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

        private string _SOIHeader;
        public string SOIHeader
        {
            get { return _SOIHeader; }
            set
            {
                _SOIHeader = value;
                NotifyOfPropertyChange(() => SOIHeader);
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





        //private List<string> inputString = new List<string>();
        //public List<string> InputString
        //{
        //    get
        //    {
        //        return inputString;
        //    }
        //    set
        //    {
        //        inputString = value;
        //        NotifyOfPropertyChange("InputString");
        //    }
        //}

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
