// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Prism.StoreApps.Extensions.Common.Extensions;
using Prism.StoreApps.Extensions.Common.Helpers;

namespace Prism.StoreApps.Extensions.UI.Views
{
	public sealed partial class DefaultSplashScreenPage : Page
	{
		public const string ProgressRingStyleKey = "SplashScreenProgressRingStyle";

		#region Fields

		private readonly SplashScreen _splashScreen;

		#endregion

		public DefaultSplashScreenPage(SplashScreen splashScreen)
		{
			_splashScreen = splashScreen;

			this.InitializeComponent();

			Window.Current.SizeChanged += ExtendedSplash_OnResize;

			var str = ManifestHelper.GetSplashBackgroundColor();
			if (!String.IsNullOrEmpty(str))
			{
				var color = str.ToColor();
				gg.Background = new SolidColorBrush(color);
			}
			else
			{
				Debug.WriteLine("Не удалось получить из манифеста цвет фона для SplashScreen.");
			}

			PositionImage();

			if (Application.Current.Resources.ContainsKey(ProgressRingStyleKey))
			{
				pr.Style = (Style)Application.Current.Resources[ProgressRingStyleKey];
			}
		}

		private void PositionImage()
		{
			var splashImage = ManifestHelper.GetSplashImage();
			if (!String.IsNullOrEmpty(splashImage))
			{
				extendedSplashImage.ImageOpened += extendedSplashImage_ImageOpened;
				extendedSplashImage.Source = new BitmapImage(new Uri("ms-appx:///" + splashImage));

				extendedSplashImage.SetValue(Canvas.LeftProperty, _splashScreen.ImageLocation.X);
				extendedSplashImage.SetValue(Canvas.TopProperty, _splashScreen.ImageLocation.Y);
				extendedSplashImage.Height = _splashScreen.ImageLocation.Height;
				extendedSplashImage.Width = _splashScreen.ImageLocation.Width;
			}
			else
			{
				Debug.WriteLine("Не удалось получить из манифеста изображение для SplashScreen.");
				Window.Current.Activate();
			}
		}

		void extendedSplashImage_ImageOpened(object sender, RoutedEventArgs e)
		{
			Window.Current.Activate();
		}

		private void ExtendedSplash_OnResize(object sender, WindowSizeChangedEventArgs e)
		{
			PositionImage();
		}
	}
}
