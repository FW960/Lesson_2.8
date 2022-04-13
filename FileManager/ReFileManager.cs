using Interfaces;
using System;
using System.IO;
using System.Linq;

namespace ReFileManager
{
    public class FileManager : IVisualizer
    {
        public string Pad { get; private set; }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

        public void Show(string sourcePath)
        {
            string[] sourceDir = Directory.EnumerateFileSystemEntries(sourcePath).ToArray();

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

                string toPrint = "Root Directory:" + String.Format("{0," + Console.WindowWidth / 3 + "}", "Source Directory:");

                Console.Write(toPrint);

                Console.WriteLine();

                for (int i = 0; i < sourceDir.Length + rootDir.Length; i++)
                {
                    if (sourceDir.Length > rootDir.Length)
                    {
                        if (i > rootDir.Length - 1)
                        {
                            Console.Write(string.Empty.PadLeft(38, ' '));
                            Console.WriteLine(sourceDir[i]);
                            continue;
                        }

                        Console.Write(rootDir[i].PadRight(38, ' '));
                        Console.WriteLine(sourceDir[i]);
                    }
                    else if (sourceDir.Length < rootDir.Length)
                    {
                        if (i > sourceDir.Length - 1)
                        {
                            Console.WriteLine(rootDir[i]);
                            continue;
                        }
                        Console.Write(rootDir[i].PadRight(38, ' '));
                        Console.WriteLine(sourceDir[i]);
                    }
                    else if (sourceDir.Length == rootDir.Length)
                    {
                        Console.Write(rootDir[i].PadRight(38, ' '));
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
            catch (IndexOutOfRangeException)
            {
            }

        }
    }
}
