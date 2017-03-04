using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace BPIDM.ViewModels
{
    [Export(typeof(HeaderViewModel))]
    class HeaderViewModel : PropertyChangedBase
    {
        private readonly IEventAggregator _events;
        [ImportingConstructor]
        public HeaderViewModel(IEventAggregator events)
        {
            _events = events;
            this.FlagText = "Flag";
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
            if (FlagText == "Flagged!")
                FlagText = "Flag";
            else
                FlagText = "Flagged!";
        }
    }
}
