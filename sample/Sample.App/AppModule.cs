using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.Extensions;
using Prism.StoreApps.Extensions.Modules;
using Prism.StoreApps.Extensions.UI;
using Prism.StoreApps.Extensions.UI.Managers;
using Prism.StoreApps.Extensions.UI.Resolvers;
using Prism.StoreApps.Extensions.UI.Resolvers.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;

namespace Sample.App
{
    public class AppModule : Module
    {
        public override void RegisterServices(IUnityContainer container)
        {
            container.RegisterTypeAs<IChildViewModelManager, ChildViewModelManager>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IViewTypeResolver, ViewTypeResolver>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IFlyoutViewTypeResolver, FlyoutViewTypeResolver>(ResolvingMode.Singleton);
            container.RegisterTypeAs<IAlertMessageManager, AlertMessageManager>(ResolvingMode.Singleton);
        }
    }
}
