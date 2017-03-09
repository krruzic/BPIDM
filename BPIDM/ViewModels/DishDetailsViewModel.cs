using BPIDM.Events;
using Caliburn.Micro;

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

        protected override void OnActivate()
        {
            base.OnActivate();
            string ts = "Dish Building Help";
            string hs = "Click the expanders and build your meal! When ready, hit confirm and it will be added to your order";
            _events.PublishOnBackgroundThread(new HelpEvent(ts, hs));
        }

        public void closeDetails()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
        public void ConfirmSelection()
        {
            _events.PublishOnBackgroundThread(new ItemConfirmedEvent(item));
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
