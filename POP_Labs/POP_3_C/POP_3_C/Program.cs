using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace POP_3_C
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Starter(3, 9);

            Console.ReadKey();
        }


        private void Starter(int storageSize, int itemNumbers)
        {
            Access = new Semaphore(1, 1);
            Full = new Semaphore(storageSize, storageSize);
            Empty = new Semaphore(0, storageSize);
            for (int i = 1; i < 4; i++)
            {
                Thread threadConsumer = new Thread(Consumer);
                threadConsumer.Name = $"Consumer {i}";
                threadConsumer.Start(itemNumbers);

                Thread threadProducer = new Thread(Producer);
                threadProducer.Name = $"Producer {i}";
                threadProducer.Start(itemNumbers);
            }
        }

        private Semaphore Access;
        private Semaphore Full;
        private Semaphore Empty;

        private readonly List<string> storage = new List<string>();


        private void Producer(Object itemNumbers)
        {
            int maxItem = 0;
            if (itemNumbers is int)
            {
                maxItem = (int)itemNumbers;
            }
            for (int i = 0; i < maxItem; i++)
            {
                Full.WaitOne();
                Access.WaitOne();

                storage.Add("item " + i);
                Console.WriteLine($"+ {Thread.CurrentThread.Name} Added item " + i);

                Access.Release();
                Empty.Release();
            }
        }

        private void Consumer(Object itemNumbers)
        {
            int maxItem = 0;
            if (itemNumbers is int)
            {
                maxItem = (int)itemNumbers;
            }
            for (int i = 0; i < maxItem; i++)
            {
                Empty.WaitOne();
                Thread.Sleep(1000);
                Access.WaitOne();

                string item = storage.ElementAt(0);
                storage.RemoveAt(0);
                Console.WriteLine($"- {Thread.CurrentThread.Name} remove {item}");
                Full.Release();
                Access.Release();
            }
        }
    }
}
