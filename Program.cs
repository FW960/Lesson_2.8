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

            string a = editor.TryFind(@"C:\DRIVER", "txt");

            string[] b = a.Split('+');

        }
    }

}
