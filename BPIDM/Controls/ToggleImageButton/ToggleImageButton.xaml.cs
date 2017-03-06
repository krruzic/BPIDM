using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace BPIDM.Controls
{
    /// <summary>
    /// Interaction logic for ToggleImageButton.xaml
    /// </summary>
    public partial class ToggleImageButton : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static DependencyProperty ClickedImageProperty =
            DependencyProperty.Register("ClickedImage", typeof(ImageSource), typeof(ToggleImageButton));

        private static DependencyProperty NonImageProperty =
            DependencyProperty.Register("NonImage", typeof(ImageSource), typeof(ToggleImageButton));

        private static DependencyProperty HighlightColorProperty =
            DependencyProperty.Register("HighlightColor", typeof(Brush), typeof(ToggleImageButton));

        public ImageSource ClickedImage
        {
            get { return (ImageSource)GetValue(ClickedImageProperty); }
            set { SetValue(ClickedImageProperty, value); }
        }

        public ImageSource NonImage
        {
            get { return (ImageSource)GetValue(NonImageProperty); }
            set { SetValue(NonImageProperty, value); }
        }

        private ImageSource _ActiveImage;
        public ImageSource ActiveImage
        {
            get { return _ActiveImage; }
            set
            {
                _ActiveImage = value;
                OnPropertyChanged("ActiveImage");
            }
        }

        public Brush HightlightColor
        {
            get { return (Brush)GetValue(HighlightColorProperty); }
            set { SetValue(HighlightColorProperty, value); }
        }

        public ToggleImageButton()
        {
            InitializeComponent();
            ActiveImage = NonImage;
        }

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            OnImageEvent();
        }

        private void Reset_Circle(object sender, MouseEventArgs e)
        {
            Shadow.Visibility = Visibility.Hidden;
        }

        private void Highlight_Circle(object sender, MouseEventArgs e)
        {
            Shadow.Visibility = Visibility.Visible;
        }

        public static readonly RoutedEvent ImageEvent = EventManager.RegisterRoutedEvent(
            "ImageClicked", // Event name
            RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
            typeof(RoutedEventHandler), // The event type
            typeof(ToggleImageButton)
        ); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event RoutedEventHandler ImageClicked
        {
            add { AddHandler(ImageEvent, value); }
            remove { RemoveHandler(ImageEvent, value); }
        }

        private void OnImageEvent()
        {
            if (ActiveImage == ClickedImage)
            {
                ImageRect.OpacityMask.SetCurrentValue(ImageBrush.ImageSourceProperty, NonImage);
                ActiveImage = NonImage;
            }
            else
            {
                ImageRect.OpacityMask.SetCurrentValue(ImageBrush.ImageSourceProperty, ClickedImage);
                ActiveImage = ClickedImage;
            }
            var newEventArgs = new RoutedEventArgs(ToggleImageButton.ImageEvent);
            RaiseEvent(newEventArgs);
        }
    }
}
