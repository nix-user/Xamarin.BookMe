﻿using System;
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
            string filepath = this.GetFilePath(fileName);
            using (StreamReader reader = File.OpenText(filepath))
            {
                return reader.ReadToEndAsync().Result;
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
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}