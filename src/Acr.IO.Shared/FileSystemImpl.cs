﻿#if __PLATFORM__
using System;
using System.IO;
#if __IOS__
using UIKit;
using Foundation;
#endif

namespace Acr.IO {

    public class FileSystemImpl : IFileSystem {

        public FileSystemImpl() {
#if __WINDOWS__
            var path = Windows.Storage.ApplicationData.Current.LocalFolder.Path;
            this.AppData = new Directory(path);
            this.Cache = new Directory(Path.Combine(path, "Cache"));
            this.Public = new Directory(Path.Combine(path, "Public"));
            this.Temp = new Directory(Path.Combine(path, "Temp"));
#elif __IOS__
            var documents = UIDevice.CurrentDevice.CheckSystemVersion(8, 0)
                ? NSFileManager.DefaultManager.GetUrls (NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            var library = Path.Combine(documents, "..", "Library");
            this.AppData = new Directory(library);
            this.Cache = new Directory(Path.Combine(library, "Caches"));
            this.Temp = new Directory(Path.Combine(documents, "..", "tmp"));
            this.Public = new Directory(documents);
#elif __ANDROID__
            this.AppData = new Directory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            this.Cache = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Temp = new Directory(Android.App.Application.Context.CacheDir.AbsolutePath);
            this.Public = new Directory(Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath);
#endif
        }

        public IDirectory AppData { get; private set; }
        public IDirectory Cache { get; private set; }
        public IDirectory Public { get; private set; }
        public IDirectory Temp { get; private set; }


        public IDirectory GetDirectory(string path) {
            return new Directory(new DirectoryInfo(path));
        }


        public IFile GetFile(string fileName) {
            return new File(new FileInfo(fileName));
        }
    }
}
#endif