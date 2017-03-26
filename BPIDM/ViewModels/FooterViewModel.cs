using BPIDM.Events;
using Caliburn.Micro;
using System.Windows.Data;

namespace BPIDM.ViewModels
{
    class FooterViewModel : PropertyChangedBase, IHandle<ItemConfirmedEvent>
    {
        private readonly IEventAggregator _events;

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

        public FooterViewModel(IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            OrderContent = new BindableCollection<BPOrderItemViewModel>();
            OrderCollection = new CollectionViewSource();
            OrderCollection.Source = this.OrderContent;
        }

        public void SubmitOrder()
        {
            OrderContent.Clear();
        }

        public void Handle(ItemConfirmedEvent message)
        {
            OrderContent.Add(new BPOrderItemViewModel(message.item));
        }

        public void RemoveItem()
        {
            OrderContent.Remove(SelectedModel);
        }
    }
}
