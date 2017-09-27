using System;
using System.Collections.Generic;
using System.IO;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";
            ShowLargeFilesWithoutLinq(path);

        }

        private static void ShowLargeFilesWithoutLinq(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files= directory.GetFiles();
            Array.Sort(files, new FileInfoComparer());
            for(int i=0;i<10;i++)
            {
                FileInfo file = files[i];
                Console.WriteLine($"{file.Length,-15:N0} : {file.Name} ");
            }
        }
    }
    public class FileInfoComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return y.Length.CompareTo(x.Length);
        }
    }
}
