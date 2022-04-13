namespace Interfaces
{
    interface IExceptionChecker
    {
        void DirectoryExceptionCheck(string sourcePath, string destinationPath);
        void FileExceptionCheck(string path);
        void DirectoryExceptionCheck(string path);

    }
}