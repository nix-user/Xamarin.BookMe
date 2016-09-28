using System;
using System.Threading.Tasks;
using BookMeMobile.Interface;
using BookMeMobile.Resources;
using BookMeMobile.WinPhone;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.WinPhone
{
    public class FileWorker : IFileWorker
    {
        public async Task DeleteAsync(string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.GetFileAsync(fileName);
            await storageFile.DeleteAsync();
        }

        public async Task<bool> ExistsAsync(string fileName)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                await localFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string> LoadTextAsync(string fileName)
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile tokenFile = await localFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(tokenFile);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task SaveTextAsync(string fileName, string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile tokenFile = await localFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tokenFile, text);
        }
    }
}