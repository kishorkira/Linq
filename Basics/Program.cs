using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Basics
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\windows";
            ShowLargeFilesWithoutLinq(path);
            Console.WriteLine("**********");
            ShowLargeFilesWithLinq(path);

        }

        private static void ShowLargeFilesWithLinq(string path)
        {
            var query = from file in new DirectoryInfo(path).GetFiles()
                        orderby file.Length descending
                        select file;
            foreach(var file in query.Take(10))
            {
                Console.WriteLine($"{file.Length,-15:N0} : {file.Name} ");
            }
            Console.WriteLine("Method Syntax");

            var query1 = new DirectoryInfo(path).GetFiles()
                        .OrderByDescending(f => f.Length)
                        .Take(10);
                        
            foreach (var file in query1)
            {
                Console.WriteLine($"{file.Length,-15:N0} : {file.Name} ");
            }
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
