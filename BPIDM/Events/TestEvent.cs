namespace BPIDM.Events
{
    public class TestEvent
    {
        public string res { get; private set; }
        public TestEvent(string t)
        {
            res = t;
        }
    }
}
