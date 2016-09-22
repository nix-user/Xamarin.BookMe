using System;
using System.IO;
using System.Threading.Tasks;
using BookMeMobile.Interface;
using BookMeMobile.Resources;
using BookMeMobile.Touch.FileWork;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.Touch.FileWork
{
    public class FileWorker : IFileWorker
    {
        private readonly string fileName = FileResources.FileName;

        public Task DeleteAsync()
        {
            File.Delete(this.GetFilePath());
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync()
        {
            string filepath = this.GetFilePath();
            bool exists = File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public async Task<string> LoadTextAsync()
        {
            string filepath = this.GetFilePath();
            using (StreamReader reader = File.OpenText(filepath))
            {
                return reader.ReadToEndAsync().Result;
            }
        }

        public async Task SaveTextAsync(string text)
        {
            string filepath = this.GetFilePath();
            using (StreamWriter writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }

        private string GetFilePath()
        {
            return Path.Combine(this.GetDocsPath(), this.fileName);
        }

        private string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}