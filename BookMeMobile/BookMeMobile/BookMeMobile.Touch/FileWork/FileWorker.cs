﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookMeMobile.Interface;
using BookMeMobile.Touch.FileWork;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileWorker))]

namespace BookMeMobile.Touch.FileWork
{
    public class FileWorker : IFileWork
    {
        private const string Filename = "14214124421";

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
                return await reader.ReadToEndAsync();
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
            return Path.Combine(this.GetDocsPath(), Filename);
        }

        private string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
    }
}