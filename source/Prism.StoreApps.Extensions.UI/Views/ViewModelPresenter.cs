using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Practices.ServiceLocation;
using Prism.StoreApps.Extensions.UI.Resolvers.Interfaces;

namespace Prism.StoreApps.Extensions.UI.Views
{
	public class ViewModelPresenter : ContentControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register("ViewModel", typeof(object), typeof(ViewModelPresenter), new PropertyMetadata(null, OnViewModelChanged));

		public object ViewModel
		{
			get { return GetValue(ViewModelProperty); }
			set { SetValue(ViewModelProperty, value); }
		}

		private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((ViewModelPresenter)d).RefreshContentPresenter();
		}

		private void RefreshContentPresenter()
		{
			if (ViewModel == null)
			{
				Content = null;
				return;
			}

			var viewTypeResolver = ServiceLocator.Current.GetInstance<IViewTypeResolver>();
			var viewType = viewTypeResolver.ResolveViewType(ViewModel);

			if (viewType == null)
			{
				Content = null;
			}
			else
			{
				var viewObject = Activator.CreateInstance(viewType);
				Debug.Assert(viewObject is FrameworkElement);

				var view = (FrameworkElement)viewObject;
				view.DataContext = ViewModel;
				Content = view;
			}
		}
	}
}
