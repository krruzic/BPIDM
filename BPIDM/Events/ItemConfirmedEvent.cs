using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class ItemConfirmedEvent
    {
        public BPMenuItemViewModel item { get; private set; }
        public ItemConfirmedEvent(BPMenuItemViewModel ci)
        {
            this.item = ci;
        }
    }
}
