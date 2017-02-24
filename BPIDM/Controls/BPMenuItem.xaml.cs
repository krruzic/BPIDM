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

namespace BPIDM.Controls
{
    /// <summary>
    /// Interaction logic for BPMenuItem.xaml
    /// </summary>
    public partial class BPMenuItem : UserControl
    {
        public static readonly new DependencyProperty NameProperty = DependencyProperty.Register("DisplayedName", typeof(String), typeof(BPMenuItem));
        public static readonly DependencyProperty PriceProperty = DependencyProperty.Register("DisplayedPrice", typeof(double), typeof(BPMenuItem));
        public static readonly DependencyProperty ImageProperty = DependencyProperty.Register("DisplayedImage", typeof(ImageSource), typeof(BPMenuItem));
        public BPMenuItem()
        {
            InitializeComponent();
        }

        public BPMenuItem(string n, double p, String i)
        {
            InitializeComponent();
            this.DisplayedName = n;
            this.DisplayedPrice = p;
            this.DisplayedImage = (ImageSource) new ImageSourceConverter().ConvertFromString(i);
        }

        public double DisplayedPrice
        {
            get { return (double) GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }

        public string DisplayedName
        {
            get { return (string) GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }
        public ImageSource DisplayedImage
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
