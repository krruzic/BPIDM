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
