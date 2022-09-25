using System;
using System.Threading;
namespace POP_1
{
    class Program
    {

        static void Main(string[] args)
        {
            (new Program()).Start();
            Console.ReadKey();
        }

        void Start()
        {
            (new Thread(Calculator)).Start();
            (new Thread(Calculator)).Start();
            (new Thread(Calculator)).Start();

            Thread thread1 = new Thread(Calculator);
            thread1.Start();

            (new Thread(Stoper)).Start();
        }

        void Calculator()
        {
            long sum = 0;
            int step = 5;
            long i = 0;
            do
            {
                sum += step;
                i++;
            }
            while (!canStop);
            Console.WriteLine("Сума = {0}, крок = {1}, кiлькiсть доданкiв = {2}.", sum, step, i);
        }

        private bool canStop = false;

        public bool CanStop { get => canStop; }

        public void Stoper()
        {
            Thread.Sleep(30 * 1000);
            canStop = true;
        }
    }
}