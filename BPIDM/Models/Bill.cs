using BPIDM.ViewModels;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace BPIDM.Models
{
    public class Bill : PropertyChangedBase
    {
        private double totalCost = 0.00;
        public double TotalCost
        {
            get { return totalCost; }
            set
            {
                totalCost = value;
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        private List<BPOrderItemViewModel> items = new List<BPOrderItemViewModel>();
        public List<BPOrderItemViewModel> Items
        {
            get { return items; }
            set
            {
                items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        private string billColor;
        public string BillColor
        {
            get { return billColor; }
            set
            {
                billColor = value;
                NotifyOfPropertyChange(() => BillColor);
            }
        }

        private SolidColorBrush billBackgroundBrush;
        public SolidColorBrush BillBackgroundBrush
        {
            get { return billBackgroundBrush; }
            set
            {
                billBackgroundBrush = value;
                NotifyOfPropertyChange(() => BillBackgroundBrush);
            }
        }

        private SolidColorBrush billForegroundBrush;
        public SolidColorBrush BillForegroundBrush
        {
            get { return billForegroundBrush; }
            set
            {
                billForegroundBrush = value;
                NotifyOfPropertyChange(() => BillForegroundBrush);
            }
        }

        private string billName;
        public string BillName
        {
            get { return billName; }
            set
            {
                billName = value;
                NotifyOfPropertyChange(() => BillName);

            }
        }

        public Bill(string color, string name)
        {
            BillColor = color;
            BillBackgroundBrush = (SolidColorBrush)Application.Current.Resources["BillBackground" + color];
            BillForegroundBrush = (SolidColorBrush)Application.Current.Resources["BillForeground" + color];
            BillName = name;
        }

        public void AddToCost(double a)
        {
            TotalCost += a;
        }
    }
}
