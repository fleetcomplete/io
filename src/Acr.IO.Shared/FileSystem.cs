﻿using System;


namespace Acr.IO {

    public static class FileSystem {

        private static IFileSystem instance;
        private static readonly object syncLock = new object();

#if __PLATFORM__
        public static void Init() { }
#else
        [Obsolete("This is the PCL version of Init.  You must call this in your platform project, not here!")]
        public static void Init() {
            throw new Exception("This is the PCL version of Init.  You must call this in your platform project, not here!");
        }
#endif

        public static IFileSystem Instance {
            get {
                if (instance == null) {
                    lock (syncLock) {
                        if (instance == null) {
#if __PLATFORM__
                            instance = new FileSystemImpl();
#else
                            throw new Exception("Platform implementation not found.  Have you added a nuget reference to your platform project?");
#endif
                        }
                    }
                }
                return instance;
            }
            set { instance = value; }
        }
    }
}