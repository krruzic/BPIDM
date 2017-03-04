using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class ItemConfirmedEvent
    {
        public BPMenuViewModel item { get; private set; }
        public ItemConfirmedEvent(BPMenuViewModel ci)
        {
            this.item = ci;
        }
    }
}
