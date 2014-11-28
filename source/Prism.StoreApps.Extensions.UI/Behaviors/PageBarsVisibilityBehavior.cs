using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Xaml.Interactivity;

namespace Prism.StoreApps.Extensions.UI.Behaviors
{
	public class PageBarsVisibilityBehavior : DependencyObject, IBehavior
	{
		public AppBar TopAppBar { get; set; }
		public AppBar BottomAppBar { get; set; }

		public DependencyObject AssociatedObject { get; private set; }

		public static readonly DependencyProperty AllowTopAppBarProperty = DependencyProperty.Register("AllowTopAppBar", typeof(bool), typeof(PageBarsVisibilityBehavior), new PropertyMetadata(false, OnAllowTopAppBarPropertyChanged));

		public bool AllowTopAppBar
		{
			get { return (bool)GetValue(AllowTopAppBarProperty); }
			set { SetValue(AllowTopAppBarProperty, value); }
		}

		public static readonly DependencyProperty AllowBottomAppBarProperty = DependencyProperty.Register("AllowBottomAppBar", typeof(bool), typeof(PageBarsVisibilityBehavior), new PropertyMetadata(false, OnAllowBottomAppBarPropertyChanged));

		public bool AllowBottomAppBar
		{
			get { return (bool)GetValue(AllowBottomAppBarProperty); }
			set { SetValue(AllowBottomAppBarProperty, value); }
		}

		public void Attach(DependencyObject associatedObject)
		{
			Debug.Assert(associatedObject is Page);

			AssociatedObject = associatedObject;
			OnAllowTopAppBarChanged();
			OnAllowBottomAppBarChanged();
		}

		public void Detach()
		{
		}

		private static void OnAllowTopAppBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PageBarsVisibilityBehavior)d).OnAllowTopAppBarChanged();
		}

		private static void OnAllowBottomAppBarPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((PageBarsVisibilityBehavior)d).OnAllowBottomAppBarChanged();
		}

		private void OnAllowTopAppBarChanged()
		{
			if (AssociatedObject == null)
				return;

			((Page)AssociatedObject).TopAppBar = AllowTopAppBar ? TopAppBar : null;
		}

		private void OnAllowBottomAppBarChanged()
		{
			if (AssociatedObject == null)
				return;

			((Page)AssociatedObject).BottomAppBar = AllowBottomAppBar ? BottomAppBar : null;
		}
	}
}