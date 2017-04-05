namespace BPIDM.Events
{
    class ShowHelpEvent
    {
        public object Param { get; internal set; }
        public ShowHelpEvent()
        {
            Param = "";
        }

        public ShowHelpEvent(string p)
        {
            Param = p;
        }

    }
}