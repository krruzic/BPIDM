using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class AddItemToBillEvent
    {
        public BPOrderItemViewModel item { get; private set; }
        public AddItemToBillEvent(BPOrderItemViewModel oi)
        {
            this.item = oi;
        }
    }
}
