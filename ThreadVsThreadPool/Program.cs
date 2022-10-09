using System;
using System.Diagnostics;
using System.Threading;

namespace ThreadVsThreadPool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                MethodWithThread();
                MethodWithThreadPool();
            }
            Stopwatch stopwatch = new Stopwatch();
            for (int i = 0; i < 10; i++)
            {
                stopwatch.Reset();
                Console.WriteLine("********Case " + (i + 1) + " ********");
                stopwatch.Start();
                MethodWithThread();
                stopwatch.Stop();
                Console.WriteLine("Time consumed by MethodWithThread is : " +
                                     stopwatch.ElapsedTicks.ToString());

                stopwatch.Reset();
                stopwatch.Start();
                MethodWithThreadPool();
                stopwatch.Stop();
                Console.WriteLine("Time consumed by MethodWithThreadPool is : " +
                                     stopwatch.ElapsedTicks.ToString());
                Console.WriteLine("________________________________");
            }


            Console.Read();
        }

        public static void MethodWithThread()
        {
            for (int i = 0; i < 10000; i++)
            {
                Thread thread = new Thread(Test);
            }
        }
        public static void MethodWithThreadPool()
        {
            for (int i = 0; i < 10000; i++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(Test));
            }

        }
        public static void Test(object obj)
        {
        }
    }
}
