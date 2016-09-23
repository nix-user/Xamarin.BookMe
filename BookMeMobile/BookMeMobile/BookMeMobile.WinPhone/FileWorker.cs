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
        private readonly string filename = FileResources.FileName;

        public async Task DeleteAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.GetFileAsync(this.filename);
            await storageFile.DeleteAsync();
        }

        public async Task<bool> ExistsAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            try
            {
                localFolder.GetFileAsync(this.filename).GetResults();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<string> LoadTextAsync()
        {
            try
            {
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile tokenFile = localFolder.GetFileAsync(this.filename).GetResults();
                string text = FileIO.ReadTextAsync(tokenFile).GetResults();
                return text;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task SaveTextAsync(string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile tokenFile = await localFolder.CreateFileAsync(this.filename,
                                                 CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tokenFile, text);
        }
    }
}