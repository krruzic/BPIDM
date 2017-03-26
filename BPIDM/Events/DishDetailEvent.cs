using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class DishDetailEvent
    {
        public BPMenuItemViewModel item;
        public DishDetailEvent(BPMenuItemViewModel ci)
        {
            this.item = ci;
        }
    }
}
