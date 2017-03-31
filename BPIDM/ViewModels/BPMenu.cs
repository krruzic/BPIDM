using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;


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

    public class BPMenuItemViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
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
            } catch
            {
                this.price = 4.99;
            }
            this.category = item.category;
            this.isVisible = true;
            this.widthPercent = 0.22;
            this.heightPercent = 0.7;
            this._events = events;

            this.category = category.ToUpper();
            this.title = title.ToUpper();
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

        private List<String> _tagImages;
        public List<string> tagImages
        {
            get { return _tagImages; }
            set
            {
                _tagImages = value;
                NotifyOfPropertyChange(() => tagImages);
            }
        }

        private List<String> _option_groups;
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

        public void ShowDish()
        {
            _events.PublishOnUIThread(new DishDetailEvent(this));
        }
    }

    public class BPOrderItemViewModel : PropertyChangedBase
    {
        public BPOrderItemViewModel(BPMenuItemViewModel item)
        {
            this.title = item.title;
            this.image = item.image;
            this.description = item.description;
            this.price = item.price;
            this.widthPercent = 0.1;
            this.heightPercent = 0.9;
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
    }
}