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
                string subs = LongestSubstring(result);
                Console.WriteLine("Самая длинная подстрока:");
                Console.WriteLine(subs);
            }
            else
            {
                Console.WriteLine("Неправильный ввод");
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
        static bool ContainsAEIOUY(string str)
        {
            return str.Contains('a') || str.Contains('e') || str.Contains('i') || str.Contains('o') || str.Contains('u') || str.Contains('y');
        }
        static string LongestSubstring(string str)
        {
            string let = "aeiouy";
            string longestSubstring = string.Empty;

            for (int i = 0; i < str.Length; i++)
            {
                if (let.Contains(str[i]))
                {
                    for (int j = str.Length - 1; j > i; j--)
                    {
                        if (let.Contains(str[j]))
                        {
                            string substring = str.Substring(i, j - i + 1);
                            if (substring.Length > longestSubstring.Length)
                            {
                                longestSubstring = substring;
                            }
                            break;
                        }
                    }
                }
            }
            return longestSubstring;

        }
    }
}
