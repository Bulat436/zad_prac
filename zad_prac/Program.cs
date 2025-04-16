using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad_prac
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите строку:");
            string row = Console.ReadLine();

            string result;
            if (LowercaseEnglish(row))
            {


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
            else
            {
                Console.WriteLine("Неправильный ввод");
            }
            Dictionary<char, int> letterCount = new Dictionary<char, int>();

            foreach (char c in row)
            {
                if (letterCount.ContainsKey(c))
                {
                    letterCount[c]++;
                }
                else
                {
                    letterCount[c] = 1;
                }
            }
            Console.WriteLine("Количество каждой буквы:");
            foreach (var item in letterCount)
            {
                Console.WriteLine($"{item.Key}: {item.Value}");
            }
        }
        static bool LowercaseEnglish(string row)
        {
            foreach (char c in row)
            {
                if (c < 'a' || c > 'z')
                {
                    return false;
                }
            }
            return true;
        }
    }
}
