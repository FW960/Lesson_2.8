﻿using Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Editor
{
    internal sealed class FileAndDirEditor : IFunctional
    {
        public void ChangeAttributes(string path, string param)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (string.Compare(param, "Normal", StringComparison.OrdinalIgnoreCase) == 0)
            {
                fileInfo.Attributes = FileAttributes.Normal;
            }
            else if (string.Compare(param, "ReadOnly", StringComparison.OrdinalIgnoreCase) == 0)
            {
                fileInfo.Attributes = FileAttributes.ReadOnly;
            }
            else if (string.Compare(param, "Hidden", StringComparison.OrdinalIgnoreCase) == 0)
            {
                fileInfo.Attributes = FileAttributes.Hidden;
            }
            else if (string.Compare(param, "System", StringComparison.OrdinalIgnoreCase) == 0)
            {
                fileInfo.Attributes = FileAttributes.System;
            }
        }

        public void Create(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Extension != "")
            {
                FileStream fs = new FileStream(path, FileMode.CreateNew);

                fs.Close();
            }
            else
            {
                Directory.CreateDirectory(fileInfo.FullName);
            }

        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
        public void DeleteDir(string path)
        {
            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(path).ToArray();

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                try
                {
                    File.Delete(dirAndFiles[i]);
                }
                catch
                {
                    DeleteDir(dirAndFiles[i]);
                }
            }
            Directory.Delete(path);
        }

        public double FileMemoryUsed(string path)
        {
            FileInfo file = new FileInfo(path);

            double memoryUsed = Convert.ToDouble(file.Length);

            return memoryUsed / 1000;
        }

        /*public string[] TryFind(string path, string param)
        {
            return TryFind(path, param, true).Split('+');
        }*/

        public int[] TxtFileInfo(string path)
        {
            FileStream fs = File.OpenRead(path);

            int[] txtFileParams = new int[5];

            string fileText = File.ReadAllText(path);

            int numOfWords = 0;

            int numOfStrings = 0;

            int numOfParagraphs = fileText.Split(Environment.NewLine + Environment.NewLine).Length;

            int numOfSymbolsWithSpace = 0;

            int numOfSymbols = 0;

            string[] strings = fileText.Split(Environment.NewLine);

            int lengthOfStringsArray = strings.Length;

            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i] == "")
                    continue;

                numOfSymbolsWithSpace += strings[i].Length;
            }

            numOfSymbolsWithSpace += strings.Length;

            for (int i = 0; i < strings.Length; i++)
            {
                if (strings[i] == "")
                    continue;

                numOfStrings++;
            }

            string[] words = fileText.Split(' ');

            string allWords = string.Empty;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Contains(Environment.NewLine))
                {
                    string[] newWords = words[i].Split(Environment.NewLine);

                    for (int j = 0; j < newWords.Length; j++)
                    {
                        if (newWords[j] == "")
                            continue;

                        allWords += $"{newWords[j]} ";
                    }
                    continue;
                }
                allWords += $"{words[i]} ";
            }

            words = allWords.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "")
                    continue;

                numOfWords++;
            }

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == "")
                    continue;

                numOfSymbols += words[i].Length;
            }

            txtFileParams[0] = numOfSymbols;

            txtFileParams[1] = numOfSymbolsWithSpace;

            txtFileParams[2] = numOfWords;

            txtFileParams[3] = numOfStrings;

            txtFileParams[4] = numOfParagraphs;

            fs.Close();

            return txtFileParams;
        }
        public void CopyDir(string sourcePath, string destinationPath)
        {
            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(sourcePath).ToArray();

            DirectoryInfo destinationDirWhereToCopy = new DirectoryInfo(destinationPath);

            DirectoryInfo sourceDirFromWhereToCopy = new DirectoryInfo(sourcePath);

            if (destinationDirWhereToCopy.Parent == null)
                destinationPath = $@"{destinationPath}{sourceDirFromWhereToCopy.Name}";
            else
            destinationPath = $@"{destinationPath}\{sourceDirFromWhereToCopy.Name}";

            Directory.CreateDirectory(destinationPath);

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                FileInfo toCopy = new FileInfo(dirAndFiles[i]);

                if (toCopy.Extension != "")
                    File.Copy(dirAndFiles[i], $@"{destinationPath}\{toCopy.Name}");
                else
                    CopyDir(dirAndFiles[i], destinationPath);
            }
        }

        public void RenameDir(string dirPath, string renamedDir)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);

            string rootDirectoryName = directoryInfo.Parent.ToString();

            renamedDir = $@"{rootDirectoryName}\{renamedDir}";

            Directory.CreateDirectory(renamedDir);

            CopyDir(dirPath, renamedDir);

            DeleteDir(dirPath);

        }

        public void RenameFile(string filePath, string renamedFile)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string fileSourceDirectory = Directory.GetParent(filePath).FullName;

            string fileExtension = '.' + fileInfo.FullName.Split('.').Last();

            renamedFile = @$"{fileSourceDirectory}\{renamedFile}{fileExtension}";

            File.Copy(filePath, renamedFile);

            File.Delete(filePath);
        }

        public void CopyFile(string sourcePath, string destinationPath)
        {
            File.Copy(sourcePath, destinationPath);
        }

        public double DirMemoryUsed(string path)
        {
            double totalMemoryUsed = 0;

            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(path).ToArray();

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                if (Directory.Exists(dirAndFiles[i]))
                {
                    totalMemoryUsed += DirMemoryUsed(dirAndFiles[i]); continue;
                }
                else if (File.Exists(dirAndFiles[i]))
                {
                    totalMemoryUsed += FileMemoryUsed(dirAndFiles[i]);
                }

            }
            return totalMemoryUsed;
        }

        public string TryFind(string path, string param)
        {
            DirectoryInfo dirName = new DirectoryInfo(path);

            if (dirName.Parent != null)
            {
                DirectoryInfo sourceDirPath = new DirectoryInfo(dirName.Parent.ToString());

                if (sourceDirPath.Root.ToString() == sourceDirPath.FullName.ToString())
                    path = $@"{sourceDirPath.FullName}{dirName.Name}";
                else
                    path = $@"{sourceDirPath.FullName}\{dirName.Name}";
            }

            string matchedToParam = string.Empty;

            try
            {
                string[] dirAndFiles = Directory.EnumerateFileSystemEntries(path).ToArray();
                if (dirAndFiles.Length == 0)
                    return string.Empty;

                for (int i = 0; i < dirAndFiles.Length; i++)
                {
                    FileInfo file = new FileInfo(dirAndFiles[i]);

                    FileInfo sourceDir = new FileInfo(file.DirectoryName);

                    string newPath = $@"{sourceDir.FullName}\{file.Name}";

                    if (Directory.Exists(newPath))
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
            catch (SystemException)
            {
                return string.Empty;
            }


        }


        public void MoveDir(string sourcePath, string destinationPath)
        {
            CopyDir(sourcePath, destinationPath);

            DeleteDir(sourcePath);
        }

        public void MoveFile(string sourcePath, string destinationPath)
        {
            FileInfo fileInfo = new FileInfo(sourcePath);

            destinationPath = $@"{destinationPath}\{fileInfo.Name}";

            File.Copy(sourcePath, destinationPath); File.Delete(sourcePath);

        }
    }
}



