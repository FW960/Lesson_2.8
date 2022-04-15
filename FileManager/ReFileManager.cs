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

        ExceptionChecker exch = new ExceptionChecker();
        
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

                    userCommand = Console.ReadLine(); // Вся эта огромная логика нужна для того, чтобы команды, файлы и директории можно было корректно сплитовать.
                                                      // К примеру если там будет папка C:\Program Files (x86), в которой несколько пробелов.
                    userCommands = userCommand.Split(':');

                    string[] splitUserCommands = userCommands[0].Split(' ');

                    userCommands[0] = splitUserCommands[0];

                    if (userCommands.Length > 1)
                    {
                        userCommands[1] = ($"{splitUserCommands[1]}:{userCommands[1]}");

                    }
                    if (userCommands.Length > 2)
                    {
                        userCommands[2] = $"{userCommands[1].ToString()[userCommands[1].Length - 1]}:{userCommands[2]}";

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
                        tryParamSplit = userCommands[1].Split('*').Last(); // Задача параметров для поиска в методе TryFind идет через '*'.
                                                                           // Также для того, чтобы переименовать директорию или файл пишется '*', а затем новое название без полного указания пути.
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
                                    if (i == splittedSecondCommandWithParam.Length - 2)
                                    {
                                        userCommands[1] += $"{splittedSecondCommandWithParam[i]}";
                                        continue;
                                    }
                                        

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
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            Show(userCommands[1]);

                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
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

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"Directory {file.Name} sucessefully copied to {userCommands[2]}\{file.Name}");
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

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"File {file.Name} sucessefully copied to {userCommands[2]}\{file.Name}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "touch":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.MakeFile(userCommands[1]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{file.Name} sucessefully created");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "mkDir":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.MakeDirectory(userCommands[1]);

                            DirectoryInfo dir = new DirectoryInfo(userCommands[1]);

                            Console.WriteLine($"{dir.Name} sucessefully created");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "reDir":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new Exception();

                            fileAndDirEditor.RenameDir(userCommands[1], userCommands[2]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Directory {file.Name} sucessefully renamed to {userCommands[2]}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "reFile":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.RenameFile(userCommands[1], userCommands[2]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"File {file.Name} sucessefully renamed to {userCommands[2]}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "rmDir":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.DeleteDir(userCommands[1]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Directory {userCommands[1]} sucessefully deleted");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "rmFile":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.DeleteFile(userCommands[1]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"File {userCommands[1]} sucessefully deleted");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "memDir":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            double memoryUsed = fileAndDirEditor.DirMemoryUsed(userCommands[1]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{memoryUsed}kb used by {file.Name}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "memFile":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            double memoryUsed = fileAndDirEditor.FileMemoryUsed(userCommands[1]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"{memoryUsed}kb used by {file.Name}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "tryFind":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            string[] allMatches = fileAndDirEditor.TryFind(userCommands[1].ToString(), userCommands[2]).Split('+');

                            if (allMatches[0] == "")
                            {
                                Console.WriteLine("No matches found");
                                break;
                            }

                            Console.WriteLine($"{allMatches.Length-1} matched files and directories to parametr: {userCommands[2]}");

                            for (int i = 0; i < allMatches.Length; i++)
                            {
                                Console.WriteLine(allMatches[i]);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "txtInfo":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            int[] txtFileParams = fileAndDirEditor.TxtFileInfo(userCommands[1]);

                            Console.WriteLine($"Number of symbols: {txtFileParams[0]}"); Console.WriteLine($"Number of symbols with space: {txtFileParams[1]}");
                            Console.WriteLine($"Number of words: {txtFileParams[2]}"); Console.WriteLine($"Number of string {txtFileParams[3]}");
                            Console.WriteLine($"Number of paragraphs: {txtFileParams[4]}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "chAtt":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.ChangeAttributes(userCommands[1], userCommands[2]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine($"Attributes of {file.Name}changed to {userCommands[2]}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "mvFile":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.MoveFile(userCommands[1], userCommands[2]);

                            FileInfo file = new FileInfo(userCommands[1]);

                            Console.WriteLine(@$"File {file.Name} sucessesfully moved to {userCommands[2]}\{file.Name}");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case "mvDir":
                        try
                        {
                            if (!userCommands[1].Contains(':'))
                                throw new IndexOutOfRangeException();

                            fileAndDirEditor.MoveDir(userCommands[1], userCommands[2]);

                            Console.WriteLine("Directory sucessesfully moved");
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Type directory path correctly");
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
                    case "clear": Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;

                }

            } while (userChoice != "Выход");

        }

        public void Show(string path)
        {
            exch.BaseDirectoryExceptions(path);

            if (path == null)
                return;

            string[] sourceDir = Directory.EnumerateFileSystemEntries(path).ToArray();

            try
            {
                string rootPath = Directory.GetParent(path).ToString();

                string[] rootDir = Directory.EnumerateFileSystemEntries(rootPath).ToArray();

                if ((rootPath + "\\") == path)
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

                lastDirSeen = path;
            }
        }
    }
}
