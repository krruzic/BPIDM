using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BPIDM
{
    public class NutritionInfo
    {
    }
    public class RootMenuObject
    {        private List<BPCategoryViewModel> _Menu;
        public List<BPCategoryViewModel> Menu {
            get { return _Menu; }
            set
            {
                _Menu = value;
            }
        }
    }

    public class BPCategoryViewModel : INotifyPropertyChanged
    {
        private string _CategoryName;
        public string CategoryName {
            get { return _CategoryName; }
            set
            {
                _CategoryName = value;
                RaisePropertyChanged("CategoryName");
            }
        }

        private string _Description;
        public string Description {
            get { return _Description; }
            set
            {
                _Description = value;
                RaisePropertyChanged("Description");
            }
        }

        private List<BPMenuViewModel> _Content;
        public List<BPMenuViewModel> Content {
            get { return _Content; }
            set
            {
                _Content = value;
                RaisePropertyChanged("Content");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public class BPMenuViewModel : INotifyPropertyChanged
    {
        private string _description;
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged("description");
            }
        }

        private bool _show_quick_add;
        public bool show_quick_add
        {
            get { return _show_quick_add; }
            set
            {
                _show_quick_add = value;
                RaisePropertyChanged("show_quick_add");
            }
        }

        private string _image;
        public string image
        {
            get { return _image; }
            set
            {
                _image = value;
                RaisePropertyChanged("image");
            }
        }

        private List<String> _tags;
        public List<string> tags
        {
            get { return _tags; }
            set
            {
                _tags = value;
                RaisePropertyChanged("tags");
            }
        }

        private List<String> _option_groups;
        public List<string> option_groups
        {
            get { return _option_groups; }
            set
            {
                _option_groups = value;
                RaisePropertyChanged("option_groups");
            }
        }

        private bool _show_customize;
        public bool show_customize
        {
            get { return _show_customize; }
            set
            {
                _show_customize = value;
                RaisePropertyChanged("show_customize");
            }
        }

        private double _retail_pricing;
        public double retail_pricing
        {
            get { return _retail_pricing; }
            set
            {
                _retail_pricing = value;
                RaisePropertyChanged("retail_pricing");
            }
        }

        private double _default_pricing;
        public double default_pricing
        {
            get { return _default_pricing; }
            set
            {
                _default_pricing = value;
                RaisePropertyChanged("default_pricing");
            }
        }

        private string _title;
        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("title");
            }
        }

        private int _quantity;
        public int quantity {
            get { return _quantity; }
            set
            {
                _quantity = value;
                RaisePropertyChanged("quantity");
            }
        }

        private NutritionInfo _nutrition_info;
        public NutritionInfo nutrition_info {
            get { return _nutrition_info; }
            set
            {
                _nutrition_info = value;
                RaisePropertyChanged("nutrition_info");
            }
        }

        private bool _upsell;
        public bool upsell {
            get { return _upsell; }
            set
            {
                _upsell = value;
                RaisePropertyChanged("upsell");
            }
        }

        private string _id;
        public string id
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("nutrition_info");
            }
        }

        private string _category;
        public string category {
            get { return _category; }
            set
            {
                _category = value;
                RaisePropertyChanged("category");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}