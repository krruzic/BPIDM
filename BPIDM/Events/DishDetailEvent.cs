using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class DishDetailEvent
    {
        public BPMenuViewModel item;
        public DishDetailEvent(BPMenuViewModel ci)
        {
            this.item = ci;
        }
    }
}
