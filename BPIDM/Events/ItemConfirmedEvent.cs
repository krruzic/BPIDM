using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class ItemConfirmedEvent
    {
        public BPBaseItemViewModel item { get; private set; }
        public ItemConfirmedEvent(BPBaseItemViewModel ci)
        {
            this.item = ci;
        }
    }
}
