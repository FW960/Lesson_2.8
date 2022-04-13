using Editor;
using Interfaces;
using System;
using System.IO;
using System.Linq;

namespace ReFileManager
{
    public class FileManager : IVisualizer
    {
        private string lastDirSeen;

        public void Interact()
        {
            string userChoice = string.Empty;

            FileAndDirEditor fileAndDirEditor = new FileAndDirEditor();

            Console.BackgroundColor = ConsoleColor.Blue;

            Console.Clear();

            Console.SetCursorPosition(Console.WindowLeft, 0);
            do
            {
                string userCommand;

                string[] userCommands = new string[3];

                try
                {
                    int checkCode = 0;

                    userCommand = Console.ReadLine(); // Вся эта огромная логика нужна для того, чтобы команды, файлы и директории можно было бы разделять одним пробелом, если там допустим будет папка C:\Program Files (x86), в которой несколько пробелов.

                    userCommands = userCommand.Split(':');

                    string[] splitUserCommands = userCommands[0].Split(' ');

                    userCommands[0] = splitUserCommands[0];

                    if (userCommands.Length > 1)
                    {
                        userCommands[1] = ($"{splitUserCommands[1]}:{userCommands[1]}");

                    }
                    if (userCommands.Length > 2)
                    {
                        userCommands[2] = userCommands[1].ToString()[userCommands[1].Length - 1] + userCommands[2];

                        string sourcePath = string.Empty;

                        for (int i = 0; i <= userCommands[1].ToString().Length - 3; i++)
                        {
                            sourcePath += userCommands[1].ToString()[i];
                        }
                        userCommands[1] = sourcePath;

                        checkCode = 1;
                    }
                    string tryParamSplit = string.Empty;

                    try
                    {
                        tryParamSplit = userCommands[1].Split(' ').Last();

                        if (tryParamSplit != userCommands[1] && checkCode == 0)
                        {
                            string temporaryFirstCommand = userCommands[0];

                            string temporarySecondCommand = userCommands[1];

                            userCommands = new string[3];

                            if (temporarySecondCommand.Contains(tryParamSplit))
                            {
                                string[] splittedSecondCommandWithParam = temporarySecondCommand.Split(' ');

                                for (int i = 0; i < splittedSecondCommandWithParam.Length; i++)
                                {
                                    if(i == splittedSecondCommandWithParam.Length - 1)
                                        break;

                                    userCommands[1] += $"{splittedSecondCommandWithParam[i]} ";
                                }

                            }

                            userCommands[0] = temporaryFirstCommand;

                            userCommands[2] = tryParamSplit;
                        }
                    }
                    catch
                    {
                    }


                }
                catch
                {
                    Console.WriteLine("Unknow command");
                    continue;
                }

                switch (userCommands[0])
                {
                    case "ls":
                        try
                        {
                            Show(userCommands[1]);

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "cpDir":
                        try
                        {
                            fileAndDirEditor.CopyDir(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"Directory {file.Name} sucessefully copied to {userCommands[2]}\{file.Name}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "cpFile":
                        try
                        {
                            fileAndDirEditor.CopyFile(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"File {file.Name} sucessefully copied to {userCommands[2]}\{file.Name}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "cr":
                        try
                        {
                            fileAndDirEditor.Create(userCommands[1]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{file.Name} sucessefully created.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "reDir":
                        try
                        {
                            fileAndDirEditor.RenameDir(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Directory {file.Name} sucessefully renamed to {userCommands[2]}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "reFile":
                        try
                        {
                            fileAndDirEditor.RenameFile(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"File {file.Name} sucessefully renamed to {userCommands[2]}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "rmDir":
                        try
                        {
                            fileAndDirEditor.DeleteDir(userCommands[1]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Directory {userCommands[1]} sucessefully deleted.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "rmFile":
                        try
                        {
                            fileAndDirEditor.DeleteFile(userCommands[1]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"File {userCommands[1]} sucessefully deleted.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "memDir":
                        try
                        {
                            double memoryUsed = fileAndDirEditor.DirMemoryUsed(userCommands[1]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{memoryUsed}kb used by {file.Name}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "memFile":
                        try
                        {
                            double memoryUsed = fileAndDirEditor.FileMemoryUsed(userCommands[1]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{memoryUsed}kb used by {file.Name}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "find":
                        try
                        {
                            string[] allMatches = fileAndDirEditor.TryFind(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            Console.WriteLine($"All matched files and directories names to parametr: {userCommands[2]}");

                            for (int i = 0; i < allMatches.Length; i++)
                            {
                                Console.WriteLine(allMatches[i]);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "txtInfo":
                        try
                        {
                            int[] txtFileParams = fileAndDirEditor.TxtFileInfo(userCommands[1]);

                            Show(lastDirSeen);

                            Console.WriteLine($"Number of symbols: {txtFileParams[0]}"); Console.WriteLine($"Number of symbols with space: {txtFileParams[1]}");
                            Console.WriteLine($"Number of words: {txtFileParams[2]}"); Console.WriteLine($"Number of string {txtFileParams[3]}");
                            Console.WriteLine($"Number of paragraphs: {txtFileParams[4]}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "chAtt":
                        try
                        {
                            fileAndDirEditor.ChangeAttributes(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Attributes of {file.Name} change to {userCommands[2]}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "mvFile":
                        try
                        {
                            fileAndDirEditor.MoveFile(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"File {file.Name} sucessesfully moved to {userCommands[2]}\{file.Name}.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "mvDir":
                        try
                        {
                            fileAndDirEditor.MoveDir(userCommands[1], userCommands[2]);

                            Show(lastDirSeen);

                            Console.WriteLine("Directory sucessesfully moved.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "back":
                        if (lastDirSeen != null)
                        {
                            try
                            {
                                DirectoryInfo file = new DirectoryInfo(lastDirSeen);

                                Show(file.Parent.ToString());
                            }
                            catch
                            {
                                Console.WriteLine($"Can't see root of the {lastDirSeen}");
                                continue;
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;

                }

            } while (userChoice != "Выход");

        }

        public void Show(string? sourcePath)
        {
            if (sourcePath == null)
                return;

            string[] sourceDir = Directory.EnumerateFileSystemEntries(sourcePath).ToArray();

            Console.Clear();

            try
            {
                string rootPath = Directory.GetParent(sourcePath).ToString();

                string[] rootDir = Directory.EnumerateFileSystemEntries(rootPath).ToArray();

                if ((rootPath + "\\") == sourcePath)
                {
                    for (int i = 0; i < rootDir.Length; i++)
                    {
                        Console.WriteLine(rootDir[i]);
                    }
                    return;
                }

                string toPrint = "Root Directory:" + String.Format("{0," + Console.WindowWidth / 2 + "}", "Source Directory:");

                Console.Write(toPrint);

                Console.WriteLine();

                for (int i = 0; i < sourceDir.Length + rootDir.Length; i++)
                {
                    if (sourceDir.Length > rootDir.Length)
                    {
                        if (i > rootDir.Length - 1)
                        {
                            Console.Write(string.Empty.PadLeft(103, ' '));
                            Console.WriteLine(sourceDir[i]);
                            continue;
                        }

                        Console.Write(rootDir[i].PadRight(103, ' '));
                        Console.WriteLine(sourceDir[i]);
                    }
                    else if (sourceDir.Length < rootDir.Length)
                    {
                        if (i > sourceDir.Length - 1)
                        {
                            Console.WriteLine(rootDir[i]);
                            continue;
                        }
                        Console.Write(rootDir[i].PadRight(103, ' '));
                        Console.WriteLine(sourceDir[i]);
                    }
                    else if (sourceDir.Length == rootDir.Length)
                    {
                        Console.Write(rootDir[i].PadRight(103, ' '));
                        Console.WriteLine(sourceDir[i]);
                    }
                }
            }
            catch (NullReferenceException)
            {
                for (int i = 0; i < sourceDir.Length; i++)
                {
                    Console.WriteLine(sourceDir[i]);
                }
            }
            catch (IndexOutOfRangeException) { }
            catch { Console.WriteLine("Directory doesn't exist."); }
            finally
            {
                Console.WriteLine("".PadRight(0, ' '));

                lastDirSeen = sourcePath;
            }
        }
    }
}
