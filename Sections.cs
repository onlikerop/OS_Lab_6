using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainNamespace
{
    class Sections
    {
        private int ID { get; set; }
        private List<Addr> AddrList { get; set; }

        string RAM;
        public Sections()
        {
            //int size = 64 * 1024 / count;
            AddrList = new();
            ID = 1;
            while (true)
            {
                Draw();
            }
        }

        private string GetByAddr(float adr)
        {
            foreach (Addr item in AddrList)
            {
                if (item.Address <= adr &&
                    item.Address + item.Task.Size >= adr)
                {
                    return item.Task.Identificator.ToString() + " ";
                }
            }
            return " ";
        }

        private bool FreeAddr(float adrStart, float adrFinish)
        {
            foreach (Addr item in AddrList)
            {
                if (item.Address < adrFinish &&
                    item.Address + item.Task.Size > adrStart)
                {
                    return false;
                }
            }
            return true;
        }

        private int FreeAddr(int adrRight)
        {
            for (int i = 1; i < adrRight; i++)
            {
                if (!FreeAddr(adrRight - i, adrRight))
                {
                    return i - 1;
                }
            }
            return adrRight;
        }

        private void ComposeRAM()
        {
            RAM = "";
            for (int j = 0; j < 65536; j++)
            {
                RAM += GetByAddr(j);
            }
            System.IO.File.WriteAllText("RAM", RAM);
        }

        private void Draw()
        {
            Console.Clear();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    Console.Write(GetByAddr((j + 50 * i) * 131.072f));
                }
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPress \'A\' for adding task or \'D\' for removing");
            Console.ForegroundColor = ConsoleColor.Cyan;
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.A:
                    Console.Write("\nEnter size of the task: ");
                    try
                    {
                        AddTask(ConsoleInput.Int(0, ConsoleInput.Infinity));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("ERROR: ");
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.ReadKey();
                    }
                    break;
                case ConsoleKey.D:
                    Console.Write("\nEnter task ID: ");
                    try
                    {
                        UnloadTask(ConsoleInput.Int(1, ConsoleInput.Infinity));
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("ERROR: ");
                        Console.WriteLine(ex.Message);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.ReadKey();
                    }
                    break;
            }
        }

        private void Optimize()
        {
            bool not_avaliable_moves = true;
            while (not_avaliable_moves)
            {
                not_avaliable_moves = false;
                foreach (Addr item in AddrList)
                {
                    if (FreeAddr(item.Address - 1, item.Address) && item.Address > 0)
                    {
                        item.Address -= FreeAddr(item.Address);
                        not_avaliable_moves = true;
                    }
                }
            }
        }


        public void AddTask(int task_size)
        {
            for (int i = 0; i < 65536 - task_size; i++)
            {
                if (FreeAddr(i, i + task_size))
                {
                    AddrList.Add(new Addr(new OS_Task(ID++, task_size), i));
                    ComposeRAM();
                    return;
                }
            }
            throw new Exception("There is no enough space for such task");
        }

        public void UnloadTask(int id)
        {
            foreach (Addr section in AddrList)
            {
                if (section.Task.Identificator == id)
                {
                    AddrList.Remove(section);
                    Optimize();
                    ComposeRAM();
                    return;
                }
            }
            throw new Exception("There is no such task");
        }
    }
}
