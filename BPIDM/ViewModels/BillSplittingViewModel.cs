using BPIDM.Events;
using Caliburn.Micro;

namespace BPIDM.ViewModels
{
    class BillSplittingViewModel : Screen
    {
        private IEventAggregator _events;

        public BillSplittingViewModel(IEventAggregator events)
        {
            this.DisplayName = "BillSplittingViewModel";
            this._events = events;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        public void Pay()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
