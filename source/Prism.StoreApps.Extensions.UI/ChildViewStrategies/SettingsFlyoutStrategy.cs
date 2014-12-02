using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Prism.StoreApps.Extensions.UI.Resolvers.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI.ChildViewStrategies
{
	internal class SettingsFlyoutStrategy : IChildViewStrategy
	{
		internal class SettingsFlyoutInfo
		{
			public SettingsFlyout Flyout { get; set; }
			public ISettingsFlyoutViewModel ViewModel { get; set; }
			public TaskCompletionSource<object> TaskSource { get; set; }
		}

		private readonly Dictionary<SettingsFlyout, SettingsFlyoutInfo> _settingsFlyoutInfos = new Dictionary<SettingsFlyout, SettingsFlyoutInfo>();

		public IViewTypeResolver ViewTypeResolver { private get; set; }
		
		public Task<object> ShowAsync(IChildViewModel viewModel, object optParam = null)
		{
			var viewType = ViewTypeResolver.ResolveViewType(viewModel);
			if (viewType == null)
				Task.FromResult<object>(null);

			var viewObject = Activator.CreateInstance(viewType);
			Debug.Assert(viewObject is SettingsFlyout);

			var view = (SettingsFlyout)viewObject;
			view.DataContext = viewModel;
			view.Unloaded += settingsFlyout_Unloaded;
			view.BackClick += settingsFlyout_BackClick;

			bool showIndependent = false;
			var showParam = optParam as SettingsFlyoutShowParam;
			if (showParam != null)
			{
				showIndependent = showParam.ShowIndependent;
			}

			if (showIndependent)
			{
				view.ShowIndependent();
			}
			else
			{
				view.Show();
			}

			var flyoutInfo = new SettingsFlyoutInfo()
			{
				TaskSource = new TaskCompletionSource<object>(),
				Flyout = view,
				ViewModel = viewModel as ISettingsFlyoutViewModel
			};

			_settingsFlyoutInfos.Add(view, flyoutInfo);
			return flyoutInfo.TaskSource.Task;
		}

		public void CloseViewModel(IChildViewModel childViewModel)
		{
			foreach (var kvp in _settingsFlyoutInfos)
			{
				if (kvp.Value.ViewModel == childViewModel)
				{
					kvp.Key.Hide();
					return;
				}
			}
		}

		public void CloseAll()
		{
			foreach (var flyout in _settingsFlyoutInfos.Keys.ToList())
			{
				flyout.Hide();
			}
		}

		void settingsFlyout_Unloaded(object sender, RoutedEventArgs e)
		{
			var settingsFlyout = sender as SettingsFlyout;
			if (settingsFlyout == null)
				return;

			settingsFlyout.Unloaded -= settingsFlyout_Unloaded;
			settingsFlyout.BackClick -= settingsFlyout_BackClick;

			//var viewModel = settingsFlyout.DataContext as ISettingsFlyoutViewModel;
			//if (viewModel == null)
			//	return;

			//viewModel.OnDismiss();

			_settingsFlyoutInfos[settingsFlyout].TaskSource.SetResult(null);
			_settingsFlyoutInfos.Remove(settingsFlyout);
		}

		void settingsFlyout_BackClick(object sender, BackClickEventArgs e)
		{
			var settingsFlyout = sender as SettingsFlyout;
			if (settingsFlyout == null)
				return;

			var viewModel = settingsFlyout.DataContext as ISettingsFlyoutViewModel;
			if (viewModel == null)
				return;

			e.Handled = viewModel.OnBack();
		}
	}
}