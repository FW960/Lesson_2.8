namespace Interfaces
{
    interface IExceptionChecker
    {
        void DirectoryAlreadyExistsException(string path);
        void FileAlreadyExistsException(string path);
        void BaseFileExceptions(string path);
        void BaseDirectoryExceptions(string path);

    }
}