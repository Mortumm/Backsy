using System;
using System.IO;

namespace BackupProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. Define backup strategy
            // ...
            
            // 2. Identify source and destination paths
            string sourceDirectory = "D:\\";
            string targetDirectory = "C:\\BackupDirectory";
            
            // 3. Set up FileSystemWatcher to monitor changes
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = sourceDirectory;
            watcher.NotifyFilter = NotifyFilters.LastWrite
                                 | NotifyFilters.FileName
                                 | NotifyFilters.DirectoryName;
            watcher.Filter = "*.*";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press 'q' to quit the program.");
            while (Console.Read() != 'q') ;
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            // 4. Backup files when changes detected
            string fileName = e.Name;
            string sourcePath = Path.Combine(sourceDirectory, fileName);
            string targetPath = Path.Combine(targetDirectory, fileName);
            File.Copy(sourcePath, targetPath, true);
            Console.WriteLine("File {0} backed up.", fileName);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            // 4. Backup files when changes detected
            string oldFileName = e.OldName;
            string newFileName = e.Name;
            string sourcePath = Path.Combine(sourceDirectory, newFileName);
            string targetPath = Path.Combine(targetDirectory, newFileName);
            File.Copy(sourcePath, targetPath, true);
            Console.WriteLine("File {0} renamed to {1}. Backup created.", oldFileName, newFileName);
        }
    }
}
