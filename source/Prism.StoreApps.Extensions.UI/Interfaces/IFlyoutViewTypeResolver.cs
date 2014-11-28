using System;

namespace Prism.StoreApps.Extensions.UI.Interfaces
{
	public interface IFlyoutViewTypeResolver
	{
		Type GetViewType(string token);
	}
}