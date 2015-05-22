﻿#if __PLATFORM__
using System;
using System.IO;
using System.Linq;


#if __IOS__
using UIKit;
using Foundation;
#endif

namespace Acr.IO {

    public class FileSystemImpl : IFileSystem {

        public FileSystemImpl() {
#if WINDOWS_PHONE
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
            var ctx = Android.App.Application.Context;
            this.AppData = new Directory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var cacheDirs = ctx.GetExternalCacheDirs();
            if (cacheDirs != null && cacheDirs.Length > 0) {
                var cachePath = cacheDirs.First().AbsolutePath;
                this.Cache = new Directory(cachePath);
                this.Temp = new Directory(cachePath);
            }
            var ext = ctx.GetExternalFilesDir(null);
            if (ext != null)
                this.Public = new Directory(ext.AbsolutePath);
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