using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL;
using Cosmos.System.Graphics;
using System.IO;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.FileSystem;
using System.Runtime.CompilerServices;
using Cosmos.Core;
using Cosmos.System.ExtendedASCII;
using System.Drawing;
using System.Threading;

namespace SimpL_OS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Clear();
            Console.WriteLine(" ------------------------------------- ");
            Console.WriteLine(" | SimpL OS booted! built by Group 1 | ");
            Console.WriteLine(" ------------------------------------- ");
            Console.ResetColor();
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Type 'help' to show commands");
            Console.WriteLine(" ");

            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

        }

        protected override void Run()
        {
            Console.Write("Command($): ");
            var input = Console.ReadLine();

            // HELP
            if (input.ToLower() == "help")
            {
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Available commands:");
                Console.WriteLine("- help           :-   SHOWS list of commands");
                Console.WriteLine("- sys prop       :-   SHOWS the system specifications");
                Console.WriteLine("- clear          :-   CLEARS the console");
                Console.WriteLine("- time now       :-   SHOWS the current time");
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("- create dir:    :-   CREATES a directory");
                Console.WriteLine("- create f:      :-   CREATES a text file");
                Console.WriteLine("- write:         :-   WRITES content to the text file");
                Console.WriteLine("- read:          :-   SHOWS the text content of the text file");
                Console.WriteLine("- update:        :-   UPDATES the content of the text file");
                Console.WriteLine("- delete dir:    :-   DELETES a directory");
                Console.WriteLine("- delete f:      :-   DELETES a text file");
                Console.WriteLine("- move f:        :-   MOVES a text file to a different location");
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("- off            :-   SHUTDOWN");
                Console.WriteLine("- reboot         :-   REBOOT");
                Console.ResetColor();
                Console.WriteLine(" ");

            }

            // CLEAR
            else if (input.ToLower() == "clear")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(" ------------------------------- ");
                Console.WriteLine(" | 'SimpL OS' built by Group 1 | ");
                Console.WriteLine(" ------------------------------- ");
                Console.ResetColor();
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine("Type 'help' to show commands");
                Console.WriteLine(" ");

            }

            // CREATE DIRECTORY
            else if (input.ToLower() == "create dir")
            {
                Console.WriteLine("[1] Root Directory");
                Console.WriteLine("[2] Sub Directory");
                Console.WriteLine(" ");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Enter Directory Name:");
                    string directoryName = Console.ReadLine();

                    try
                    {
                        Directory.CreateDirectory(@"0:\" + directoryName);
                        Console.WriteLine("Directory has been created.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error creating directory: " + e.Message);
                    }
                }
                else if (option == "2")
                {
                    var directoryList = Directory.GetDirectories(@"0:\");

                    Console.WriteLine("List of Directories:");
                    foreach (var directoryPath in directoryList)
                    {
                        string directoryName = Path.GetFileName(directoryPath);
                        Console.WriteLine($"-{directoryName}");
                    }

                    Console.Write("Select Directory: ");
                    string selectedDirectoryName = Console.ReadLine();

                    bool directoryExists = false;
                    foreach (var directoryPath in directoryList)
                    {
                        string directoryName = Path.GetFileName(directoryPath);
                        if (directoryName == selectedDirectoryName)
                        {
                            directoryExists = true;
                            break;
                        }
                    }

                    if (directoryExists)
                    {
                        Console.WriteLine("Enter Sub Directory Name:");
                        string subDirectoryName = Console.ReadLine();

                        try
                        {
                            string selectedDirectoryPath = Path.Combine(@"0:\", selectedDirectoryName);
                            Directory.CreateDirectory(Path.Combine(selectedDirectoryPath, subDirectoryName));
                            Console.WriteLine("Sub directory has been created.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error creating sub directory: " + e.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Directory does not exist.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }
            }

            // CREATE A TEXT FILE
            else if (input.ToLower() == "create f")
            {
                Console.WriteLine("Where do you want the text file to be located?");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Enter file name to create:");
                    string fileName = Console.ReadLine() + ".txt"; 
                    try
                    {
                        var fileStream = File.Create(@"0:\" + fileName);
                        fileStream.Close(); 
                        Console.WriteLine("Text File has been Created");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("List of Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Enter file name to create:");
                    string fileName = Console.ReadLine() + ".txt";

                    try
                    {
                        var fileStream = File.Create(@"0:\" + rootDirName + @"\" + fileName);
                        fileStream.Close(); 
                        Console.WriteLine("Text File has been created");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("List Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter Root Directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Listing Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();

                    Console.WriteLine("Enter file name to create:");
                    string fileName = Console.ReadLine() + ".txt"; 

                    try
                    {
                        var fileStream = File.Create(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileName);
                        fileStream.Close();
                        Console.WriteLine("Text File has been created");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }

            }

            // WRITE
            else if (input.ToLower() == "write")
            {
                Console.WriteLine("Where is the file located?");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("List of files:");
                    var files = Directory.GetFiles(@"0:\");
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    Console.WriteLine("File has been selected!");

                    Console.WriteLine("Enter content to be written:");
                    string content = Console.ReadLine();

                    try
                    {
                        File.WriteAllText(@"0:\" + fileName, content);
                        Console.WriteLine("Content saved to file!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("List of Files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    Console.WriteLine("File has been selected!");

                    Console.WriteLine("Enter content to be written:");
                    string content = Console.ReadLine();

                    try
                    {
                        File.WriteAllText(@"0:\" + rootDirName + @"\" + fileName, content);
                        Console.WriteLine("Content saved to file!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter Root Directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();

                    Console.WriteLine("List of Files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName + @"\" + subDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    Console.WriteLine("File has been selected!");

                    Console.WriteLine("Enter content to be written:");
                    string content = Console.ReadLine();

                    try
                    {
                        File.WriteAllText(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileName, content);
                        Console.WriteLine("Content saved to file!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }

            }

            // READ
            else if (input.ToLower() == "read")
            {
                Console.WriteLine("Where is the file located?");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("List of files:");
                    var files = Directory.GetFiles(@"0:\");
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("File Content:");
                        Console.WriteLine(File.ReadAllText(@"0:\" + fileName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("List of files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("File Content:");
                        Console.WriteLine(File.ReadAllText(@"0:\" + rootDirName + @"\" + fileName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();

                    Console.WriteLine("List of Files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName + @"\" + subDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("File Content:");
                        Console.WriteLine(File.ReadAllText(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileName));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            // UPDATE
            else if (input.ToLower() == "update")
            {
                Console.WriteLine("Where is the file located?");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("List of files:");
                    var files = Directory.GetFiles(@"0:\");
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Old content:");
                        string oldContent = File.ReadAllText(@"0:\" + fileName);
                        Console.WriteLine(oldContent);

                        Console.WriteLine("Enter new content:");
                        string newContent = Console.ReadLine();

                        File.WriteAllText(@"0:\" + fileName, newContent);
                        Console.WriteLine("File has been updated!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("List of files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Old content:");
                        string oldContent = File.ReadAllText(@"0:\" + rootDirName + @"\" + fileName);
                        Console.WriteLine(oldContent);

                        Console.WriteLine("Enter new content:");
                        string newContent = Console.ReadLine();

                        File.WriteAllText(@"0:\" + rootDirName + @"\" + fileName, newContent);
                        Console.WriteLine("File has been updated!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();

                    Console.WriteLine("List of Files:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName + @"\" + subDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name:");
                    string fileName = Console.ReadLine();

                    try
                    {
                        Console.WriteLine("Old content:");
                        string oldContent = File.ReadAllText(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileName);
                        Console.WriteLine(oldContent);

                        Console.WriteLine("Enter new content:");
                        string newContent = Console.ReadLine();

                        File.WriteAllText(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileName, newContent);
                        Console.WriteLine("File has been updated!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }

            }

            // DELETE DIRECTORY
            else if (input.ToLower() == "delete dir")
            {
                Console.WriteLine("[1] Root Directory");
                Console.WriteLine("[2] Sub Directory");
                Console.WriteLine("Select:");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("List directories like this:");
                    var directories = Directory.GetDirectories(@"0:\");
                    foreach (var dir in directories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(dir));
                    }

                    Console.WriteLine("Enter directory name to delete:");
                    string dirToDelete = Console.ReadLine();

                    try
                    {
                        Directory.Delete(@"0:\" + dirToDelete);
                        Console.WriteLine("Root Directory has been deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("List Root Directories");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter Root Directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("List Sub directories in that root directory");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter Sub Directory Name to delete:");
                    string subDirToDelete = Console.ReadLine();

                    try
                    {
                        Directory.Delete(@"0:\" + rootDirName + @"\" + subDirToDelete);
                        Console.WriteLine("Sub Directory Has been Deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            // DELETE FILE
            else if (input.ToLower() == "delete f")
            {
                Console.WriteLine("Delete file from:");
                Console.WriteLine("[1] Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.WriteLine("Listing files in the root directory:");
                    var files = Directory.GetFiles(@"0:\");
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name to delete:");
                    string fileToDelete = Console.ReadLine();

                    try
                    {
                        File.Delete(@"0:\" + fileToDelete);
                        Console.WriteLine("File has been deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "2")
                {
                    Console.WriteLine("List Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter Root Directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Listing files in the root directory:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name to delete:");
                    string fileToDelete = Console.ReadLine();

                    try
                    {
                        File.Delete(@"0:\" + rootDirName + @"\" + fileToDelete);
                        Console.WriteLine("File has been deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
                else if (choice == "3")
                {
                    Console.WriteLine("List Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter Root Directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Listing sub directories in the root directory:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter Sub Directory name:");
                    string subDirName = Console.ReadLine();

                    Console.WriteLine("Listing files in the sub directory:");
                    var files = Directory.GetFiles(@"0:\" + rootDirName + @"\" + subDirName);
                    foreach (var file in files)
                    {
                        Console.WriteLine("-" + Path.GetFileName(file));
                    }

                    Console.WriteLine("Enter file name to delete:");
                    string fileToDelete = Console.ReadLine();

                    try
                    {
                        File.Delete(@"0:\" + rootDirName + @"\" + subDirName + @"\" + fileToDelete);
                        Console.WriteLine("File has been deleted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            // MOVE FILE
            else if (input.ToLower() == "move f")
            {
                Console.WriteLine("Move file from:");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string sourceChoice = Console.ReadLine();

                string sourceDirectory = "";
                if (sourceChoice == "1")
                {
                    sourceDirectory = @"0:\";
                }
                else if (sourceChoice == "2")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();
                    sourceDirectory = @"0:\" + rootDirName;
                }
                else if (sourceChoice == "3")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();
                    sourceDirectory = @"0:\" + rootDirName + @"\" + subDirName;
                }

                Console.WriteLine("List of Files:");
                var files = Directory.GetFiles(sourceDirectory);
                foreach (var file in files)
                {
                    Console.WriteLine("-" + Path.GetFileName(file));
                }

                Console.WriteLine("Enter file name to move:");
                string fileName = Console.ReadLine();
                string sourceFile = Path.Combine(sourceDirectory, fileName);

                Console.WriteLine("Where do you want to move the file?");
                Console.WriteLine("[1] Main Root");
                Console.WriteLine("[2] Root Directory");
                Console.WriteLine("[3] Sub Directory");

                string destinationChoice = Console.ReadLine();

                string destinationDirectory = "";
                if (destinationChoice == "1")
                {
                    destinationDirectory = @"0:\";
                }
                else if (destinationChoice == "2")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();
                    destinationDirectory = @"0:\" + rootDirName;
                }
                else if (destinationChoice == "3")
                {
                    Console.WriteLine("Root Directories:");
                    var rootDirectories = Directory.GetDirectories(@"0:\");
                    foreach (var rootDir in rootDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(rootDir));
                    }

                    Console.WriteLine("Enter root directory name:");
                    string rootDirName = Console.ReadLine();

                    Console.WriteLine("Sub Directories:");
                    var subDirectories = Directory.GetDirectories(@"0:\" + rootDirName);
                    foreach (var subDir in subDirectories)
                    {
                        Console.WriteLine("-" + Path.GetFileName(subDir));
                    }

                    Console.WriteLine("Enter sub directory name:");
                    string subDirName = Console.ReadLine();
                    destinationDirectory = @"0:\" + rootDirName + @"\" + subDirName;
                }

                string destinationFile = Path.Combine(destinationDirectory, fileName);

                try
                {
                    File.Copy(sourceFile, destinationFile);
                    File.Delete(sourceFile);
                    Console.WriteLine($"File has been moved to {destinationFile}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }


            // OS SHUTDOWN
            else if (input.ToLower() == "off")
            {
                Sys.Power.Shutdown();
            }

            // OS RESTART
            else if (input.ToLower() == "reboot")
            {
                Sys.Power.Reboot();
            }

            // SHOW SYSTEM PROPERTIES
            else if (input.ToLower() == "sys prop")
            {
                Console.WriteLine(" ");
                Console.WriteLine("OS: SimpL OS");
                Console.WriteLine("Version: 1.0");
                Console.WriteLine("Build Date: April 2024");

                Console.WriteLine(" ");

                Console.WriteLine("Processor: " + Cosmos.Core.CPU.GetCPUBrandString());
                Console.WriteLine("RAM: " + Cosmos.Core.CPU.GetAmountOfRAM() + "MB");

                Console.WriteLine(" ");

                string[] developers = { "-Eugine Bryan Cadiz", "-Ainie Rose Ongcoy", "-Rhea Jane Alangcas", "-Charrize Hista", "-Stefany Dizon" };

                Console.WriteLine("Developed by:");
                foreach (string developer in developers)
                {
                    Console.WriteLine(developer);
                }
                Console.WriteLine(" ");

            }

            else if (input.ToLower() == "time now")
            {
                DateTime currentTime = DateTime.Now;
                string formattedDateTime = currentTime.ToString("MM/dd/yyyy hh:mm:ss tt");
                Console.WriteLine("Current date and time: " + formattedDateTime);
            }

            // ELSE NOTHING
            else
            {
                Console.WriteLine("Invalid Command");
            }
        }
    }
}
