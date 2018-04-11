using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeafX.Roy.Business
{
    public class FontCharacter
    {
        public char Character { get; set; }

        public string[] Strings { get; set; }

        public int Height
        {
            get
            {
                return Strings == null ? 0 : Strings.Length;
            }
        }

        public int Width
        {
            get
            {
                return Strings == null ? 0 : Strings[0].Length;
            }
        }
    }
}
