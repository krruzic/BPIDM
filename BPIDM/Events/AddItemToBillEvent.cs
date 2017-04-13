using BPIDM.ViewModels;
using System.Collections.Generic;

namespace BPIDM.Events
{
    class AddItemToBillEvent
    {
        public List<BPOrderItemViewModel> items { get; private set; }
        public AddItemToBillEvent()
        {
        }

        public void Add(BPOrderItemViewModel item)
        {
            items.Add(item);
        }
    }
}
