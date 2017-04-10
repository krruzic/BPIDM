using System.Collections.Generic;

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
