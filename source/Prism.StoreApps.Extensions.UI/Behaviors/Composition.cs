using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Microsoft.Practices.ServiceLocation;
using Prism.StoreApps.Extensions.Common.IoC.Interfaces;

namespace Prism.StoreApps.Extensions.UI.Behaviors
{
    public static class Composition
    {
        public static readonly DependencyProperty ComposableProperty = DependencyProperty.RegisterAttached("Composable", typeof(bool), typeof(Composition), new PropertyMetadata(false, OnComposableChanged));

        public static bool GetComposable(DependencyObject obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return (bool)obj.GetValue(ComposableProperty);
        }
		  
        public static void SetComposable(DependencyObject obj, bool value)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            obj.SetValue(ComposableProperty, value);
        }

        private static void OnComposableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Debug.Assert(e.NewValue is bool);

            if ((bool)e.NewValue)
            {
                var locator = ServiceLocator.Current as IExtendedServiceLocator;

                if (locator == null)
                {
                    throw new InvalidOperationException("No proper Extended Service Locator set");
                }

                locator.BuildUp(d);
            }
        }
    }
}
