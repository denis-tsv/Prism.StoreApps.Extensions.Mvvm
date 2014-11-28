using System.Linq;
using System.Xml.Linq;

namespace Prism.StoreApps.Extensions.Common.Helpers
{
	public static class ManifestHelper
	{
		private readonly static XDocument Manifest;
		private readonly static XNamespace XNamespace;
		private readonly static XNamespace M2Namespace;

		static ManifestHelper()
		{
			Manifest = XDocument.Load("AppxManifest.xml", LoadOptions.None);
			XNamespace = XNamespace.Get("http://schemas.microsoft.com/appx/2010/manifest");
			M2Namespace = XNamespace.Get("http://schemas.microsoft.com/appx/2013/manifest");
		}

		public static string GetSplashBackgroundColor()
		{
			return GetValue("SplashScreen", "BackgroundColor", M2Namespace) ?? GetValue("VisualElements", "BackgroundColor", M2Namespace);
		}

		public static string GetSplashImage()
		{
			return GetValue("SplashScreen", "Image", M2Namespace);
		}

		private static string GetValue(string node, string attribute, XNamespace ns)
		{
			var valueQuery = from el in Manifest.Descendants(ns + node)
							 let attr = el.Attribute(attribute)
							 where attr != null
							 select attr.Value;

			return valueQuery.FirstOrDefault();
		}
	}
}
