using BPIDM.ViewModels;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace BPIDM.Models
{
    class Bill : PropertyChangedBase
    {
        private float totalCost = 4.00F;
        public float TotalCost
        {
            get { return totalCost; }
            set {
                totalCost = value;
                NotifyOfPropertyChange(() => TotalCost);
            }
        }

        private List<BPOrderItemViewModel> items = new List<BPOrderItemViewModel>();
        private List<BPOrderItemViewModel> Items
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

        public Bill(string color)
        {
            BillColor = color;
            BillBackgroundBrush = (SolidColorBrush)Application.Current.Resources["BillBackground" + color];
            BillForegroundBrush = (SolidColorBrush)Application.Current.Resources["BillForeground" + color];
        }

        public void AddToCost(float a)
        {
            TotalCost += a;
        }
    }
}
