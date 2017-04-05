using BPIDM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPIDM.Events
{
    class SendBillInformationEvent
    {
        public int TotalBills;
        public List<string> BillColors { get; private set; }

        public SendBillInformationEvent(int tb, List<string> bc)
        {
            TotalBills = tb;
            BillColors = bc;
        }

    }
}
