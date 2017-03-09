using BPIDM.Events;
using Caliburn.Micro;

namespace BPIDM.ViewModels
{
    class BPDialogViewModel : PropertyChangedBase, IHandle<HelpEvent>
    {
        private string helpText;
        public string HelpText
        {
            get { return helpText; }
            set
            {
                helpText = value;
                NotifyOfPropertyChange(() => HelpText);
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private IEventAggregator events;
        public BPDialogViewModel(IEventAggregator _events)
        {
            this.events = _events;
            events.Subscribe(this);
        }

        public void Handle(HelpEvent message)
        {
            this.Title = message.Title;
            this.HelpText = message.HelpText;
        }
    }
}
