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
            this.Foregroundcolor = "#DDFFFFFF";
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
        private string _Foregroundcolor;
        public string Foregroundcolor
        {
            get { return _Foregroundcolor; }
            set {
                _Foregroundcolor = value;
                NotifyOfPropertyChange(() => Foregroundcolor);
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
            {
                FlagText = "Flag " + this.ServerName;
                Foregroundcolor = "#DDFFFFFF";
            }
            else
            {
                FlagText = this.ServerName + " Flagged!";
                Foregroundcolor = "#FFEB3B";
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
