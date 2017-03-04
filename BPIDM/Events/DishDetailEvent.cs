using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
