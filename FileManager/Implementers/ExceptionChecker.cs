using Interfaces;

namespace Editor
{
    internal class ExceptionChecker : IExceptionChecker
    {
        public void DirectoryExceptionCheck(string path)
        {
            throw new System.NotImplementedException();
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
