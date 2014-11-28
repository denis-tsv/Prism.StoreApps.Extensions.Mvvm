namespace Microsoft.Practices.Prism.Mvvm.Interfaces
{
    public static class NavigationServiceEx
    {
        public static bool Navigate(this INavigationService service, string pageToken)
        {
            return service.Navigate(pageToken, null);
        }
    }
}
