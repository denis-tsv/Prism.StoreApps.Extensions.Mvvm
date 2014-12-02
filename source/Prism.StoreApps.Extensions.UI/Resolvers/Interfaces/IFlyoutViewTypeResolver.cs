using System;

namespace Prism.StoreApps.Extensions.UI.Resolvers.Interfaces
{
	public interface IFlyoutViewTypeResolver
	{
		Type GetViewType(string token);
	}
}