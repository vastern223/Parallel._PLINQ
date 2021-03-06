using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace task3
{
 
    class Program
    {
       static  List<int> numbers = new List<int>();

        static void Main(string[] args)
        {         
            Read_from_file(numbers);

            List<int> index_list = new List<int>();
            for (int i = 0; i < numbers.Count; i++){ index_list.Add(i);}

            var plnq = index_list.AsParallel()
                                    .AsOrdered()
                                    .Select(n => MaximumDurationOfSequenceOfPositiveNumbers(n, numbers));

            Console.WriteLine("Maximum Duration Of Sequence Of Positive Numbers: "+plnq.Max());
            Console.ReadKey();
        }

        static int MaximumDurationOfSequenceOfPositiveNumbers(int x, List<int> list)
        {
            int count = 0;
            for (int i = x; i < list.Count; i++)
            {
                if (list[i] < 0){ return 0;}
                else if (i < list.Count - 1)
                {
                    if (list[i + 1] >= 0){ count++; if (i + 1 == list.Count - 1) {count++; return count;} }
                    if (list[i + 1] < 0){ count++; return count; }
                }
            }
            return ++count;
        }

        public static void Read_from_file(List<int> numbers)
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


