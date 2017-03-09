namespace BPIDM
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;
    using ViewModels;

    public class AppBootstrapper : BootstrapperBase
    {
        private SimpleContainer container;
        private readonly SimpleContainer _container = new SimpleContainer();
        public Dictionary<string, object> settings = new Dictionary<string, object>
        {
            { "Icon", new BitmapImage(new Uri("pack://application:,,,/BPIDM;component/bp.ico")) },
        };

        public AppBootstrapper()
        {
            // add bindings to use fancy caliburn binds with new controls
            ConventionManager.AddElementConvention<Label>(Label.ContentProperty,
                "Content",
                "DataContextChanged");
            ConventionManager.AddElementConvention<AccessText>(AccessText.TextProperty,
                "Text",
                "DataContextChanged");
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.Singleton<IWindowManager, WindowManager>();
            container.Singleton<IEventAggregator, EventAggregator>();
            container.PerRequest<MainViewModel>();
            container.PerRequest<MainMenuViewModel>();
            container.PerRequest<HeaderViewModel>();
            container.PerRequest<FooterViewModel>();
            container.PerRequest<BPDialogViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>(settings);
        }
    }
}