using DeafX.Roy.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeafX.Roy
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RTC v.1.0");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine("1. Projektanteckningar");
            Console.WriteLine("2. Mötesanteckningar");
            Console.WriteLine();
            Console.Write("Välj ett alternativ:");

            var choice = Console.ReadKey();

            switch(choice.Key)
            {
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    CreateFile(false);
                    break;
                default:
                    CreateFile(true);
                    break;
            }

        }

        private static void CreateFile(bool project)
        {
            Console.Write("\n" + (project ? "Projektnamn:" : "Mötesnamn"));
            var projectName = Console.ReadLine();

            Console.Write("Titel:");
            var title = Console.ReadLine();

            var templateWriter = new TemplateWriter();

            var folder = project ? "Projekt & Features" : "Möten";
            var folderPath = $@"H:\dokument\-=[Dokument Sparas\{folder}\{projectName}";

            Directory.CreateDirectory(folderPath);

            templateWriter.PrintToFile($@"{folderPath}\Anteckningar.txt", title.Length == 0 ? projectName : title, "Anteckningar");
        }

    }
}
