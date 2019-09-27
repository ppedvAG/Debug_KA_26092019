using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HalloTPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** Hallo TPL ***");

            //Parallel.Invoke(Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle, Zähle);
            //Parallel.For(0, 1000000, i => Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] {i}"));

            Task t1 = new Task(() =>
            {
                Console.WriteLine("T1 gestartet");
                Thread.Sleep(800);
                Console.WriteLine("T1 fertig");
            });

            Task<long> t2 = new Task<long>(() =>
            {
                Console.WriteLine("T2 gestartet");
                Thread.Sleep(700);
                Console.WriteLine("T2 fertig");
                //throw new FieldAccessException();
                return 234567890;
            });

            t2.ContinueWith(tr =>
            {
                Console.WriteLine($" T2 ERROR: {tr.Exception.InnerException.Message}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Default);

            t2.ContinueWith(tr =>
            {
                Console.WriteLine($" T2 OK: {tr.Result}");
            }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.Default);

            t1.Start();
            t2.Start();

            Task.WaitAll(t1, t2);
            //Console.WriteLine($"{t2.Result}");

            Console.WriteLine("Ende");
            Console.ReadLine();
        }

        static void Zähle()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] {i}");
            }
        }
    }
}
