using BPIDM.ViewModels;
using Caliburn.Micro;

namespace BPIDM {

    public class AppBootstrapper : BootstrapperBase {

        public AppBootstrapper() {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}