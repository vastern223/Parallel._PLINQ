using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            Read_from_file(numbers);

            var ArrUniqueNumbers = numbers.AsParallel()
                                .AsOrdered()
                                .Where(n => Unique_Numbers(n,numbers) == false)
                                .Select(n => n);

            Console.Write($"Unique Numbers: ");
            foreach (var item in ArrUniqueNumbers)
                Console.Write($"{item} ");
    
            Console.ReadKey();
        }

        static bool Unique_Numbers(int x, List<int> list)
        {
            bool Unique = false;
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (x == list[i])
                    count++;         
                if (count >= 2){ Unique = true; break;}
            }
            return Unique;
        }

        public static  void Read_from_file(List<int> numbers)
        {
            using (FileStream fs = new FileStream("number.txt", FileMode.Open, FileAccess.Read))
            {
                string lineStr = null;
                StreamReader streamReader = new StreamReader(fs);
                char[] wordsSplit = new char[] { ' ', ',', '!', '?', '.' };
                while (!streamReader.EndOfStream)
                {
                    lineStr = streamReader.ReadLine();
                    string[] words = lineStr.Split(wordsSplit, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var item in words)
                    {
                        numbers.Add(int.Parse(item));
                    }
                }  
                

            }
        }
    }
}

