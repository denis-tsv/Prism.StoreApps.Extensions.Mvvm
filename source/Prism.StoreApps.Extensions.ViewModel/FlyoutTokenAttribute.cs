using System;

namespace Prism.StoreApps.Extensions.ViewModel
{
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
