using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BPIDM.Views
{
    /// <summary>
    /// Interaction logic for DishDetailsView.xaml
    /// </summary>
    public partial class DishDetailsView : Page
    {
        public DishDetailsView()
        {
            InitializeComponent();
        }

        private void close_details_Click(object sender, RoutedEventArgs e)
        {

        }

        // button click event to show the description of a menu item and hide the nutrition
        private void desc_button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        // Button click event to show the nutrition of a menu item and hide the description
        private void nutr_button_Click(object sender, RoutedEventArgs e)
        {

            dish_description.Visibility = Visibility.Hidden;
            nutrition_data_grid.Visibility = Visibility.Visible;
            var data = new Nutrition
            {
                 ServingSize = "465"
                ,Calories = "1110"
                ,TotalFat = "77"
                ,SaturatedFat = "28"
                ,TransFat = "2"
                ,Cholesterol = "220"
                ,Sodium = "1520"
                ,Carbohydrates = "48"
                ,Protein = "53"
                ,DietaryFibre = "3"
                ,Surgars = "9"
                ,VitaminA = "25"
                ,VitaminC = "15"
                ,Calcium = "30"
                ,Iron = "35"
            };
            nutrition_data_grid.Items.Add(data);
        }

        public class Nutrition {
            public string ServingSize { get; set; }
            public string Calories { get; set; }
            public string TotalFat { get; set; }
            public string SaturatedFat { get; set; }
            public string TransFat { get; set; }
            public string Cholesterol { get; set; }
            public string Sodium { get; set; }
            public string Carbohydrates { get; set; }
            public string Protein { get; set; }
            public string DietaryFibre { get; set; }
            public string Surgars { get; set; }
            public string VitaminA { get; set; }
            public string VitaminC { get; set; }
            public string Calcium { get; set; }
            public string Iron { get; set; }

        }

        
    }
}
