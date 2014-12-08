using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Prism.StoreApps.Extensions.Common.IoC;
using Prism.StoreApps.Extensions.Modules;
using Prism.StoreApps.Extensions.UI.Views;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.UI
{
	public abstract class PrismStoreExtensionsApp : MvvmAppBaseEx
	{
		private IUnityContainer _container;
		protected virtual IUnityContainer Container
		{
			get { return _container ?? (_container = new UnityContainer()); }
		}

		public abstract string ViewsAssembly { get; }
		public abstract string ViewsNamespace { get; }
		public abstract string ViewModelsAssembly { get; }
		public abstract string ViewModelsNamespace { get; }

	    protected abstract ModuleCatalog CreateaModuleCatalog(IUnityContainer container);

		protected PrismStoreExtensionsApp()
		{
			ExtendedSplashScreenFactory = (splashscreen) => new DefaultSplashScreenPage(splashscreen);
		}

		protected override async Task OnInitializeAsync(IActivatedEventArgs args)
		{
			InitServiceLocator();
			RegisterBasicInstances();
			ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(ResolveViewModelType);

			await InitializeModulesAsync();
		}

		protected virtual void InitServiceLocator()
		{
			var serviceLocator = new ExtendedUnityServiceLocator(Container);
			ServiceLocator.SetLocatorProvider(() => serviceLocator);
		}

		protected virtual void RegisterBasicInstances()
		{
			Container.RegisterInstance(Container);

			Container.RegisterInstance(NavigationService);
			Container.RegisterInstance(SessionStateService);
		}

		protected virtual async Task InitializeModulesAsync()
		{
            var moduleCatalog = CreateaModuleCatalog(Container);
            await moduleCatalog.InitializeAsync();
		}

		protected override object Resolve(Type type)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			
			return Container.Resolve(type);
		}

		protected virtual Type ResolveViewModelType(Type viewType)
		{
			var viewModelFullName = viewType.FullName.Replace(ViewsNamespace, ViewModelsNamespace) + "ViewModel" + ", " + ViewModelsAssembly;
			var viewModelType = Type.GetType(viewModelFullName);

			if (viewModelType == null)
				throw new Exception(String.Format("Could not resolve viewModel for '{0}' view", viewType));
			
			return viewModelType;
		}

		protected override Type GetPageType(string pageToken)
		{
			var viewFullName = string.Format(CultureInfo.InvariantCulture, "{0}.Pages.{1}Page, {2}", ViewsNamespace, pageToken, ViewsAssembly);
			var pageType = Type.GetType(viewFullName);

			if (pageType == null)
				throw new Exception(String.Format("Could not resolve page type for '{0}' token", pageToken));

			return pageType;
		}

		protected override IList<SettingsCommand> GetSettingsCommands()
		{
			return new List<SettingsCommand>();
		}

		protected SettingsCommand CreateSettingsFlyout<T>(string label)
			where T : ISettingsFlyoutViewModel
		{
			return new SettingsCommand(Guid.NewGuid(), label, ShowSettingsFlyout<T>);
		}

		protected async void ShowSettingsFlyout<T>(IUICommand command)
			where T : ISettingsFlyoutViewModel
		{
			var childViewModelManager = Container.Resolve<IChildViewModelManager>();
			var viewModel = childViewModelManager.Resolve<T>();
			await viewModel.ShowAsync();
		}
	}
}
