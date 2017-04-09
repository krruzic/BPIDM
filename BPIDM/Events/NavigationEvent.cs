namespace BPIDM.Events
{
    public class NavigationEvent
    {
        public string res { get; private set; }
        public NavigationEvent(string t)
        {
            res = t;
        }
    }
}
