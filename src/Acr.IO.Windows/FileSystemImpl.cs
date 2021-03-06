﻿using System;
using Windows.Storage;


namespace Acr.IO {

    public class FileSystemImpl : IFileSystem {

        public FileSystemImpl() {
            var path = ApplicationData.Current.LocalFolder.Path;
            //this.AppData = new Directory(path);
            //this.Cache = new Directory(Path.Combine(path, "Cache"));
            //this.Public = new Directory(Path.Combine(path, "Public"));
            //this.Temp = new Directory(Path.Combine(path, "Temp"));
            //StorageFolder.GetFolderFromPathAsync("").AsTask().ContinueWith(x => {
            //});
        }

        public IDirectory AppData { get; private set; }
        public IDirectory Cache { get; private set; }
        public IDirectory Public { get; private set; }
        public IDirectory Temp { get; private set; }


        public IDirectory GetDirectory(string path) {
            return null;
            //return new Directory(new DirectoryInfo(path));
        }


        public IFile GetFile(string path) {
            return null;
            //return new File(new FileInfo(fileName));
        }
    }
}
