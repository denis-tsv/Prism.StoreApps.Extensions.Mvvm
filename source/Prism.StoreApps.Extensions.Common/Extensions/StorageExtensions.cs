using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Prism.StoreApps.Extensions.Common.Extensions
{
	public static class StorageExtensions
	{
		[Obsolete("Use Windows.Storage.StorageFolder.TryGetItemAsync(string name)")]
		public static async Task<StorageFile> TryGetFileAsync(this IStorageFolder folder, string name)
		{
			try
			{
				return await folder.GetFileAsync(name);
			}
			catch (FileNotFoundException)
			{
				return null;
			}
		}

		[Obsolete("Use Windows.Storage.StorageFolder.TryGetItemAsync(string name)")]
		public static async Task<StorageFolder> TryGetFolderAsync(this IStorageFolder folder, string name)
		{
			try
			{
				return await folder.GetFolderAsync(name);
			}
			catch (FileNotFoundException)
			{
				return null;
			}
		}

		public static async Task MoveToFolderAsync(this IStorageFolder source, IStorageFolder destination, CreationCollisionOption folderCollisionOption = CreationCollisionOption.OpenIfExists, NameCollisionOption nameCollisionOption = NameCollisionOption.GenerateUniqueName)
		{
			var target = await destination.CreateFolderAsync(source.Name, folderCollisionOption);
			await MoveChildsToFolderAsync(source, target, folderCollisionOption, nameCollisionOption);
		}

		public static async Task MoveChildsToFolderAsync(this IStorageFolder source, IStorageFolder destination, CreationCollisionOption folderCollisionOption = CreationCollisionOption.OpenIfExists, NameCollisionOption nameCollisionOption = NameCollisionOption.GenerateUniqueName)
		{
			IReadOnlyList<StorageFile> childFiles = await source.GetFilesAsync();
			IReadOnlyList<StorageFolder> childFolders = await source.GetFoldersAsync();

			// move files
			foreach (StorageFile storageFile in childFiles)
			{
				await storageFile.MoveAsync(destination, storageFile.Name, nameCollisionOption);
			}

			foreach (StorageFolder storageFolder in childFolders)
			{
				StorageFolder newFolder = await destination.CreateFolderAsync(storageFolder.Name, folderCollisionOption);
				await MoveChildsToFolderAsync(storageFolder, newFolder);
			}

			await source.DeleteAsync();
		}

		public static async Task CopyToFolderAsync(this IStorageFolder source, IStorageFolder destination, CreationCollisionOption folderCollisionOption = CreationCollisionOption.OpenIfExists, NameCollisionOption nameCollisionOption = NameCollisionOption.GenerateUniqueName)
		{
			var target = await destination.CreateFolderAsync(source.Name, folderCollisionOption);
			await CopyChildsToFolderAsync(source, target, folderCollisionOption, nameCollisionOption);
		}

		public static async Task CopyChildsToFolderAsync(this IStorageFolder source, IStorageFolder destination, CreationCollisionOption folderCollisionOption = CreationCollisionOption.OpenIfExists, NameCollisionOption nameCollisionOption = NameCollisionOption.GenerateUniqueName)
		{
			IReadOnlyList<StorageFile> childFiles = await source.GetFilesAsync();
			IReadOnlyList<StorageFolder> childFolders = await source.GetFoldersAsync();

			// copy files
			foreach (StorageFile storageFile in childFiles)
			{
				await storageFile.CopyAsync(destination, storageFile.Name, nameCollisionOption);
			}

			foreach (StorageFolder storageFolder in childFolders)
			{
				StorageFolder newFolder = await destination.CreateFolderAsync(storageFolder.Name, folderCollisionOption);
				await CopyChildsToFolderAsync(storageFolder, newFolder);
			}
		}
	}
}
