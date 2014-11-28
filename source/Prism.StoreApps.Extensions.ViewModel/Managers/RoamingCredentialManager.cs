using System;
using System.Linq;
using Windows.Security.Credentials;
using Microsoft.Practices.Prism.StoreApps.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.Managers
{
	public class RoamingCredentialManager : ICredentialStore
	{
		public void SaveCredentials(string resource, string userName, string password)
		{
			var vault = new PasswordVault();

			RemoveAllCredentialsByResource(resource, vault);

			// Add the new credential 
			var passwordCredential = new PasswordCredential(resource, userName, password);
			vault.Add(passwordCredential);
		}

		private static void RemoveAllCredentialsByResource(string resource, PasswordVault vault)
		{
			try
			{
				// Remove the old credentials for this resource
				var oldCredentials = vault.FindAllByResource(resource);
				foreach (var oldCredential in oldCredentials)
				{
					vault.Remove(oldCredential);
				}
			}
			catch (Exception)
			{
			} // FindAllByResource throws Exception if nothing stored for that resource
		}

		public PasswordCredential GetSavedCredentials(string resource)
		{
			if (String.IsNullOrEmpty(resource))
				return null;

			try
			{
				var vault = new PasswordVault();
				var credentials = vault.FindAllByResource(resource);
				var cred = credentials.FirstOrDefault();
				if (cred != null)
					return vault.Retrieve(resource, cred.UserName);

				return null;
			}
			// The password vault throws System.Exception if no credentials have been stored with this resource.
			catch (Exception)
			{
				return null;
			}
		}

		public void RemoveSavedCredentials(string resource)
		{
			var vault = new PasswordVault();
			RemoveAllCredentialsByResource(resource, vault);
		}
	}
}
