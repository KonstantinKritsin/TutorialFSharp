using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCodeCSharp
{
    class ASCIIArt
    {
        private static Dictionary<char, int> Alf = new Dictionary<char, int>
        {
            {'A', 0},
            {'B', 1},
            {'C', 2},
            {'D', 3},
            {'E', 4},
            {'F', 5},
            {'G', 6},
            {'H', 7},
            {'I', 8},
            {'J', 9},
            {'K', 10},
            {'L', 11},
            {'M', 12},
            {'N', 13},
            {'O', 14},
            {'P', 15},
            {'Q', 16},
            {'R', 17},
            {'S', 18},
            {'T', 19},
            {'U', 20},
            {'V', 21},
            {'W', 22},
            {'X', 23},
            {'Y', 24},
            {'Z', 25},
            {'_', 26}
        };

        static void Main(string[] args)
        {
            int L = int.Parse(Console.ReadLine());
            int H = int.Parse(Console.ReadLine());
            string T = Console.ReadLine().ToUpper();

            var dic = new string[H];
            for (int i = 0; i < H; i++) dic[i] = Console.ReadLine();

            var output = new string[H];

            for (var i = 0; i < H; ++i)
            {
                var tmp = new StringBuilder(T.Length * L);
                foreach (var ch in T)
                {
                    var ind = Alf.ContainsKey(ch) ? Alf[ch] : Alf['_'];
                    tmp.Append(dic[i].Substring(ind * L, L));
                }
                output[i] = tmp.ToString();
            }

            foreach (var line in output) Console.WriteLine(line);
        }
    }
}
