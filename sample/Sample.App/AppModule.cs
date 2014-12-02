using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.Extensions;
using Prism.StoreApps.Extensions.Modules;
using Prism.StoreApps.Extensions.UI;
using Prism.StoreApps.Extensions.UI.DefaultImplementation;
using Prism.StoreApps.Extensions.UI.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;

namespace Sample.App
{
    public class AppModule : Module
    {
        public override void RegisterServices(IUnityContainer container)
        {
            container.RegisterTypeAs<IChildViewModelManager, ChildViewModelManager>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IViewTypeResolver, GeneralViewTypeResolver>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IFlyoutViewTypeResolver, DefaultFlyoutViewTypeResolver>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IAlertMessageManager, DefaultAlertMessageManager>(ResolvingMode.Singleton);
        }
    }
}
