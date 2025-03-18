using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad_prac
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string row = Console.ReadLine();

            string result;

            if (row.Length % 2 == 0)
            {
                int mid = row.Length / 2;
                string firsthalf = row.Substring(0, mid);
                string secondhalf = row.Substring(mid);
                string reversfirsthalf = new string(firsthalf.Reverse().ToArray());
                string reverssecondhalf = new string(secondhalf.Reverse().ToArray());
                result = reversfirsthalf + reverssecondhalf;
            }
            else
            {
                string reversed = new string(row.Reverse().ToArray());
                result = reversed + row;
            }

            Console.WriteLine("Обработанная строка:");
            Console.WriteLine(result);
        }
    }
}
