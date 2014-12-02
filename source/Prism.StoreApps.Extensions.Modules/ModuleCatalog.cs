using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Modules
{
	public class ModuleCatalog
	{
		private readonly IUnityContainer _context;
		private IList<IModule> _modules;
        private readonly List<Type> _moduleTypes = new List<Type>();

		public ModuleCatalog(IUnityContainer context)
		{
			_context = context;
		}

	    public void AddModule(Type moduleType)
	    {
            _moduleTypes.Add(moduleType);
	    }

        /// <summary>
        /// Performs registering of services and initialization of modules in order of modules was added
        /// </summary>
        public async Task InitializeAsync()
		{
			_modules = CreateModules();

			RegisterModuleServices();

			await InitializeModulesAsync();
		}

		private void RegisterModuleServices()
		{
			for(int i=0; i<_modules.Count; i++)
			{
			    IModule module = _modules[i];
				module.RegisterServices(_context);
			}
		}

		private async Task InitializeModulesAsync()
		{
            for(int i=0; i<_modules.Count; i++)
			{
			    IModule module = _modules[i];
				await module.InitializeAsync(_context);
			}
		}

		private IList<IModule> CreateModules()
		{
			return _moduleTypes.Select(type => (IModule)Activator.CreateInstance(type)).ToList();
		}
	}
	
}
