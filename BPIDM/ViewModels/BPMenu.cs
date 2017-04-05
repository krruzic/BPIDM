using BPIDM.Events;
using BPIDM.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using Utils;


// TODO: convert these into normal models and handle the property changes elsewhere!
namespace BPIDM.ViewModels
{
    public class NutritionInfo
    {
        private int _Calcium;
        public int Calcium { get; internal set; }

        private int _Calories;
        public int Calories { get; internal set; }

        private int _Carbohydrates;
        public int Carbohydrates { get; internal set; }

        private int _Cholesterol;
        public int Cholesterol { get; internal set; }

        private int _DietaryFibre;
        public int DietaryFibre { get; internal set; }

        private int _Iron;
        public int Iron { get; internal set; }

        private int _Protein;
        public int Protein { get; internal set; }

        private int _SaturatedFat;
        public int SaturatedFat { get; internal set; }

        private int _ServingSize;
        public int ServingSize { get; internal set; }

        private int _Sodium;
        public int Sodium { get; internal set; }

        private int _Sugars;
        public int Sugars { get; internal set; }

        private int _TotalFat;
        public int TotalFat { get; internal set; }

        private int _TransFat;
        public int TransFat { get; internal set; }

        private int _VitaminA;
        public int VitaminA { get; internal set; }

        private int _VitaminC;
        public int VitaminC { get; internal set; }
    }
    public class RootMenuObject
    {
        private List<dynamic> _Menu;
        public List<dynamic> Menu {
            get { return _Menu; }
            set
            {
                _Menu = value;
            }
        }
    }

    public class BPCategoryItemViewModel : PropertyChangedBase
    {
        public BPCategoryItemViewModel(dynamic cat)
        {
            this.CategoryName = cat.CategoryName;
            this.CategoryName = CategoryName.ToUpper();
            this.Image = cat.Image;
            this.Description = cat.Description;
            this.widthPercent = 0.7;
            this.heightPercent = 0.25;
        }

        private double _widthPercent;
        public double widthPercent
        {
            get { return _widthPercent; }
            set
            {
                _widthPercent = value;
                NotifyOfPropertyChange(() => widthPercent);
            }
        }

        private double _heightPercent;
        public double heightPercent
        {
            get { return _heightPercent; }
            set
            {
                _heightPercent = value;
                NotifyOfPropertyChange(() => heightPercent);
            }
        }

        private string _CategoryName;
        public string CategoryName {
            get { return _CategoryName; }
            set
            {
                _CategoryName = value;
                NotifyOfPropertyChange(() => CategoryName);
            }
        }

        private string _Description;
        public string Description {
            get { return _Description; }
            set
            {
                _Description = value;
                NotifyOfPropertyChange(() => Description);
            }
        }

