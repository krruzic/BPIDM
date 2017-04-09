using Caliburn.Micro;

namespace BPIDM.Models
{
    public class BillCheck : PropertyChangedBase
    {
        private string _Bill;
        public string Bill
        {
            get { return _Bill; }
            set
            {
                _Bill = value;
                NotifyOfPropertyChange(() => Bill);
            }
        }
        private bool _Checked;
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                _Checked = value;
                NotifyOfPropertyChange(() => Checked);
            }
        }
        public BillCheck(string b)
        {
            Bill = b;
            Checked = false;
        }
    }
}
