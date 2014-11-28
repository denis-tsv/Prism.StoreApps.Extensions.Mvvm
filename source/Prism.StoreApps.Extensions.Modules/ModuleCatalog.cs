using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Modules
{
	public class ModuleCatalog
	{
		#region Fields

		private readonly IUnityContainer _context;
		private IList<IModule> _modules;
        private readonly List<Type> _moduleTypes = new List<Type>();

		#endregion

		#region .ctor

		public ModuleCatalog(IUnityContainer context)
		{
			_context = context;
		}

		#endregion

	    public void AddModule(Type moduleType)
	    {
            _moduleTypes.Add(moduleType);
	    }

	    public async Task LoadAsync()
		{
			_modules = CreateModules();

			LoadModules();

			await InitializeModules();
		}

		private void LoadModules()
		{
			for(int i=0; i<_modules.Count; i++)
			{
			    IModule module = _modules[i];
				module.Load(_context);
			}
		}

		private async Task InitializeModules()
		{
            for(int i=0; i<_modules.Count; i++)
			{
			    IModule module = _modules[i];
				await module.Initialize(_context);
			}
		}

		private IList<IModule> CreateModules()
		{
			return _moduleTypes.Select(type => (IModule)Activator.CreateInstance(type)).ToList();
		}
	}
	
}
