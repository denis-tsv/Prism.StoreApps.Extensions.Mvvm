using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.Extensions;
using Prism.StoreApps.Extensions.Modules;
using Prism.StoreApps.Extensions.UI;
using Prism.StoreApps.Extensions.UI.DefaultImplementation;
using Prism.StoreApps.Extensions.UI.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Sample.Logic;
using Sample.ViewModel;

namespace Sample.App
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App 
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected async override Task OnLaunchApplication(LaunchActivatedEventArgs args)
        {
            //if (args.PreviousExecutionState != ApplicationExecutionState.Running)
            //{
            //    // Here we would load the application's resources.
            //    await Task.Delay(2000);
            //}

            NavigationService.Navigate("Main");
        }

        public override string ViewsAssembly
        {
            get { return "Sample.App"; }
        }

        public override string ViewsNamespace
        {
            get { return "Sample.App"; }
        }

        public override string ViewModelsAssembly
        {
            get { return "Sample.ViewModel"; }
        }

        public override string ViewModelsNamespace
        {
            get { return "Sample.ViewModel"; }
        }

        protected override ModuleCatalog CreateaModuleCatalog(IUnityContainer container)
        {
            var moduleCatalog = new ModuleCatalog(container);

            moduleCatalog.AddModule(typeof(LogicModule));
            moduleCatalog.AddModule(typeof(ViewModelModule));
            moduleCatalog.AddModule(typeof(AppModule));

            return moduleCatalog;
        }
    }
}
