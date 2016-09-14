using System;
using System.Threading.Tasks;
using BookMeMobile.Interface;
using BookMeMobile.WinPhone;
using Windows.Storage;
using Xamarin.Forms;


[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.WinPhone
{
    public class FileWorker : IFileWork
    {
        private const string Filename = "14214124421";

        public async Task DeleteAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.GetFileAsync(Filename);
            await storageFile.DeleteAsync();
        }

        public async Task<bool> ExistsAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                await localFolder.GetFileAsync(Filename);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<string> LoadTextAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = await localFolder.GetFileAsync(Filename);
            string text = await FileIO.ReadTextAsync(helloFile);
            return text;
        }

        public async Task SaveTextAsync(string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = await localFolder.CreateFileAsync(Filename,
                                                 CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(helloFile, text);
        }
    }
}