using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainNamespace
{
    public class ConsoleInput
    {
        public const int Infinity = int.MaxValue;
        public static int Int(int min, int max)
        {
            int c = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    c = int.Parse(Console.ReadLine());
                    if (c > max) throw new Exception();
                    if (c < min) throw new Exception();
                    flag = false;
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"ERROR: Max size is {max}! Retry again: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    flag = true;
                }
            }
            return c;
        }
    }
}
