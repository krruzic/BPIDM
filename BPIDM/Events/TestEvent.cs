namespace BPIDM.Events
{
    public class TestEvent
    {
        public TestEvent(string t)
        {
            Res = t;
        }

        public string Res { get; private set; }
    }
}
