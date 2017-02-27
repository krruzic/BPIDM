using System;
using Caliburn.Micro;


namespace BPIDM.ViewModels
{
    class MainViewModel : Conductor<IScreen>
    {
        public MainViewModel() 
        {
            this.ActivateItem(new MainMenuViewModel());
        }
    }
}
