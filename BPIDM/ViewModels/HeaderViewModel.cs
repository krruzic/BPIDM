using BPIDM.Events;
using Caliburn.Micro;

namespace BPIDM.ViewModels
{
    class HeaderViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        public HeaderViewModel(IEventAggregator events)
        {
            _events = events;
            this.ServerName = "Trisha";
            this.FlagText = "Flag "+this.ServerName;
        }

        private string _ServerName;
        public string ServerName
        {
            get { return _ServerName; }
            set
            {
                _ServerName = value;
                NotifyOfPropertyChange(() => ServerName);
            }
        }

        private string _FlagText;
        public string FlagText
        {
            get { return _FlagText; }
            set
            {
                _FlagText = value;
                NotifyOfPropertyChange(() => FlagText);
            }
        }

        public void FlagServer()
        {
            if (FlagText == this.ServerName + " Flagged!")
                FlagText = "Flag "+this.ServerName;
            else
                FlagText = this.ServerName+" Flagged!";
        }

        public void ViewBill()
        {
            _events.PublishOnUIThread(new TestEvent("BILL"));
        }
    }
}
