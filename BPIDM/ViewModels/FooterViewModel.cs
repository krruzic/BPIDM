using BPIDM.Events;
using Caliburn.Micro;
using System.Windows.Data;

namespace BPIDM.ViewModels
{
    class FooterViewModel : IHandle<ItemConfirmedEvent>
    {
        private readonly IEventAggregator _events;

        public CollectionViewSource OrderCollection { get; set; }
        public BindableCollection<BPOrderViewModel> OrderContent { get; private set; }

        public FooterViewModel(IEventAggregator events)
        {
            _events = events;
            events.Subscribe(this);
            OrderContent = new BindableCollection<BPOrderViewModel>();
            OrderCollection = new CollectionViewSource();
            OrderCollection.Source = this.OrderContent;
        }

        public void SubmitOrder()
        {
            OrderContent.Clear();
        }

        public void Handle(ItemConfirmedEvent message)
        {
            OrderContent.Add(new BPOrderViewModel(message.item));
        }
    }
}
