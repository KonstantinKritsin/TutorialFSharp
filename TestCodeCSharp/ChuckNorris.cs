using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCodeCSharp
{
    class ChuckNorris
    {
        static void Main(string[] args)
        {
            string msg = Console.ReadLine();
            Console.Error.WriteLine(msg);

            var str = msg.Select(ch => ToBin(ch, 7)).Aggregate((res, s) => res + s);
            var result = new StringBuilder();
            var currentCh = str[0];
            var count = 0;

            result.Append(currentCh == '0' ? "00 " : "0 ");

            foreach (var ch in str)
            {
                if (ch != currentCh)
                {
                    result.Append('0', count);
                    result.Append(ch == '0' ? " 00 " : " 0 ");
                    currentCh = ch;
                    count = 0;
                }
                count++;
            }

            result.Append('0', count);

            Console.WriteLine(result.ToString());
        }

        public static string ToBin(int value, int len)
        {
            return (len > 1 ? ToBin(value >> 1, len - 1) : "") + "01"[value & 1];
        }
    }

}
