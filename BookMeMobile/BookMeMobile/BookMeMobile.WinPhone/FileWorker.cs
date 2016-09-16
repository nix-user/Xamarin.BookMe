using System;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using BookMeMobile.WinPhone;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.WinPhone
{
    public class FileWorker : IFileWork
    {
        private readonly string filename = Resources.FileName;

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

        public string LoadText()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = localFolder.GetFileAsync(this.filename).GetResults();
            string text = FileIO.ReadTextAsync(helloFile).GetResults();
            return text;
        }

        public async Task<string> LoadTextAsync()
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = await localFolder.GetFileAsync(this.filename);
            string text = await FileIO.ReadTextAsync(helloFile);
            return text;
        }

        public async Task SaveTextAsync(string text)
        {
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile helloFile = await localFolder.CreateFileAsync(this.filename,
                                                 CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(helloFile, text);
        }
    }
}