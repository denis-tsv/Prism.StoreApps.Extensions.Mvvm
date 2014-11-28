using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Prism.StoreApps.Extensions.ViewModel.Strings;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs;

namespace Prism.StoreApps.Extensions.ViewModel.Interfaces
{
	public class DialogCommand
	{
		public static DialogCommand OkCommand = new DialogCommand { DialogResult = DialogResult.Ok, Label = DialogCommandStrings.Ok };
		public static DialogCommand CancelCommand = new DialogCommand { DialogResult = DialogResult.Cancel, Label = DialogCommandStrings.Cancel };
		public static DialogCommand YesCommand = new DialogCommand { DialogResult = DialogResult.Yes, Label = DialogCommandStrings.Yes };
		public static DialogCommand NoCommand = new DialogCommand { DialogResult = DialogResult.No, Label = DialogCommandStrings.No };
		public static DialogCommand AbortCommand = new DialogCommand { DialogResult = DialogResult.Abort, Label = DialogCommandStrings.Abort };
		public static DialogCommand RetryCommand = new DialogCommand { DialogResult = DialogResult.Retry, Label = DialogCommandStrings.Retry };
		public static DialogCommand IgnoreCommand = new DialogCommand { DialogResult = DialogResult.Ignore, Label = DialogCommandStrings.Ignore };

		public static IEnumerable<DialogCommand> OkCommands = new List<DialogCommand> { OkCommand };
		public static IEnumerable<DialogCommand> OkCancelCommands = new List<DialogCommand> { OkCommand, CancelCommand };
		public static IEnumerable<DialogCommand> YesNoCommands = new List<DialogCommand> { YesCommand, NoCommand };
		public static IEnumerable<DialogCommand> YesNoCancelCommands = new List<DialogCommand> { YesCommand, NoCommand, CancelCommand };
		public static IEnumerable<DialogCommand> RetryCancelCommands = new List<DialogCommand> { RetryCommand, CancelCommand };
		public static IEnumerable<DialogCommand> AbortRetryIgnoreCommands = new List<DialogCommand> { AbortCommand, RetryCommand, IgnoreCommand };

		public string Label { get; set; }
		public DialogResult DialogResult { get; set; }

		public DialogCommand()
		{
		}

		public DialogCommand(string label, DialogResult result)
		{
			Label = label;
			DialogResult = result;
		}
	}

	public interface IAlertMessageManager
	{
		Task ShowMessageAsync(string message, string title);
		void ShowMessage(string message, string title);
		Task ShowErrorAsync(Exception ex, string title);
		void ShowError(Exception ex, string title);
		Task<DialogResult> ShowDialogAsync(string message, string title, IEnumerable<DialogCommand> dialogCommands, uint? defaultCommandIndex = null, uint? cancelCommandIndex = null);
	}
}
