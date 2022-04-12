namespace Interfaces
{
    interface IExceptionChecker
    {
        void ExceptionCheck(string SourcePath, string DestinationPath);
        void FileExceptionCheck(string Path);
        void DirectoryExceptionCheck(string Path);

    }
}