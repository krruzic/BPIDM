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

        // confusing but:
        // key is item, value is number of bills this item is shared across.
        // this way we can update the cost of the bill easily 
        private Dictionary<BPOrderItemViewModel, int> items = new Dictionary<BPOrderItemViewModel, int>();
        public Dictionary<BPOrderItemViewModel, int> Items
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

        public void AddItem(BPOrderItemViewModel item, int billSplitAcross)
        {
            if (Items.ContainsKey(item))
            {
                RemoveItem(item);
            }
            Items.Add(item, billSplitAcross);
            TotalCost += (item.price / billSplitAcross);
        }

        public void RemoveItem(BPOrderItemViewModel item)
        {
            int div = Items[item];
            Items.Remove(item);
            TotalCost -= (item.price / div);
        }
    }
}
