﻿using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace Prism.StoreApps.Extensions.Modules
{
	public abstract class Module : IModule
	{
		public virtual async Task InitializeAsync(IUnityContainer container)
		{
			
		}

        public virtual void RegisterServices(IUnityContainer container)
		{
			
		}
	}
}
