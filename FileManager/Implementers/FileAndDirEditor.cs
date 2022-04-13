using Interfaces;
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

            if (param == "Normal")
            {
                fileInfo.Attributes = FileAttributes.Normal;
            }
            else if (param == "ReadOnly")
            {
                fileInfo.Attributes = FileAttributes.ReadOnly;
            }
            else if (param == "Hidden")
            {
                fileInfo.Attributes = FileAttributes.Hidden;
            }
            else if (param == "System")
            {
                fileInfo.Attributes = FileAttributes.System;
            }
        }

        public void Create(string path)
        {
            FileInfo fileInfo = new FileInfo(path);

            if (fileInfo.Extension != "")
                File.Create(fileInfo.FullName);
            else
                Directory.CreateDirectory(fileInfo.FullName);
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

        public string TryFind(string path, string param)
        {
            string matchedParam = string.Empty;

            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(path).ToArray();

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                FileInfo fileInfo = new FileInfo(dirAndFiles[i]);

                if (Directory.Exists(dirAndFiles[i]))
                {
                    if (fileInfo.Name.Contains(param))
                    {
                        if (matchedParam == string.Empty)
                        {
                            matchedParam = dirAndFiles[i];
                            continue;
                        }
                        matchedParam = $"{matchedParam}+{dirAndFiles[i]}";

                        string isEmptyDir = TryFind(dirAndFiles[i], param);

                        if (isEmptyDir == string.Empty)
                            continue;

                        matchedParam = $"{matchedParam}+{isEmptyDir}";

                    }
                    else
                    {
                        string isEmptyDir = TryFind(dirAndFiles[i], param);

                        if (isEmptyDir == string.Empty)
                            continue;

                        if (matchedParam == string.Empty) 
                        {
                            matchedParam = isEmptyDir;
                            continue;
                        }
                        matchedParam = $"{matchedParam}+{isEmptyDir}";
                    }
                }
                if (fileInfo.Name.Contains(param))
                {
                    if (matchedParam == string.Empty) 
                    {
                        matchedParam = dirAndFiles[i];
                        continue;
                    }
                    matchedParam = $"{matchedParam}+{dirAndFiles[i]}"; continue;
                }
            }
            return matchedParam;
        }

        public string TxtFileInfo(string path)
        {
            throw new NotImplementedException();
        }
        public void CopyDir(string sourcePath, string destinationPath)
        {
            string[] dirAndFiles = Directory.EnumerateFileSystemEntries(sourcePath).ToArray();

            for (int i = 0; i < dirAndFiles.Length; i++)
            {
                if (Directory.Exists(dirAndFiles[i]))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(dirAndFiles[i]);

                    string copiedDirectoryPath = $@"{destinationPath}\{directoryInfo.Name}";

                    Directory.CreateDirectory(copiedDirectoryPath);
                    CopyDir(dirAndFiles[i], copiedDirectoryPath);
                }

                else
                {
                    FileInfo fileInfo = new FileInfo(dirAndFiles[i]);

                    string copiedFile = $@"{destinationPath}\{fileInfo.Name}";

                    if (File.Exists(copiedFile))
                        File.Delete(copiedFile);
                    File.Copy(dirAndFiles[i], copiedFile);
                }
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
    }
}



