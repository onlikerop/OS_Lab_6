using System;
using System.Collections.Generic;

namespace mainNamespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            int max_size = 64 * 1024;
            Console.WriteLine("\t\t\tMemory allocation by movable sectors\n");
            Console.Write("Enter number of sectors: ");
            int c1 = ConsoleInput.Int(1, ConsoleInput.Infinity);
            List<int> sections1 = new();
            for (int i1 = 0; i1 < c1 - 1; i1++)
            {
                Console.Write($"Enter size of sector {i1 + 1}: ");
                int currentSector = ConsoleInput.Int(1, max_size);
                sections1.Add(currentSector);
                max_size -= currentSector;
                Console.WriteLine($"Successfuly added sector. Space remains: {max_size}");
            }
            sections1.Add(max_size);
            _ = new Sections();
        }
    }
}
