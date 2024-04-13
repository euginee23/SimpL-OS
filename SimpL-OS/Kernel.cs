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

namespace SimpL_OS
{
    public class Kernel : Sys.Kernel
    {
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();

        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Clear();
            Console.WriteLine(" ------------------------------------- ");
            Console.WriteLine(" | SimpL OS booted! built by Group 1 | ");
            Console.WriteLine(" ------------------------------------- ");
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
                Console.WriteLine("Available commands:");
                Console.WriteLine("- help       :-   SHOWS list of commands");
                Console.WriteLine("- sys prop   :-   SHOWS the system specifications");
                Console.WriteLine("- clear      :-   CLEARS the console");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("- create:    :-   CREATES a text file or create a directory");
                Console.WriteLine("- write:     :-   WRITES content to the text file");
                Console.WriteLine("- read:      :-   SHOWS the text content of the text file");
                Console.WriteLine("- update:    :-   UPDATES the content of the text file");
                Console.WriteLine("- delete:    :-   DELETE a text file or a directory");
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("- off        :-   SHUTDOWN");
                Console.WriteLine("- reboot     :-   REBOOT");
                Console.WriteLine(" ");

            }

            // CLEAR
            else if (input.ToLower() == "clear")
            {
                Console.Clear();

                Console.WriteLine(" ------------------------------- ");
                Console.WriteLine(" | 'SimpL OS' built by Group 1 | ");
                Console.WriteLine(" ------------------------------- ");
                Console.WriteLine("Type 'help' to show commands");
                Console.WriteLine(" ");

            }

            // CREATE
            else if (input.ToLower() == "create")
            {
                
            }

            // WRITE
            else if (input.ToLower() == "write")
            {
               
            }

            // READ
            else if (input.ToLower() == "read")
            {
                
            }

            // UPDATE
            else if (input.ToLower() == "update")
            {
                
            }

            // DELETE
            else if (input.ToLower() == "delete")
            {
                
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

            // ELSE NOTHING
            else
            {
                Console.WriteLine("Invalid Command");
            }
        }
    }
}
