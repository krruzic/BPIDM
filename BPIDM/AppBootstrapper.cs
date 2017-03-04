namespace BPIDM
{
    using Caliburn.Micro;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using ViewModels;
    using System.Linq;
    using Views;
    using System.Windows.Controls;
    using CachedImage;
    public class AppBootstrapper : BootstrapperBase
    {
        private CompositionContainer container;
        private readonly SimpleContainer _container = new SimpleContainer();
        public Dictionary<string, object> settings = new Dictionary<string, object>
        {
            { "Icon", new BitmapImage(new Uri("pack://application:,,,/BPIDM;component/bp.ico")) },
        };

        public AppBootstrapper()
        {
            // no idea why this is needed!
            ViewLocator.NameTransformer.AddRule(typeof(BPMenuViewModel).FullName, typeof(BPMenuView).FullName);
            ViewLocator.NameTransformer.AddRule(typeof(BPCategoryViewModel).FullName, typeof(BPCategoryView).FullName);
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
            container = new CompositionContainer(new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));
            CompositionBatch batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(container);

            container.Compose(batch);
            _container.Singleton<IEventAggregator, EventAggregator>();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var exports = container.GetExportedValues<object>(contract);

            if (exports.Count() > 0)
            {
                return exports.First();
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }
        protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>(settings);
        }
    }
}