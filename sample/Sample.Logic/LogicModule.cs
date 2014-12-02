using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.Extensions;
using Prism.StoreApps.Extensions.Modules;

namespace Sample.Logic
{
    public class LogicModule : Module
    {
        public override void RegisterServices(IUnityContainer container)
        {
            container.RegisterTypeAs<IRepository, Repository>(ResolvingMode.Singleton);
        }

        public async override Task InitializeAsync(IUnityContainer container)
        {
            await container.Resolve<IRepository>().LoadSomeDataIntoMemory();
        }
    }
}
