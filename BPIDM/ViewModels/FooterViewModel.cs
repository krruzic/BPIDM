using BPIDM.Events;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace BPIDM.ViewModels
{
    class FooterViewModel : PropertyChangedBase, IHandle<ItemConfirmedEvent>, IHandle<OrderConfirmedEvent>
    {
        public CollectionViewSource OrderCollection { get; set; }
        public BindableCollection<BPOrderItemViewModel> OrderContent { get; private set; }

        private BPOrderItemViewModel selectedModel;
        public BPOrderItemViewModel SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                NotifyOfPropertyChange(() => SelectedModel);
            }
        }

        private readonly IEventAggregator _events;
        public FooterViewModel(IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            OrderContent = new BindableCollection<BPOrderItemViewModel>();
            OrderCollection = new CollectionViewSource();
            OrderCollection.Source = this.OrderContent;
        }

        public bool CanSubmitOrder
        {
            get
            {
                return (OrderContent.Count > 0) ? true : false;
            }
        }

        public void SubmitOrder()
        {
            _events.PublishOnUIThread(new ShowHelpEvent("OrderConfirm"));
        }

        public void Handle(OrderConfirmedEvent message)
        {
            foreach (BPOrderItemViewModel item in OrderContent)
            {
                _events.PublishOnBackgroundThread(new AddItemToBillEvent(item));
            }
            OrderContent.Clear();
        }

        public void Handle(ItemConfirmedEvent message)
        {
            OrderContent.Add((BPOrderItemViewModel)(BPBaseItemViewModel) message.item);
            NotifyOfPropertyChange(() => CanSubmitOrder);
        }

        public void RemoveItem()
        {
            OrderContent.Remove(SelectedModel);
        }

        public void TriggerEdit(BPOrderItemViewModel dataContext)
        {
            System.Console.WriteLine("TEST");
        }

        public void ScrollLeft(UserControl view)
        {
            ListView OrderList = (ListView)view.FindName("OrderList");
            ScrollViewer sv = VisualTreeHelperEx.FindDescendantByType<ScrollViewer>(OrderList);
            sv.PageLeft();
        }

        public void ScrollRight(UserControl view)
        {
            ListView OrderList = (ListView)view.FindName("OrderList");
            ScrollViewer sv = VisualTreeHelperEx.FindDescendantByType<ScrollViewer>(OrderList);
            sv.PageRight();
        }
    }
}

