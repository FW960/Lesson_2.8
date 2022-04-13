using Editor;
using ReFileManager;
using System;
using System.IO;

namespace Lesson_2._8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileManager reFileManager = new FileManager();

            FileAndDirEditor editor = new FileAndDirEditor();

            string a = @"C:\\Users\\windo\\111\\444";

            Directory.CreateDirectory(a);

            reFileManager.Interact();

        }
    }

}
