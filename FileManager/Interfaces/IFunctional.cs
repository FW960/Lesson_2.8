namespace Interfaces
{
    interface IFunctional
    {
        public void Create(string Path);

        public void Delete(string Path);

        public void Rename(string Path);

        public void Copy(string SourcePath, string DestinationPath);

        /// <summary>
        /// Show in console directory or file memory using in KB.
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public int MemoryUsed(string Path);

        /// <summary>
        /// Enumerates and shows in parent directory all files and directories with specific name parametrs.
        /// </summary>
        /// <param name="Params"></param>
        public string[] TryFind(string Path, string Params);

        /// <summary>
        /// Shows information in txt file about amount of string, paragraphs, letters and etc.
        /// </summary>
        /// <param name="Path"></param>
        public string TxtFileInfo(string Path);

        /// <summary>
        /// Changes attributes of specific file or directory.
        /// </summary>
        /// <param name="Path"></param>
        public void ChangeAttributes(string Path);
    }
}
