using BPIDM.Events;
using Caliburn.Micro;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.Core.Utilities;

namespace BPIDM.ViewModels
{
    class FooterViewModel : PropertyChangedBase, IHandle<ItemConfirmedEvent>, IHandle<OrderConfirmedEvent>
    {
        public CollectionViewSource OrderCollection { get; set; }
        public static BindableCollection<BPOrderItemViewModel> OrderContent { get; private set; }

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
            OrderCollection.Source = OrderContent;
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
            if (OrderContent.Count < 1)
            {
                _events.PublishOnUIThread(new ShowHelpEvent("NothingToSubmit"));
            }
            else
            {
                _events.PublishOnUIThread(new ShowHelpEvent("OrderConfirm"));
            }
        }

        public void Handle(OrderConfirmedEvent message)
        {
            foreach (BPOrderItemViewModel item in OrderContent)
            {
                _events.PublishOnBackgroundThread(new AddItemToBillEvent(item));
            }
            OrderContent.Clear();
            NotifyOfPropertyChange(() => CanSubmitOrder);

            // change to bill view, user then knows what they should do next
            _events.PublishOnUIThread(new NavigationEvent("BILL"));
        }

        public void Handle(ItemConfirmedEvent message)
        {
            OrderContent.Add((BPOrderItemViewModel)(BPBaseItemViewModel)message.item);
            NotifyOfPropertyChange(() => CanSubmitOrder);
        }

        public void EditItem()
        {
            _events.PublishOnUIThread(new DishDetailEvent(selectedModel));
        }

        public void RemoveItem()
        {
            OrderContent.Remove(SelectedModel);
            // If the last item in the Current Order object list is removed, set the Submit Order button to be disabled
            if (OrderContent.Count < 1)
            {
                NotifyOfPropertyChange(() => CanSubmitOrder);
            }
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

