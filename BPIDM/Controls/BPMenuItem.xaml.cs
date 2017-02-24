using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register("DisplayedCategory", typeof(String), typeof(BPMenuItem));

        public BPMenuItem()
        {
            InitializeComponent();
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

        public string DisplayedCategory
        {
            get { return (string)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }
    }
}
