using Interfaces;
using System;
using System.IO;

namespace Editor
{
    internal class ExceptionChecker : IExceptionChecker
    {
        public void BaseDirectoryExceptions(string path)
        {
            if (File.Exists(path))
                throw new Exception($"Can't operate with {path} using command designed for directories.");
            if (!Directory.Exists(path))
                throw new Exception($"Can't find directory {path}");
            
        }
        public void BaseFileExceptions(string path)
        {
            if (Directory.Exists(path))
                throw new Exception($"Can't operate with {path} using command designed for files.");
            if (!File.Exists(path))
                throw new Exception($"Can't find file {path}.");
        }
        public void DirectoryAlreadyExistsException(string path)
        {
            if (Directory.Exists(path))
                throw new Exception($"Directory {path} already exists");
        }
        public void FileAlreadyExistsException(string path)
        {
            if (File.Exists(path))
                throw new Exception($"File {path} already exists");
        }
    }
}
