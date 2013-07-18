using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GifRec
{
    public static class RandomString
    {
        public static string Create(int length)
        {
            StringBuilder sb = new StringBuilder();
            Random ran = new Random();
            char ch;

            for (int i = 0; i < length; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * ran.NextDouble() + 65)));
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}
