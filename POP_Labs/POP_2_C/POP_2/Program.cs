using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace POP_2
{
    class Program
    {
        public static void Main(string[] args)
        {
            var arr = new int[40];
            var arrsup1 = new int[10];
            var arrsup2 = new int[10];
            var arrsup3 = new int[10];
            var arrsup4 = new int[10];
            var random = new Random();

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(-100, 101);
                Console.Write("{0}, ", arr[i]);
                if (i >= 0 && i <= 9)
                {
                    arrsup1[i] = arr[i];
                }
                else
                {
                    if (i > 9 && i <= 19)
                    {
                        arrsup2[i - 10] = arr[i];
                    }
                    else
                    {
                        if (i > 19 && i <= 29)
                        {
                            arrsup3[i - 20] = arr[i];
                        }
                        else
                        {
                            arrsup4[i - 30] = arr[i];
                        }
                    }
                }
            }
            Console.Write("\n");
            FindMinInThead(arrsup1, 1);
            FindMinInThead(arrsup2, 2);
            FindMinInThead(arrsup3, 3);
            FindMinInThead(arrsup4, 4);
            Console.ReadKey();
        }

        private static void FindMinInThead(int[] arr, int ThreadNum)
        {
            var thread1 = new Thread[ThreadNum];
            var MinVal = int.MaxValue;
            var MinIdx = -1;
            object locker = new { };

            for (int i = 0; i < thread1.Length; i++)
            {
                thread1[i] = new Thread(() =>
                {
                    for (int j = 0; j < arr.Length; j++)
                    {
                        lock (locker)
                        {
                            if (arr[j] < MinVal)
                            {
                                MinVal = arr[j];
                                MinIdx = j;
                            }
                        }
                    }
                });
                thread1[i].Start();
            }

            foreach (var item in thread1)
            {
                item.Join();
            }

            Console.WriteLine("{0}-й потiк: мiнiмальне значення - {1} пiд iндексом - {2})", ThreadNum, MinVal, MinIdx);
        }


    }
}
