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
            var font = ParseFont("Broadway");
            
            var stringToPrint = "hejsan";

            StringBuilder[] rowsToPrint = font[0].Strings.Select(s => new StringBuilder()).ToArray();

            foreach(var charToPrint in stringToPrint)
            {
                

                if(charToPrint == ' ')
                {
                    foreach(var row in rowsToPrint)
                    {
                        row.Append("   ");
                    }

                    continue;
                }

                var fontChar = font.FirstOrDefault(f => f.Character == charToPrint);

                if(fontChar == null)
                {
                    continue;
                }

                for(var row = 0; row < rowsToPrint.Length; row++)
                {
                    rowsToPrint[row].Append(fontChar.Strings[row]);
                }

                foreach (var row in rowsToPrint)
                {
                    row.Append(' ');
                }
            }

            using (var streamWriter = new StreamWriter("C:\\Temp\\Testfil.txt"))
            {
                foreach(var row in rowsToPrint)
                {
                    streamWriter.WriteLine(row.ToString().TrimEnd());
                }
            }
        }

        private static char[] Characters = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        static FontChar[] ParseFont(string fontName)
        {
            string fontString = GetStringFromFile(fontName);

            var rows = fontString.Split('\n');

            var fontChars = new List<FontChar>();
            var charCnt = 0;

            for(var index = 0; index < rows[0].Length;)
            {
                var charEnd = FindNextSpace(index, rows);

                fontChars.Add(new FontChar()
                {
                    Character = Characters[charCnt++],
                    Strings = rows.Select(s => s.Substring(index, charEnd - index)).ToArray()
                });

                index = charEnd + 1;
            }

            return fontChars.ToArray();
        }

        private static int FindNextSpace(int startIndex, string[] rows)
        {

            for(var index = startIndex; index < rows[0].Length; index++)
            {
                if(rows.All(r => r[index] == ' '))
                {
                    if (index - startIndex > 3)
                    {
                        return index;
                    }
                }
            }

            return rows[0].Length - 1;
        }

        private static string GetStringFromFile(string fileName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = $"DeafX.Roy.Fonts.{fileName}.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public class FontChar
        {
            public char Character { get; set; }

            public string[] Strings { get; set; }

            public int Height {
                get
                {
                    return Strings == null ? 0 : Strings.Length;
                }
            }

            public int Width
            {
                get
                {
                    return  Strings == null ? 0 : Strings[0].Length;
                }
            }
        }
    }
}
