using System;
using System.Windows.Controls;
using BPIDM.Controls;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Media;

namespace BPIDM.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl, ISwitchable
    {
        public ObservableCollection<BPMenuItem> MenuContent { get; private set; }
        public MainMenu()
        {
            InitializeComponent();
            MenuContent = new ObservableCollection<BPMenuItem>();
            for(int i = 0; i<10; i++)
            {                BPMenuItem item = new BPMenuItem
                {
                    DisplayedName = "Test Item " + i,
                    DisplayedPrice = 2 * i + 0.99,
                    DisplayedImage = (ImageSource)new ImageSourceConverter().ConvertFromString(@"C:\Users\krruz\Desktop\bpchicken.png"),
                };
                MenuContent.Add(item);
            }
            this.DataContext = this;
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

    }
}
