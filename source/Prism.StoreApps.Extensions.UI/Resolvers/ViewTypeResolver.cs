using System;
using Windows.UI.Xaml;
using Prism.StoreApps.Extensions.UI.Resolvers.Interfaces;

namespace Prism.StoreApps.Extensions.UI.Resolvers
{
	public class ViewTypeResolver : IViewTypeResolver
	{
		public string ViewsAssembly { get; set; }
		public string ViewsNamespace { get; set; }
		public string ViewModelsAssembly { get; set; }
		public string ViewModelsNamespace { get; set; }

		public ViewTypeResolver()
		{
			var app = Application.Current as PrismStoreExtensionsApp;
			
			if (app != null)
			{
				ViewsAssembly = app.ViewsAssembly;
				ViewsNamespace = app.ViewsNamespace;
				ViewModelsAssembly = app.ViewModelsAssembly;
				ViewModelsNamespace = app.ViewModelsNamespace;
			}
		}

		public Type ResolveViewType(object viewModel)
		{
			var result = viewModel.GetType().FullName.Replace(ViewModelsNamespace, ViewsNamespace);
			if (result.EndsWith("ViewModel"))
			{
				result = result.Substring(0, result.Length - "ViewModel".Length);
			}

			result += ", " + ViewsAssembly;

			return Type.GetType(result);
		}
	}
}