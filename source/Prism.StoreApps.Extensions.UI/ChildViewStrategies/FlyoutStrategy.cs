using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Prism.StoreApps.Extensions.UI.Resolvers;
using Prism.StoreApps.Extensions.UI.Views;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI.ChildViewStrategies
{
	internal class FlyoutStrategy : IChildViewStrategy
	{
		internal class FlyoutInfo
		{
			public Flyout Flyout { get; set; }
			public TaskCompletionSource<object> TaskSource { get; set; }
		}

		private readonly Dictionary<IFlyoutViewModel, FlyoutInfo> _flyouts = new Dictionary<IFlyoutViewModel, FlyoutInfo>();
		private readonly Dictionary<string, FrameworkElement> _flyoutControls = new Dictionary<string, FrameworkElement>();

		public Task<object> ShowAsync(IChildViewModel viewModel, object optParam = null)
		{
		    var flyoutViewModel = (IFlyoutViewModel) viewModel;
            var flyoutTokenAttribute = flyoutViewModel.GetType().GetTypeInfo().GetCustomAttribute<FlyoutTokenAttribute>();
			if (flyoutTokenAttribute == null)
			{
				throw new Exception("IFlyoutViewModel must have FlyoutToken attribute");
			}

			var frameworkElement = _flyoutControls[flyoutTokenAttribute.Token];
			var fl = new Flyout();

		    if (!double.IsNaN(flyoutViewModel.MaxHeight) || !double.IsNaN(flyoutViewModel.MaxWidth))
		    {
                var flyoutPresenterStyle = new Style(typeof(FlyoutPresenter));

		        if (!double.IsNaN(flyoutViewModel.MaxHeight))
		        {
		            flyoutPresenterStyle.Setters.Add(new Setter(FlyoutPresenter.MaxHeightProperty, flyoutViewModel.MaxHeight));
		        }

                if (!double.IsNaN(flyoutViewModel.MaxWidth))
                {
                    flyoutPresenterStyle.Setters.Add(new Setter(FlyoutPresenter.MaxWidthProperty, flyoutViewModel.MaxWidth));
                }

		        fl.FlyoutPresenterStyle = flyoutPresenterStyle;
		    }

		    fl.Closed += Flyout_OnClosed;

			var vmp = new ViewModelPresenter();
            vmp.ViewModel = flyoutViewModel;
			fl.Content = vmp;

			//			TODO	fl.Placement
			fl.ShowAt(frameworkElement);

			var flyoutInfo = new FlyoutInfo()
			{
				TaskSource = new TaskCompletionSource<object>(),
				Flyout = fl
			};
            
            _flyouts.Add(flyoutViewModel, flyoutInfo);

			return flyoutInfo.TaskSource.Task;
		}

		public void CloseViewModel(IChildViewModel childViewModel)
		{
			var viewModel = childViewModel as IFlyoutViewModel;
			_flyouts[viewModel].Flyout.Hide();
		}

		public void CloseAll()
		{
			foreach (var flyout in _flyouts.Values.ToList())
			{
				flyout.Flyout.Hide();
			}

			_flyoutControls.Clear();
		}

		private void Flyout_OnClosed(object sender, object o)
		{
			var flyout = sender as Flyout;
			if (flyout == null)
				return;

			flyout.Closed -= Flyout_OnClosed;

			var viewModelPresenter = flyout.Content as ViewModelPresenter;
			if (viewModelPresenter == null)
				return;

			var viewModel = viewModelPresenter.ViewModel as IFlyoutViewModel;
			if (viewModel == null)
				return;

			_flyouts[viewModel].TaskSource.SetResult(!viewModel.IsDismissed);
			_flyouts.Remove(viewModel);
		}

		public void RegisterFlyout(FrameworkElement obj, string token)
		{
			_flyoutControls.Add(token, obj);
		}
	}
}