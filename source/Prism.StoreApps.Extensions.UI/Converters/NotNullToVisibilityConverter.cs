﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Prism.StoreApps.Extensions.UI.Converters
{
    public sealed class NotNullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (value != null) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value is Visibility && (Visibility)value == Visibility.Visible) ? true : false;
        }
    }
}
