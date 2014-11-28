using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Prism.StoreApps.Extensions.UI.Views.Child
{
	public sealed partial class ChildView : UserControl
	{
		public ChildView()
		{
			this.InitializeComponent();
		}

		private void ChildView_OnLoaded(object sender, RoutedEventArgs e)
		{
			var pane = InputPane.GetForCurrentView();
			if (pane != null)
			{
				pane.Showing += pane_Showing;
				pane.Hiding += pane_Hiding;
			}
		}

		private void ChildView_OnUnloaded(object sender, RoutedEventArgs e)
		{
			var pane = InputPane.GetForCurrentView();
			if (pane != null)
			{
				pane.Showing -= pane_Showing;
				pane.Hiding -= pane_Hiding;
			}
		}

		void pane_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			var popup = this.Parent as Popup;
			if (popup != null)
			{
				popup.Height = Window.Current.Bounds.Height;
				this.Height = popup.Height;
			}
		}

		void pane_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
		{
			var popup = this.Parent as Popup;
			if (popup != null)
			{
				popup.Height = Window.Current.Bounds.Height - args.OccludedRect.Top;
				this.Height = popup.Height;
			}
		}
	}
}
