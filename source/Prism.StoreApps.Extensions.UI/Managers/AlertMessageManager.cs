using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Prism.StoreApps.Extensions.UI.Strings;
using Prism.StoreApps.Extensions.ViewModel.Interfaces;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;

namespace Prism.StoreApps.Extensions.UI.Managers
{
	public class AlertMessageManager : IAlertMessageManager
	{
		private static bool _isShowing;

		public async Task ShowMessageAsync(string message, string title)
		{
			await ShowDialogAsync(message, title, DialogCommand.OkCommands);
		}

		public async void ShowMessage(string message, string title)
		{
			await ShowDialogAsync(message, title, DialogCommand.OkCommands);
		}

		public async Task ShowErrorAsync(Exception ex, string title)
		{
			await ShowDialogAsync(ex.Message, title, DialogCommand.OkCommands);
		}

		public async void ShowError(Exception ex, string title)
		{
			await ShowDialogAsync(ex.Message, title, DialogCommand.OkCommands);
		}

		public async Task<DialogResult> ShowDialogAsync(string message, string title, IEnumerable<DialogCommand> dialogCommands, uint? defaultCommandIndex = null, uint? cancelCommandIndex = null)
		{
			if (_isShowing)
				throw new Exception(ErrorStrings.AlertMessage_ShowTwoDialog);

			var messageDialog = new MessageDialog(message, title);
			
			if (dialogCommands != null)
			{
				foreach (var command in dialogCommands)
				{
					messageDialog.Commands.Add(new UICommand
					{
						Label = command.Label,
						Id = command.DialogResult,
					});
				}
			}

			if (defaultCommandIndex.HasValue)
				messageDialog.DefaultCommandIndex = defaultCommandIndex.Value;

			if (cancelCommandIndex.HasValue)
				messageDialog.CancelCommandIndex = cancelCommandIndex.Value;			

			_isShowing = true;
			var result = await messageDialog.ShowAsync();
			_isShowing = false;

			return (DialogResult)result.Id;
		}
	}
}
