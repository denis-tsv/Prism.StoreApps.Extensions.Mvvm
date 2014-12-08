using System;
using Windows.UI.Xaml;
using Prism.StoreApps.Extensions.UI.Resolvers.Interfaces;

namespace Prism.StoreApps.Extensions.UI.Resolvers
{
	public class FlyoutViewTypeResolver : IFlyoutViewTypeResolver
	{
		public string ViewsAssembly { get; set; }
		public string ViewsNamespace { get; set; }

		public FlyoutViewTypeResolver()
		{
			var app = Application.Current as PrismStoreExtensionsApp;

			if (app != null)
			{
				ViewsAssembly = app.ViewsAssembly;
				ViewsNamespace = app.ViewsNamespace;
			}
		}

		public Type GetViewType(string token)
		{
			var viewFullName = String.Format("{0}.Flyouts.{1}Flyout , {2}", ViewsNamespace, token, ViewsAssembly);
			return Type.GetType(viewFullName);
		}
	}
}
