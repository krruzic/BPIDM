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
            this.AllowFlagServer = true;
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
        private bool _AllowFlagServer;
        public bool AllowFlagServer
        {
            get { return _AllowFlagServer; }
            set {
                _AllowFlagServer = value;
                NotifyOfPropertyChange(() => AllowFlagServer);
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
            AllowFlagServer = !AllowFlagServer;
            if (FlagText == this.ServerName + " Flagged!")
            {
                FlagText = "Flag " + this.ServerName;
            }
            else
            {
                FlagText = this.ServerName + " Flagged!";
            }
        }

        public void ViewBill()
        {
            _events.PublishOnUIThread(new TestEvent("BILL"));
        }

        public void Help()
        {
            _events.PublishOnUIThread(new ShowHelpEvent());
        }
    }
}
