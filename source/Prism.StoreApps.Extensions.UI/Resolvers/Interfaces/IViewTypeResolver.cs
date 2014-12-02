using System;

namespace Prism.StoreApps.Extensions.UI.Resolvers.Interfaces
{
	public interface IViewTypeResolver
	{
		Type ResolveViewType(object viewModel);
	}
}