        private string _Image;
        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                NotifyOfPropertyChange(() => Image);
            }
        }

        private List<BPMenuItemViewModel> _Content;
        public List<BPMenuItemViewModel> Content {
            get { return _Content; }
            set
            {
                _Content = value;
                NotifyOfPropertyChange(() => Content);
            }
        }

        public override string ToString()
        {
            return this.CategoryName;
        }
    }

    public abstract class BPBaseItemViewModel : PropertyChangedBase
    {
        public BPBaseItemViewModel() { }

        private IEventAggregator _events;
        public IEventAggregator events { get { return _events; } set { _events = value; } }

        private double _widthPercent;
        public double widthPercent
        {
            get { return _widthPercent; }
            set
            {
                _widthPercent = value;
                NotifyOfPropertyChange(() => widthPercent);
            }
        }

        private double _heightPercent;
        public double heightPercent
        {
            get { return _heightPercent; }
            set
            {
                _heightPercent = value;
                NotifyOfPropertyChange(() => heightPercent);
            }
        }

        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyOfPropertyChange(() => description);
            }
        }

        private bool _show_quick_add;
        public bool show_quick_add
        {
            get { return _show_quick_add; }
            set
            {
                _show_quick_add = value;
                NotifyOfPropertyChange(() => show_quick_add);
            }
        }

        private string _image;
        public string image
        {
            get { return _image; }
            set
            {
                _image = value;
                NotifyOfPropertyChange(() => image);
            }
        }

        private List<String> _tags;
        public List<string> tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                NotifyOfPropertyChange(() => tags);
            }
        }

        private List<string> _tagImages;
        public List<string> tagImages
        {
            get { return _tagImages; }
            set
            {
                _tagImages = value;
                NotifyOfPropertyChange(() => tagImages);
            }
        }

        private List<string> _option_groups;
        public List<string> option_groups
        {
            get { return _option_groups; }
            set
            {
                _option_groups = value;
                NotifyOfPropertyChange(() => option_groups);
            }
        }

        private bool _show_customize;
        public bool show_customize
        {
            get { return _show_customize; }
            set
            {
                _show_customize = value;
                NotifyOfPropertyChange(() => show_customize);
            }
        }

        private double _price;
        public double price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => price);
            }
        }

        private double _default_pricing;
        public double default_pricing
        {
            get { return _default_pricing; }
            set
            {
                _default_pricing = value;
                NotifyOfPropertyChange(() => default_pricing);
            }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => title);
            }
        }

        private int _quantity;
        public int quantity {
            get { return _quantity; }
            set
            {
                _quantity = value;
                NotifyOfPropertyChange(() => quantity);
            }
        }

        private NutritionInfo _nutrition_info;
        public NutritionInfo nutrition_info {
            get { return _nutrition_info; }
            set
            {
                _nutrition_info = value;
                NotifyOfPropertyChange(() => nutrition_info);
            }
        }

        private bool _upsell;
        public bool upsell {
            get { return _upsell; }
            set
            {
                _upsell = value;
                NotifyOfPropertyChange(() => upsell);
            }
        }

        private string _id;
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyOfPropertyChange(() => id);
            }
        }

        private string _category;
        public string category
        {
            get { return _category; }
            set
            {
                _category = value;
                NotifyOfPropertyChange(() => category);
            }
        }

        private bool _isVisible;
        public bool isVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                NotifyOfPropertyChange(() => isVisible);
            }
        }

        public override string ToString()
        {
            return this.title;
        }
    }

    public class BPMenuItemViewModel : BPBaseItemViewModel
    {
        public BPMenuItemViewModel(dynamic item, IEventAggregator events)
        {
            this.title = item.title;
            this.image = item.image;
            this.description = item.description;
            this.tags = (List<string>)item.tags.ToObject(typeof(List<string>));
            this.tagImages = (List<string>)item.tagImages.ToObject(typeof(List<string>));
            this.tagImages.Sort();
            try
            {
                this.price = item.retail_pricing;
            }
            catch
            {
                this.price = 4.99;
            }
            this.category = item.category;
            this.isVisible = true;
            this.widthPercent = 0.22;
            this.heightPercent = 0.7;
            this.events = events;

            this.category = category.ToUpper();
            this.title = title.ToUpper();
        }

        public void ShowDish()
        {
            events.PublishOnUIThread(new DishDetailEvent(new BPOrderItemViewModel(this, events)));
        }
    }

    public class BPOrderItemViewModel : BPBaseItemViewModel
    {
        public BPOrderItemViewModel()
        {
            this.widthPercent = 0.3;
            this.heightPercent = 0.85;
        }

        public BPOrderItemViewModel(BPMenuItemViewModel item, IEventAggregator events)
        {
            this.title = item.title;
            this.image = item.image;
            this.description = item.description;
            this.tags = item.tags;
            this.tagImages = item.tagImages;
            this.price = item.price;

            this.category = item.category;
            this.isVisible = true;
            this.widthPercent = 0.3;
            this.heightPercent = 0.85;
            this.events = item.events;
            BillsSelected = new ObservableDictionary<string, string>();
        }

        // just store the color of the bill, we can handle any lookup 
        // later
        private ObservableDictionary<string,string> _billsSelected;
        public ObservableDictionary<string, string> BillsSelected
        {
            get { return _billsSelected; }
            set
            {
                _billsSelected = value;
                NotifyOfPropertyChange(() => BillsSelected);
            }
        }

        private string _sidesSelected;
        public string sidesSelected
        {
            get { return _sidesSelected; }
            set
            {
                _sidesSelected = value;
                NotifyOfPropertyChange(() => sidesSelected);
            }
        }

        private string _extrasSelected;
        public string extrasSelected
        {
            get { return _extrasSelected; }
            set
            {
                _extrasSelected = value;
                NotifyOfPropertyChange(() => extrasSelected);
            }
        }

        private string _specialInstructions;
        public string specialInstructions
        {
            get { return _specialInstructions; }
            set
            {
                _specialInstructions = value;
                NotifyOfPropertyChange(() => specialInstructions);
            }
        }
    }
}