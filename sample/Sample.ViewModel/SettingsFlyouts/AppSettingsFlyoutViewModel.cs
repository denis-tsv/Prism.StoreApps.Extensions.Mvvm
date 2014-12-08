using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;

namespace Sample.ViewModel.SettingsFlyouts
{
    public class AppSettingsFlyoutViewModel : SettingsFlyoutViewModel
    {
        private bool _allowBack = true;

        public bool AllowBack
        {
            get { return _allowBack; }
            set { SetProperty(ref _allowBack, value); }
        }

        public ICommand CloseMeCommand { get; private set; }

        public AppSettingsFlyoutViewModel()
        {
            CloseMeCommand = new DelegateCommand(Close);
        }

        public override bool OnBack()
        {
            return !AllowBack;
        }
    }
}
