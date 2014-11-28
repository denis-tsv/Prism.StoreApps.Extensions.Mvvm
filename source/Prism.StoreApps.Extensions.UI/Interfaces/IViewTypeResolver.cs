using System;

namespace Prism.StoreApps.Extensions.UI.Interfaces
{
	public interface IViewTypeResolver
	{
		Type ResolveViewType(object viewModel);
	}
}