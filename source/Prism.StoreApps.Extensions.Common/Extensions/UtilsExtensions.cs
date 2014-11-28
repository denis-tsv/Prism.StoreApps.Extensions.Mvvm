using System;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class UtilsExtensions
	{
		public static void NullSafeDispose(this IDisposable obj)
		{
			if (obj != null)
				obj.Dispose();
		}
	}
}
