using System;
using System.Threading;

namespace POP_4_C
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 5;
            Console.WriteLine("Виберiть яку програму треба завантажити:\n1 - без офiцiянта; 2 - з офiцiянтом");
            string task = Convert.ToString(Console.ReadLine());
            if (task == "1")
            {
                FirstTask.Task(n);
            }
            else
            {
                SecondTask.Task(n);
            }
            Console.ReadKey();
        }
    }
    class FirstTask
    {
        static public void Task(int n)
        {
            Semaphore[] fork = new Semaphore[n];
            for (int i = 0; i < n; i++)
            {
                fork[i] = new Semaphore(1, 1);
            }
            for (int i = 0; i < n; i++)
            {
                if (i < n - 1)
                {
                    new Thread(() => Philosopher(i, fork[i], fork[i + 1])).Start();
                }
                else
                {
                    new Thread(() => Philosopher(i, fork[0], fork[i])).Start();
                }
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
        static void Philosopher(int id, Semaphore forkleft, Semaphore forkright)
        {
            Console.WriteLine("Фiлософ {0} розмiрковує", id);
            Console.WriteLine("Фiлософ {0} шукає виделку", id);
            forkleft.WaitOne();
            forkright.WaitOne();
            Console.WriteLine("Фiлософ {0} бере виделку", id);
            forkleft.Release();
            forkright.Release();
            Console.WriteLine("Фiлософ {0} їсть i кладе виделку назад", id);
            Thread.Sleep(100);
        }
    }
    class SecondTask
    {
        static public void Task(int n)
        {
            Semaphore[] fork = new Semaphore[2];
            for (int i = 0; i < 2; i++)
            {
                fork[i] = new Semaphore(1, 1);
                fork[i].WaitOne();
            }
            for (int i = 0; i < n; i++)
            {
                new Thread(() => Waiter(fork)).Start();
                new Thread(() => Philosopher(i, fork)).Start();
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
        static void Philosopher(int id,Semaphore[] fork)
        {
            Console.WriteLine("Фiлософ {0} чекає на виделку", id);
            Thread.Sleep(100);
            fork[0].WaitOne();
            fork[1].WaitOne();
            Console.WriteLine("Фiлософ {0} бере виделку i їсть", id);
            Console.WriteLine("Фiлософ {0} їсть i кладе виделку назад", id);
        }
        static void Waiter(Semaphore[] fork)
        {
            fork[0].Release();
            fork[1].Release();
        }
    }
}
