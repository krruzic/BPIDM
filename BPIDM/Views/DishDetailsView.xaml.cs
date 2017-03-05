using System.Windows;
using System.Windows.Controls;

namespace BPIDM.Views
{
    /// <summary>
    /// Interaction logic for DishDetailsView.xaml
    /// </summary>
    public partial class DishDetailsView : UserControl
    {
        public DishDetailsView()
        {
            InitializeComponent();
        }

        // Button click event to show the nutrition of a menu item and hide the description

        private void nutr_button_Click(object sender, RoutedEventArgs e)
        {
            item_description.Visibility = Visibility.Hidden;
            nutrition_data_grid.Visibility = Visibility.Visible;
            var data = new Nutrition
            {
                ServingSize = "465",
                Calories = "1110",
                TotalFat = "77",
                SaturatedFat = "28",
                TransFat = "2",
                Cholesterol = "220",
                Sodium = "1520",
                Carbohydrates = "48",
                Protein = "53",
                DietaryFibre = "3",
                Surgars = "9",
                VitaminA = "25",
                VitaminC = "15",
                Calcium = "30",
                Iron = "35"
            };
            nutrition_data_grid.Items.Add(data);
        }

        private void close_details_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    internal class Nutrition
    {
        public string Calcium { get; set; }
        public string Calories { get; set; }
        public string Carbohydrates { get; set; }
        public string Cholesterol { get; set; }
        public string DietaryFibre { get; set; }
        public string Iron { get; set; }
        public string Protein { get; set; }
        public string SaturatedFat { get; set; }
        public string ServingSize { get; set; }
        public string Sodium { get; set; }
        public string Surgars { get; set; }
        public string TotalFat { get; set; }
        public string TransFat { get; set; }
        public string VitaminA { get; set; }
        public string VitaminC { get; set; }
    }
}
