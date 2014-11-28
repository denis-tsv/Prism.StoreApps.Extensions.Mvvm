using Windows.UI.Core;

namespace Prism.StoreApps.Extensions.UI.Behaviors
{
	public class CoreWindowPresenter
	{
		public CoreWindow Current
		{
			get { return CoreWindow.GetForCurrentThread(); }
		}
	}
}