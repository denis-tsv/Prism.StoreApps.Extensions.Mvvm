using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class DependencyObjectExtensions
	{
		public static T GetVisualParent<T>(this DependencyObject child) 
			where T : FrameworkElement
		{
			while ((child != null) && !(child is T))
			{
				child = VisualTreeHelper.GetParent(child);
			}
			return child as T;
		}

		public static T GetVisualChild<T>(this DependencyObject parent) 
			where T : DependencyObject
		{
			T child = default(T);
			int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < numVisuals; i++)
			{
				DependencyObject v = VisualTreeHelper.GetChild(parent, i);
				child = v as T;
				if (child == null)
					child = GetVisualChild<T>(v);
				if (child != null)
					break;
			}
			return child;
		}
	}
}
