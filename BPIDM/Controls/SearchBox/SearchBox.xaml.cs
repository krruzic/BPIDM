using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BPIDM.Controls
{
    /// <summary>
    /// Interaction logic for SearchBox.xaml
    /// </summary>
    public partial class SearchBox : UserControl, INotifyPropertyChanged
    {
        private static DependencyProperty SearchTextProperty =
                DependencyProperty.Register("SearchText", typeof(string), typeof(SearchBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault |
                        FrameworkPropertyMetadataOptions.Inherits));

        public string SearchText
        {
            get { return (string)GetValue(SearchTextProperty); }
            set { SetValue(SearchTextProperty, value); }
        }

        public SearchBox()
        {
            InitializeComponent(); 
        }

        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var t = (TextBox)sender;
            t.SelectAll();
        }

        private void SearchTextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            var t = (TextBox)sender;
            t.SelectAll();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            OnSearchEvent();
        }

        public static readonly RoutedEvent SearchEvent = EventManager.RegisterRoutedEvent(
                "Search", // Event name
                RoutingStrategy.Bubble, // Bubble means the event will bubble up through the tree
                typeof(RoutedEventHandler), // The event type
                typeof(SearchBox)
            ); // Belongs to ChildControlBase

        // Allows add and remove of event handlers to handle the custom event
        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key.Equals(Key.Enter))
            {
                OnSearchEvent();
            } 
        }

        private void OnSearchEvent()
        {
            SearchText = SearchTextBox.Text;
            var newEventArgs = new RoutedEventArgs(SearchBox.SearchEvent);
            RaiseEvent(newEventArgs);
        }
    }
}