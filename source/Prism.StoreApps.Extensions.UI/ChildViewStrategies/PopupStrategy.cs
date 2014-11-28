using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Prism.StoreApps.Extensions.UI.Views.Child;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI.ChildViewStrategies
{
	internal class PopupStrategy : IChildViewStrategy
	{
		internal class PopupInfo
		{
			public Popup Popup { get; set; }
			public TaskCompletionSource<object> TaskSource { get; set; }
		}

		private readonly Dictionary<IChildViewModel, PopupInfo> _popups = new Dictionary<IChildViewModel, PopupInfo>();

		public Task<object> ShowAsync(IChildViewModel viewModel, object optParam = null)
		{
			if (_popups.Count != 0)
			{
				return Task.FromResult<object>(null);
			}

			Popup popup;
			
			var showParam = optParam as PopupShowParam;
			if (showParam != null && showParam.Mode == PopupShowParamMode.UpperRegionNotification)
			{
				popup = PrepareUpperRegionNotificationView(viewModel);
			}
			else
			{
				popup = PrepareChildView(viewModel);
			}

			var popupInfo = new PopupInfo
			{
				Popup = popup,
				TaskSource = new TaskCompletionSource<object>()
			};
			_popups.Add(viewModel, popupInfo);

			StartDisplayDurationTimer(viewModel, showParam);

			return popupInfo.TaskSource.Task;
		}

		private void StartDisplayDurationTimer(IChildViewModel viewModel, PopupShowParam showParam)
		{
			if (showParam != null && showParam.DisplayDuration.HasValue)
			{
				var timer = new DispatcherTimer();
				timer.Interval = showParam.DisplayDuration.Value;

				EventHandler<object> eh = null;
				eh = (sender, e) =>
				{
					timer.Tick -= eh;
					timer.Stop();

					CloseViewModel(viewModel);
					//await Window.Current.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => CloseViewModel(viewModel));
				};

				timer.Tick += eh;

				timer.Start();
			}
		}

		private static Popup PrepareUpperRegionNotificationView(IChildViewModel viewModel)
		{
			var width = Window.Current.Bounds.Width;

			var childView = new UpperRegionNotificationView()
			{
				Width = width,
				DataContext = viewModel
			};

			return new Popup
			{
				Width = width,
				Child = childView,
				IsLightDismissEnabled = false,
				IsOpen = true
			};			
		}

		private static Popup PrepareChildView(IChildViewModel viewModel)
		{
			Rect bounds = Window.Current.Bounds;
			var childView = new ChildView
			{
				Width = bounds.Width,
				Height = bounds.Height,
				DataContext = viewModel
			};

			return new Popup
			{
				Width = bounds.Width,
				Height = bounds.Height,
				Child = childView,
				IsLightDismissEnabled = false,
				IsOpen = true
			};			
		}

		public void CloseViewModel(IChildViewModel childViewModel)
		{
			PopupInfo popupInfo;
			if (_popups.TryGetValue(childViewModel, out popupInfo))
			{
				_popups.Remove(childViewModel);
				popupInfo.Popup.IsOpen = false;

				if (childViewModel is IModalChildViewModel)
				{
					var viewModel = childViewModel as IModalChildViewModel;
					popupInfo.TaskSource.SetResult(viewModel.ModalResult);
				}
				else
				{
					popupInfo.TaskSource.SetResult(null);
				}
			}
		}

		public void CloseAll()
		{
			foreach (var childViewModel in _popups.Keys.ToList())
			{
				CloseViewModel(childViewModel);
			}
		}
	}
}