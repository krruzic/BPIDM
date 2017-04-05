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
    class DishDetailsViewModel : Screen, IHandle<SendBillInformationEvent>
    {
        public ObservableCollection<Nutrition> NutritionInfos { get; set; }

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

        public static List<string> BillColors;
        private BindableCollection<string> _bills;
        public BindableCollection<string> Bills
        {
            get { return _bills; }
            set
            {
                _bills = value;
                NotifyOfPropertyChange(() => Bills);
            }
        }

        private BindableCollection<string> _dipOptions;
        public BindableCollection<string> DipOptions
        {
            get { return _dipOptions; }
            set
            {
                _dipOptions = value;
                NotifyOfPropertyChange(() => DipOptions);
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

        public List<string> SelectedExtras = new List<string>();
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

        public List<string> SelectedAllergies = new List<string>();
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

        //private Dictionary<string, string> SelectedBills = new Dictionary<string, string>();
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

        private readonly IEventAggregator _events;
        public DishDetailsViewModel(IEventAggregator events, BPOrderItemViewModel ci)
        {
            this.DisplayName = "DishDetailsViewModel";
            _events = events;
            _events.Subscribe(this);
            item = ci;
            Bills = new BindableCollection<string>();
            BillColors = new List<string>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            _events.PublishOnBackgroundThread(new GetBillInformationEvent());

            NutritionInfos = new ObservableCollection<Nutrition>
            {
                new Nutrition {
                    ServingSize = 465, Calories = 1110, TotalFat = 77,
                    SaturatedFat = 28, TransFat = 2, Cholesterol = 220,
                    Sodium = 1520, Carbohydrates = 48, Protein = 53,
                    DietaryFibre = 3, Sugars = 9, VitaminA = 25,
                    VitaminC = 15, Calcium = 30, Iron = 35
                }
            };
            DipOptions = new BindableCollection<string>(new string[] { "None", "Creamy Alfredo", "Blue Cheese", "Bolognese" });
        }

        internal class Nutrition
        {
            public int ServingSize { get; set; }
            public int Calories { get; set; }
            public int TotalFat { get; set; }
            public int SaturatedFat { get; set; }
            public int TransFat { get; set; }
            public int Cholesterol { get; set; }
            public int Sodium { get; set; }
            public int Carbohydrates { get; set; }
            public int Protein { get; set; }
            public int DietaryFibre { get; set; }
            public int Sugars { get; set; }
            public int VitaminA { get; set; }
            public int VitaminC { get; set; }
            public int Calcium { get; set; }
            public int Iron { get; set; }
        }

        public void SetSideHeader(RoutedEventArgs val)
        {
            RadioButton r = (RadioButton)val.Source;
            SidesHeader = "Sides - " + r.Content.ToString() + " Selected";
        }

        public void SetExtrasHeader(RoutedEventArgs val)
        {
            if (val.RoutedEvent.Name == "SelectionChanged")
            {
                SelectedExtras.RemoveAll(x => x.StartsWith("Dip"));
                if (((ComboBox)val.Source).SelectedValue.ToString() != "None")
                    SelectedExtras.Add("Dip (" + ((ComboBox)val.Source).SelectedValue.ToString() + ")");
            }
            else
            {
                CheckBox c = (CheckBox)val.Source;
                if (c.IsChecked.Value)
                    SelectedExtras.Add(c.Content.ToString());
                else
                    SelectedExtras.Remove(c.Content.ToString());
            }
            if (SelectedExtras.Count == 0)
                ExtrasHeader = "Extras";
            else if (SelectedExtras.Count == 1)
                ExtrasHeader = "Extras - " + SelectedExtras[0] + " Selected";
            else // multiple
                ExtrasHeader = "Extras - " + SelectedExtras[0] + " Selected & More";
        }

        public void SetAllergiesHeader(RoutedEventArgs val)
        {
            CheckBox c = (CheckBox)val.Source;
            if (c.IsChecked.Value)
                SelectedAllergies.Add(c.Content.ToString());
            else
                SelectedAllergies.Remove(c.Content.ToString());
            if (SelectedAllergies.Count == 0)
                AllergiesHeader = "Allergies";
            else if (SelectedAllergies.Count == 1)
                AllergiesHeader = "Allergies - " + SelectedAllergies[0] + " Selected";
            else // multiple
                AllergiesHeader = "Allergies - " + SelectedAllergies[0] + " Selected & More";

        }

        public void SetBills(RoutedEventArgs val)
        {
            CheckBox c = (CheckBox)val.Source;
            int index = Int32.Parse(c.Content.ToString().Split(' ')[1]);

            string actualColor = ((SolidColorBrush)Application.Current.Resources["BillForeground" + BillColors[index]]).Color.ToString();
            if (c.IsChecked.Value)
            {
                item.BillsSelected[actualColor] = c.Content.ToString();
            }
            else
                item.BillsSelected.Remove(actualColor);

        }

        public void SOIChanged(RoutedEventArgs val)
        {
            string truncatedString = ((TextBox)val.Source).Text;
            truncatedString = truncatedString.Length <= 30 ? truncatedString : truncatedString.Substring(0, 30) + "...";
            string labelTitle = " - (" + truncatedString + ")";
            SOIHeader = "Special Order Instructions" + labelTitle;
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

        public void AddBill()
        {
            if (Bills.Count >= 15) return;
            _events.PublishOnBackgroundThread(new AddBillEvent());
            Bills.Add("Bill " + (Bills.Count + 1));
        }

        public void Handle(SendBillInformationEvent message)
        {
            if (Bills.Count != 0) return;
            for (int i = 0; i < message.TotalBills; i++)
            {
                Bills.Add("Bill " + (i + 1));
            }
            BillColors = message.BillColors;
        }
    }
}
