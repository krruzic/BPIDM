using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

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
