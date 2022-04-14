using Editor;
using ReFileManager;
using System;
using System.IO;
using System.Linq;

namespace Lesson_2._8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager reFileManager = new FileManager();

            FileAndDirEditor editor = new FileAndDirEditor();

            string all = editor.TryFind(@"C:\gdg", ".txt");

            reFileManager.Interact();

        }

        static string TryFind(string path, string param)
        {
            string matchedToParam = string.Empty;

            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(path).ToArray();

            if (dirAndFiles.Length == 0)
                return string.Empty;

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                if (Directory.Exists(dirAndFiles[i]))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dirAndFiles[i]);

                    if (directoryInfo.Name.Contains(param))
                        matchedToParam += $@"{directoryInfo.FullName}+";

                    matchedToParam += TryFind(directoryInfo.FullName, param);
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(dirAndFiles[i]);

                    if (fileInfo.Name.Contains(param))
                        matchedToParam += $@"{fileInfo.FullName}+";
                }
            }
            return matchedToParam;
        }
    }


}
