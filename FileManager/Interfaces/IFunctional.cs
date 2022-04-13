namespace Interfaces
{
    interface IFunctional
    {
        public void CopyDir(string sourcePath, string destinationPath);
        /// <summary>
        /// Type dir path which one you d'like to rename and renamed version of dir.
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="renamedDir"></param>
        public void RenameDir(string dirPath, string renamedDir);
        /// <summary>
        /// Type path where you d'like to create dir or file. If you type without extinsion - will be created dir.
        /// </summary>
        /// <param name="path"></param>
        public void Create(string path);

        public void DeleteDir(string path);

        public void DeleteFile(string path);
        /// <summary>
        /// Type file path which one you d'like to rename and renamed version of file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="renamedFile"></param>
        public void RenameFile(string filePath, string renamedFile);

        public void CopyFile(string sourcePath, string destinationPath);

        /// <summary>
        /// Shows file memory usage in KB.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public double FileMemoryUsed(string path);

        /// <summary>
        /// Shows directory memory usage in KB.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public double DirMemoryUsed(string path);

        /// <summary>
        /// Enumerates and shows in parent directory all files and directories with specific name parametrs.
        /// </summary>
        /// <param name="params"></param>
        public string TryFind(string path, string param);

        /// <summary>
        /// Shows information in txt file about amount of string, paragraphs, letters and etc.
        /// </summary>
        /// <param name="path"></param>
        public string TxtFileInfo(string path);

        /// <summary>
        /// Type path to file or directory and then attributes you would like to change.
        /// </summary>
        /// <param name="path"></param>
        public void ChangeAttributes(string path, string parametrs);
    }
}
