using BPIDM.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPIDM.ViewModels
{
    class DishDetailsViewModel : Screen
    {
        private readonly IEventAggregator _events;
        public DishDetailsViewModel(IEventAggregator events)
        {
            _events = events;
        }
        public void closeDetails()
        {
            _events.PublishOnUIThread(new TestEvent("BACK"));
        }
    }
}
