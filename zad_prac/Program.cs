using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace zad_prac
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите строку:");
            string row = Console.ReadLine();
            Console.WriteLine("Выберие тип сортировки: Quicksort or Tree sort");
            int index = Convert.ToInt32(Console.ReadLine());

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
                if (index < 2)
                {
                    char[] chars = result.ToCharArray();
                    QuickSort(chars, 0, chars.Length - 1);
                    string sorted = new string(chars);
                    Console.WriteLine("Сортировка строки методом Quicksort");
                    Console.WriteLine(sorted);
                }
                else
                {
                    Node root = null;
                    foreach (char c in result)
                    {
                        root = Insert(root, c);
                    }

                    StringBuilder sorted1 = new StringBuilder();
                    InOrderTraversal(root, sorted1);
                    Console.WriteLine("Сортировка строки метоом Tree sort");
                    Console.WriteLine(sorted1.ToString());
                }
                int? randomPosition = await GetRandomNumberFromApiAsync(result.Length);

                int pos = randomPosition ?? RandomNumberLoc(result.Length);

                string newresult = result.Remove(pos, 1);

                Console.WriteLine("Результат после удаления символа:");
                Console.WriteLine(newresult);
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
        static void QuickSort(char[] arr, int left, int right)
        {
            if (left >= right)
                return;

            int pivotIndex = Partition(arr, left, right);
            QuickSort(arr, left, pivotIndex - 1);
            QuickSort(arr, pivotIndex + 1, right);
        }
        static int Partition(char[] arr, int left, int right)
        {
            char pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }
        static void Swap(char[] arr, int i, int j)
        {
            char temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
        class Node
        {
            public char Data;
            public Node Left, Right;

            public Node(char data)
            {
                Data = data;
                Left = Right = null;
            }
        }
        static Node Insert(Node root, char data)
        {
            if (root == null)
                return new Node(data);

            if (data < root.Data)
                root.Left = Insert(root.Left, data);
            else
                root.Right = Insert(root.Right, data);

            return root;
        }
        static void InOrderTraversal(Node root, StringBuilder result)
        {
            if (root == null)
                return;

            InOrderTraversal(root.Left, result);
            result.Append(root.Data);
            InOrderTraversal(root.Right, result);
        }
        static readonly HttpClient client = new HttpClient();

        static async Task<int?> GetRandomNumberFromApiAsync(int maxExclusive)
        {
            try
            {
                string url = $"https://www.randomnumberapi.com/api/v1.0/random?min=0&max={maxExclusive - 1}&count=1";
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                responseBody = responseBody.Trim(new char[] { '[', ']', ' ', '\n', '\r' });
                if (int.TryParse(responseBody, out int number))
                {
                    return number;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        static int RandomNumberLoc(int maxExclusive)
        {
            var rnd = new Random();
            return rnd.Next(maxExclusive);
        }
    }
}
