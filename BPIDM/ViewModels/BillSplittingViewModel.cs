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

        protected override void OnActivate()
        {
            base.OnActivate();
            string ts = "Bill Overview Help";
            string hs = "Here you can split your bill, or just confirm you're ready to pay. To split your bill, tap the circle that you want to represent your bill and then select items to add them to your tab";
            _events.PublishOnBackgroundThread(new HelpEvent(ts, hs));
        }

        public void Pay()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
