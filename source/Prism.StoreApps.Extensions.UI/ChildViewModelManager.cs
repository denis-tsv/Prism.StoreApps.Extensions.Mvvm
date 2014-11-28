using System.Threading.Tasks;
using Windows.UI.Xaml;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.UI.ChildViewStrategies;
using Prism.StoreApps.Extensions.UI.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI
{
	public class ChildViewModelManager : IChildViewModelManager
	{
		#region Fields

		private IViewTypeResolver _viewTypeResolver;

		private readonly PopupStrategy _popupStrategy = new PopupStrategy();
		private readonly FlyoutStrategy _flyoutStrategy = new FlyoutStrategy();
		private readonly SettingsFlyoutStrategy _settingsFlyoutStrategy = new SettingsFlyoutStrategy();

		#endregion

		[Dependency]
		public IUnityContainer Container { private get; set; }

		[Dependency]
		public IViewTypeResolver ViewTypeResolver
		{
			private get { return _viewTypeResolver; }
			set
			{
				_viewTypeResolver = value;
				if (value != null)
				{
					_settingsFlyoutStrategy.ViewTypeResolver = value;
				}
			}
		}

		public T Resolve<T>()
			where T : IChildViewModel
		{
			return Container.Resolve<T>();
		}

		public Task<object> ShowViewModelAsync(IChildViewModel viewModel, object optParam = null)
		{
			var strategy = GetStrategy(viewModel);
			return strategy.ShowAsync(viewModel, optParam);			
		}

		public void CloseViewModel(IChildViewModel childViewModel)
		{
			var strategy = GetStrategy(childViewModel);
			strategy.CloseViewModel(childViewModel);
		}

		public void CloseAll()
		{
			_popupStrategy.CloseAll();
			_flyoutStrategy.CloseAll();
			_settingsFlyoutStrategy.CloseAll();
		}

		public void RegisterFlyout(FrameworkElement obj, string token)
		{
			_flyoutStrategy.RegisterFlyout(obj, token);
		}

		private IChildViewStrategy GetStrategy(IChildViewModel viewModel)
		{
			if (viewModel is IFlyoutViewModel)
			{
				return _flyoutStrategy;
			}

			if (viewModel is ISettingsFlyoutViewModel)
			{
				return _settingsFlyoutStrategy;
			}

			return _popupStrategy;
		}
	}
}
