using System;
using Caliburn.Micro;


namespace BPIDM.ViewModels
{
    class MainViewModel : Conductor<IScreen>
    {
        public MainViewModel() 
        {
            DisplayName = "BPIDM";
            this.ActivateItem(new MainMenuViewModel());
        }
    }
}
