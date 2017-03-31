using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
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

        private NutritionInfo nutrition;
        public NutritionInfo Nutrition
        {
            get { return nutrition; }
            set
            {
                nutrition = value;
                NotifyOfPropertyChange(() => Nutrition);
            }
        }

        public DishDetailsViewModel(IEventAggregator events, BPMenuItemViewModel ci)
        {
            this.DisplayName = "DishDetailsViewModel";
            _events = events;
            item = ci;
            Nutrition =
                new NutritionInfo
                {
                    ServingSize = 465,
                    Calories = 1110,
                    TotalFat = 77,
                    SaturatedFat = 28,
                    TransFat = 2,
                    Cholesterol = 220,
                    Sodium = 1520,
                    Carbohydrates = 48,
                    Protein = 53,
                    DietaryFibre = 3,
                    Sugars = 9,
                    VitaminA = 25,
                    VitaminC = 15,
                    Calcium = 30,
                    Iron = 35
                };
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

        // END RADIO BUTTONS

      

        public void handleDipSelected(String selectedDip)
        {   
            if (selectedDip == null)
            {
                setAppropriateLabel("", "Extras");
            }
            else
            {
                setAppropriateLabel(" - (" + selectedDip + " Selected)", "Extras");
            } 
        }

        public void handleCheckboxAndDips (String checkbox, String name, String header )
        {
            string labelTitle = null;
            if (checkbox == "True" && name != null)
            {
                if(header == "Extras")
                {
                    _selectedExtras.Add(name);
                }
                if(header == "Allergies")
                {
                    _selectedAllergies.Add(name);
                }
                if(header == "Bill")
                {
                    _selectedBills.Add(name);
                }
            }

            if (checkbox == "False")
            {
                if (header == "Extras")
                {
                    _selectedExtras.Remove(name);
                }
                if (header == "Allergies")
                {
                    _selectedAllergies.Remove(name);
                }
                if (header == "Bill")
                {
                    _selectedBills.Remove(name);
                }
            }

            if(_selectedExtras.Count > 1 && header == "Extras")
            {
                labelTitle = " - (" + _selectedExtras.ToArray()[0] + " Selected & More)";
            }
            else if(_selectedAllergies.Count > 1 && header == "Allergies")
            {
                labelTitle = " - (" + _selectedAllergies.ToArray()[0] + " Selected & More)";
            }
            else if(_selectedBills.Count > 1 && header == "Bill")
            {
                labelTitle = " - (" + _selectedBills.ToArray()[0] + " Selected & More)";
            }
            else
            {
                if (_selectedExtras.Count == 0 && header == "Extras")
                {
                    labelTitle = "";
                }
                else if (_selectedAllergies.Count == 0 && header == "Allergies")
                {
                    labelTitle = "";
                }
                else if (_selectedBills.Count == 0 && header == "Bill")
                {
                    labelTitle = "";
                }
                else
                {
                    if(header == "Extras")
                    {
                        if (_selectedExtras.Contains(name) == false)
                        {
                            labelTitle = " - (" + _selectedExtras.ToArray()[0] + " Selected)";
                        }
                        else if (name == "" || name == null)
                        {
                            labelTitle = "";
                        }
                        else
                        {
                            labelTitle = " - (" + name + " Selected)";
                        } 
                    }

                    if (header == "Allergies")
                    {
                        if (_selectedAllergies.Contains(name) == false)
                        {
                            labelTitle = " - (" + _selectedAllergies.ToArray()[0] + " Selected)";
                        }
                        else
                        {
                            labelTitle = " - (" + name + " Selected)";
                        }
                    }

                    if (header == "Bill")
                    {
                        if (_selectedBills.Contains(name) == false)
                        {
                            labelTitle = " - (" + _selectedBills.ToArray()[0] + " Selected)";
                        }
                        else
                        {
                            labelTitle = " - (" + name + " Selected)";
                        }
                    }
                }                
            }
            //if (checkbox == "False")
            //{
            //    setAppropriateLabel("", header);
            //}
            //else
            //{
            setAppropriateLabel(labelTitle, header);
            //}
        }

        // Handle any changes to the Special Order Instructions Text box
        public void handleSOIRichTextBoxChange(string text)
        {
            string truncatedString;
            string labelTitle;

            truncatedString = text.Length <= 30 ? text : text.Substring(0, 30) + "...";
            labelTitle = " - (" + truncatedString + ")";

            setAppropriateLabel(labelTitle, "SOI");
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
                //if(ExtrasHeader != "Extras" && ExtrasHeader != null)
                //{
                //    if (ExtrasHeader.Contains(" & More") == false)
                //    {
                //        ExtrasHeader = ExtrasHeader + " & More";
                //    }                                     
                //}
                //else
                //{
                    ExtrasHeader = sectionHeader + labelTitle;
                //}
                
            }

            // If the section header == Allergies Header then check if the section header has 
            // already been set, if it's been set,  and it contains the & More string, 
            if(sectionHeader + "Header" == "AllergiesHeader")
            {
                //if (AllergiesHeader != "Allergies" && AllergiesHeader != null)
                //{
                //    if(AllergiesHeader.Contains(" & More") == false && labelTitle != null)
                //    {
                //        AllergiesHeader = AllergiesHeader + " & More";
                //    }
                //    if(AllergiesHeader.Contains(labelTitle) == false)
                //    {
                //        AllergiesHeader = sectionHeader + labelTitle;
                //    }                    
                //}
                //else
                //{
                    AllergiesHeader = sectionHeader + labelTitle;
                //}                
            }

            if (sectionHeader + "Header" == "SOIHeader")
            {
                SOIHeader = "Special Order Instructions" + labelTitle;
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

        private List<string> _selectedExtras = new List<string>();
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

        private List<string> _selectedAllergies = new List<string>();
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

        private List<string> _selectedBills = new List<string>();
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


        public BindableCollection<string> DipOptions
        {
            get
            {
                // Collection of Dip Options
                return new BindableCollection<string>(
                                 new string[] { "None", "Creamy Alfreado", "Blue Cheese", "Bolognese" });
            }
        }

        private String _selectedDip;
        public String AddDipComboBox
        {
            get { return _selectedDip; }
            set
            {
                string previousChoice = _selectedDip;
                _selectedDip = value;
                NotifyOfPropertyChange(() => AddDipComboBox);

                // Remove the previous choice from the _selectedExtras list
                _selectedExtras.Remove(previousChoice);
                handleCheckboxAndDips("True", _selectedDip, "Extras");
            }
        }

        private String _GravyCheckbox;
        public String GravyCheckbox
        {
            get { return _GravyCheckbox; }
            set
            {
                _GravyCheckbox = value;
                NotifyOfPropertyChange(() => GravyCheckbox);

                handleCheckboxAndDips(_GravyCheckbox, "Gravy", "Extras");          
            }
        }

        private String _CheeseCheckbox;
        public String CheeseCheckbox
        {
            get { return _CheeseCheckbox; }
            set
            {
                _CheeseCheckbox = value;
                NotifyOfPropertyChange(() => CheeseCheckbox);

                handleCheckboxAndDips(_CheeseCheckbox, "Cheese", "Extras");
            }
        }

        private String _BaconCheckbox;
        public String BaconCheckbox
        {
            get { return _BaconCheckbox; }
            set
            {
                _BaconCheckbox = value;
                NotifyOfPropertyChange(() => BaconCheckbox);

                handleCheckboxAndDips(_BaconCheckbox, "Bacon", "Extras");
            }
        }

        private String _NutAllergiesCheckbox;
        public String NutAllergiesCheckbox
        {
            get { return _NutAllergiesCheckbox; }
            set
            {
                _NutAllergiesCheckbox = value;
                NotifyOfPropertyChange(() => NutAllergiesCheckbox);

                handleCheckboxAndDips(_NutAllergiesCheckbox, "Nut Allergy", "Allergies");
            }
        }

        private String _ShellfishAllergiesCheckbox;
        public String ShellfishAllergiesCheckbox
        {
            get { return _ShellfishAllergiesCheckbox; }
            set
            {
                _ShellfishAllergiesCheckbox = value;
                NotifyOfPropertyChange(() => ShellfishAllergiesCheckbox);

                handleCheckboxAndDips(_ShellfishAllergiesCheckbox, "Shellfish Allergy", "Allergies");
            }
        }

        private String _DairyAllergiesCheckbox;
        public String DairyAllergiesCheckbox
        {
            get { return _DairyAllergiesCheckbox; }
            set
            {
                _DairyAllergiesCheckbox = value;
                NotifyOfPropertyChange(() => DairyAllergiesCheckbox);

                handleCheckboxAndDips(_DairyAllergiesCheckbox, "Dairy Allergy", "Allergies");
            }
        }

        private String _GlutenAllergiesCheckbox;
        public String GlutenAllergiesCheckbox
        {
            get { return _GlutenAllergiesCheckbox; }
            set
            {
                _GlutenAllergiesCheckbox = value;
                NotifyOfPropertyChange(() => GlutenAllergiesCheckbox);

                handleCheckboxAndDips(_GlutenAllergiesCheckbox, "Gluten Allergy", "Allergies");
            }
        }

        private string _SOITextBox;
        public string SOITextBox
        {
            get { return _SOITextBox; }
            set
            {
                _SOITextBox = value;
                NotifyOfPropertyChange(() => SOITextBox);

                handleSOIRichTextBoxChange(_SOITextBox);
            }
        }

        private String _BillCheckbox1;
        public String BillCheckbox1
        {
            get { return _BillCheckbox1; }
            set
            {
                _BillCheckbox1 = value;
                NotifyOfPropertyChange(() => BillCheckbox1);

                handleCheckboxAndDips(_BillCheckbox1, "Bill 1", "Bill");
            }
        }

        private String _BillCheckbox2;
        public String BillCheckbox2
        {
            get { return _BillCheckbox2; }
            set
            {
                _BillCheckbox2 = value;
                NotifyOfPropertyChange(() => BillCheckbox2);

                handleCheckboxAndDips(_BillCheckbox2, "Bill 2", "Bill");
            }
        }

        private String _BillCheckbox3;
        public String BillCheckbox3
        {
            get { return _BillCheckbox3; }
            set
            {
                _BillCheckbox3 = value;
                NotifyOfPropertyChange(() => BillCheckbox3);

                handleCheckboxAndDips(_BillCheckbox3, "Bill 3", "Bill");
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
