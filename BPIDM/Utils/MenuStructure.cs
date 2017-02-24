
using System.Collections.Generic;

namespace BPIDM.Utils { 
    public class NutritionInfo
    {
    }

    public class Content
    {
        public string description { get; set; }
        public bool show_quick_add { get; set; }
        public string image { get; set; }
        public List<string> tags { get; set; }
        public List<string> option_groups { get; set; }
        public bool show_customize { get; set; }
        public double retail_pricing { get; set; }
        public double default_pricing { get; set; }
        public string title { get; set; }
        public int quantity { get; set; }
        public NutritionInfo nutrition_info { get; set; }
        public bool upsell { get; set; }
        public string id { get; set; }
    }

    public class Menu
    {
        public string CategoryName { get; set; }
        public List<Content> Content { get; set; }
    }

    public class RootMenuObject
    {
        public List<Menu> Menu { get; set; }
    }
}