using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DeafX.Roy.Business
{
    public class Font
    {

        private static char[] CharactersToParse = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        public FontCharacter[] Characters { get; set; }

        public StringBuilder[] GetStringRows(string stringToPrint)
        {
            StringBuilder[] stringRows = Characters[0].Strings.Select(s => new StringBuilder()).ToArray();

            foreach (var charToPrint in stringToPrint.ToLower())
            {


                if (charToPrint == ' ')
                {
                    foreach (var row in stringRows)
                    {
                        row.Append("   ");
                    }

                    continue;
                }

                var fontChar = Characters.FirstOrDefault(f => f.Character == charToPrint);

                if (fontChar == null)
                {
                    continue;
                }

                for (var row = 0; row < stringRows.Length; row++)
                {
                    stringRows[row].Append(fontChar.Strings[row]);
                }

                foreach (var row in stringRows)
                {
                    row.Append(' ');
                }
            }

            return stringRows;
        }


        public static Font ParseFont(string fontName)
        {
            string fontString = GetStringFromFile(fontName);

            var rows = fontString.Split('\n');

            var fontChars = new List<FontCharacter>();
            var charCnt = 0;

            for (var index = 0; index < rows[0].Length;)
            {
                var charEnd = FindNextBar(index, rows);

                fontChars.Add(new FontCharacter()
                {
                    Character = CharactersToParse[charCnt++],
                    Strings = rows.Select(s => s.Substring(index, charEnd - index)).ToArray()
                });

                index = charEnd + 1;
            }

            return new Font()
            {
                Characters = fontChars.ToArray()
            };
        }   

        private static int FindNextBar(int startIndex, string[] rows)
        {

            for (var index = startIndex; index < rows[0].Length; index++)
            {
                if (rows.All(r => r[index] == '|'))
                {
                    return index;
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
    }
}
