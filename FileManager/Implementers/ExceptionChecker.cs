using Interfaces;
using System.IO;

namespace Editor
{
    internal class ExceptionChecker : IExceptionChecker
    {
        public void DirectoryExceptionCheck(string path)
        {
            if (!Directory.Exists(path))
            {

            }
        }

        public void DirectoryExceptionCheck(string sourcePath, string destinationPath)
        {
            throw new System.NotImplementedException();
        }
        public void FileExceptionCheck(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}
