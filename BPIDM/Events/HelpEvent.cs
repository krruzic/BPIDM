namespace BPIDM.Events
{
    public class HelpEvent
    {
        public string Title { get; internal set; }
        public string HelpText { get; private set; }
        public HelpEvent(string t, string h)
        {
            Title = t;
            HelpText = h;
        }
    }
}
