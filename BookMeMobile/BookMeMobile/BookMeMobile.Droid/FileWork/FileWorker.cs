using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BookMeMobile.Droid.FileWork;
using BookMeMobile.Interface;
using BookMeMobile.Resources;

[assembly: Xamarin.Forms.Dependency(typeof(FileWorker))]

namespace BookMeMobile.Droid.FileWork
{
    public class FileWorker : IFileWorker
    {
        private static readonly object LockThis = new object();

        public Task DeleteAsync(string fileName)
        {
            return Task.Run(() => File.Delete(this.GetFilePath(fileName)));
        }

        public Task<bool> ExistsAsync(string fileName)
        {
            string filepath = this.GetFilePath(fileName);
            return Task.Run(() => File.Exists(filepath));
        }

        public async Task<string> LoadTextAsync(string fileName)
        {
            try
            {
                string filepath = this.GetFilePath(fileName);
                lock (LockThis)
                {
                    using (StreamReader reader = File.OpenText(filepath))
                    {
                        return reader.ReadToEndAsync().Result;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task SaveTextAsync(string fileName, string text)
        {
            string filepath = this.GetFilePath(fileName);
            using (StreamWriter writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }

        private string GetFilePath(string fileName)
        {
            return Path.Combine(this.GetDocsPath(), fileName);
        }

        private string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        }
    }
}