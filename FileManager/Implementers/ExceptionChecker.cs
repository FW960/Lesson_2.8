using Interfaces;

namespace Editor
{
    internal class ExceptionChecker : IExceptionChecker
    {
        public void DirectoryExceptionCheck(string Path)
        {
            throw new System.NotImplementedException();
        }

        public void ExceptionCheck(string SourcePath, string DestinationPath)
        {
            throw new System.NotImplementedException();
        }
        public void FileExceptionCheck(string Path)
        {
            throw new System.NotImplementedException();
        }
    }
}
