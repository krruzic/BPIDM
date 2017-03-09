using BPIDM.Events;
using Caliburn.Micro;

namespace BPIDM.ViewModels
{
    class BillSplittingViewModel : Screen
    {
        private IEventAggregator _events;

        public BillSplittingViewModel(IEventAggregator _events)
        {
            this._events = _events;
        }

        public void Pay()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
