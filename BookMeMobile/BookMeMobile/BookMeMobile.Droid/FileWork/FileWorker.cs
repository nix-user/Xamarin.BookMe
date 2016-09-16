using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookMeMobile.Droid.FileWork;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.Droid.FileWork
{
    public class FileWorker : IFileWork
    {
        private static readonly object LockThis = new object();
        private readonly string filename = Resources.FileName;

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

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(this.GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync()
        {
            string filepath = this.GetFilePath();
            lock (LockThis)
            {
                using (StreamReader reader = File.OpenText(filepath))
                {
                    return reader.ReadToEndAsync().Result;
                }
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
            return Path.Combine(this.GetDocsPath(), this.filename);
        }

        private string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}