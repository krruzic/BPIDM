using System.Windows;

namespace BPIDM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Application.Current.Properties["menuJSON"] = "";
            InitializeComponent();
        }
    }
}
