using BPIDM.ViewModels;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace BPIDM {

    public class AppBootstrapper : BootstrapperBase {
        public Dictionary<string, object> settings = new Dictionary<string, object>
        {
            { "Icon", new BitmapImage(new Uri("pack://application:,,,/BPIDM;component/bp.ico")) },
        };
        public AppBootstrapper() {
            Initialize();
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e) {
            DisplayRootViewFor<MainViewModel>(settings);
        }
    }
}