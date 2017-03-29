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
            this.Backgroundcolor = "#304FFE";
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
        private string _Backgroundcolor;
        public string Backgroundcolor
        {
            get { return _Backgroundcolor; }
            set {
                _Backgroundcolor = value;
                NotifyOfPropertyChange(() => Backgroundcolor);
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
                Backgroundcolor = "#304FFE";
            }
            else
            {
                FlagText = this.ServerName + " Flagged!";
                Backgroundcolor = "#FFEE30";
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
