using BPIDM.ViewModels;

namespace BPIDM.Events
{
    class DishDetailEvent
    {
        public BPOrderItemViewModel item { get; private set; }
        public DishDetailEvent(BPOrderItemViewModel ci)
        {
            this.item = ci;
        }
    }
}
