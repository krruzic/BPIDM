using BPIDM.Events;
using Caliburn.Micro;
using System.Threading.Tasks;

namespace BPIDM.ViewModels
{
    class DishDetailsViewModel : Screen
    {
        private readonly IEventAggregator _events;
        private BPMenuViewModel _item;
        public BPMenuViewModel item
        {
            get { return _item; }
            set
            {
                _item = value;
                NotifyOfPropertyChange(() => item);
            }
        } 

        public DishDetailsViewModel(IEventAggregator events, BPMenuViewModel ci)
        {
            _events = events;
            item = ci;
        }

        public void closeDetails()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
        public async Task ConfirmSelection()
        {
            _events.PublishOnBackgroundThread(new ItemConfirmedEvent(item));
            _events.PublishOnBackgroundThread(new TestEvent("BACK"));
            await Task.Delay(1);
        }
    }
}
