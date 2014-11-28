using System;
using Windows.UI.Xaml;
using Prism.StoreApps.Extensions.UI.Interfaces;

namespace Prism.StoreApps.Extensions.UI.DefaultImplementation
{
	public class DefaultFlyoutViewTypeResolver : IFlyoutViewTypeResolver
	{
		public string ViewsAssembly { get; set; }
		public string ViewsNamespace { get; set; }

		public DefaultFlyoutViewTypeResolver()
		{
			var app = Application.Current as AstroSoftApp;

			if (app != null)
			{
				ViewsAssembly = app.ViewsAssembly;
				ViewsNamespace = app.ViewsNamespace;
			}
		}

		public Type GetViewType(string token)
		{
			var viewFullName = String.Format("{0}.Flyouts.{1}Flyout , {2}", ViewsNamespace, token, ViewsAssembly);
			return Type.GetType(viewFullName);
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class FlyoutTokenAttribute : Attribute
	{
		private readonly string _token;

		public FlyoutTokenAttribute(string token)
		{
			_token = token;
		}

		public string Token
		{
			get { return _token; }
		}
	}
}
