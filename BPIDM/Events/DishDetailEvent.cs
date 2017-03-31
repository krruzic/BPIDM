using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class DishDetailEvent
    {
        public BPMenuItemViewModel item { get; private set; }
        public DishDetailEvent(BPMenuItemViewModel ci)
        {
            this.item = ci;
        }
    }
}
