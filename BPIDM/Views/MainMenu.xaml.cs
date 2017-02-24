using System;
using System.IO;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Media;

using Newtonsoft.Json;

using BPIDM.Controls;
using BPIDM.Utils;

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
            FillList();
            this.DataContext = this;
        }

        public void FillList()
        {
            string json = LoadJson();
            dynamic jsonObj = JsonConvert.DeserializeObject<RootMenuObject>(json);
            foreach (var cat in jsonObj.Menu)
            {
                foreach (var item in cat.Content)
                {
                    BPMenuItem bpitem = new BPMenuItem
                    { 
                        DisplayedName = item.title,
                        DisplayedPrice = item.retail_pricing,
                        DisplayedImage = (ImageSource)new ImageSourceConverter().ConvertFromString(@"C:\Users\krruz\Pictures\smartcrop\65-spaghetti.png"),
                        DisplayedCategory = cat.CategoryName,
                    };
                    MenuContent.Add(bpitem);
                }
            }
        }

        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        public string LoadJson()
        {
            using (StreamReader r = new StreamReader(@"Data/menu.json"))
            {
               return r.ReadToEnd();
            }
        }
    }
}
