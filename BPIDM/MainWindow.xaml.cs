using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BPIDM.Controls;
namespace BPIDM
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            // Required to initialize variables
            InitializeComponent();
        }
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //Switcher.Switch(new MainMenu());
        }
        #endregion
    }
}