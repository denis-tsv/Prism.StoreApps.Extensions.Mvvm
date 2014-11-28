using Windows.UI.Xaml;
using Microsoft.Practices.ServiceLocation;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;

namespace Prism.StoreApps.Extensions.UI.Views.Child
{
	public class FlyoutView : DependencyObject
	{
		public static readonly DependencyProperty FlyoutTokenProperty = DependencyProperty.RegisterAttached("FlyoutToken", typeof(string), typeof(FlyoutView), new PropertyMetadata(default(string), OnFlyoutTokenPropertyChanged));

		public static void SetFlyoutToken(DependencyObject element, string value)
		{
			element.SetValue(FlyoutTokenProperty, value);
		}

		public static string GetFlyoutToken(DependencyObject element)
		{
			return (string)element.GetValue(FlyoutTokenProperty);
		}

		private static void OnFlyoutTokenPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
				return;

			var token = e.NewValue as string;
			
			var childViewModelManager = ServiceLocator.Current.GetInstance<IChildViewModelManager>();
			childViewModelManager.RegisterFlyout(d as FrameworkElement, token);
		}
	}
}
