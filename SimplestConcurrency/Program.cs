

using System.ComponentModel;

namespace SimplestConcurrency
{
    /// <summary>
    /// https://dotnettutorials.net/lesson/concurrent-collection-in-csharp/
    /// </summary>
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //使用Thread类：Thread类是C#中最基本的多线程类，通过实例化Thread类并传入一个委托作为线程函数来创建线程
            Thread t = new Thread(DoThreadWork);
            t.Start();


            //BackgroundWorker类使得在后台线程执行异步任务变得容易
            BackgroundWorker worker = new BackgroundWorker();
            // 绑定异步任务
            worker.DoWork += new DoWorkEventHandler(DoBackgroundWork);
            // 开始异步任务
            worker.RunWorkerAsync();


            //使用ThreadPool类：ThreadPool类是一个线程池，它负责管理线程的创建和回收，从而更加高效地利用系统资源。
            //需要使用ThreadPool.QueueUserWorkItem方法将工作项加入线程池。
            ThreadPool.QueueUserWorkItem(DoThreadPoolWork);

            //使用Task类：Task类是C# 4.0引入的新特性，它提供了一种更加简单和高效的管理多线程的方式。
            //可以使用Task.Run方法来创建异步任务并运行它
            await Task.Run(() => DoThreadWork());

            //使用Parallel类：Parallel类提供了一种简化并行编程的方法，它可以自动将任务分发到多个线程上执行
            Parallel.Invoke(DoThreadWork, DoThreadWork, DoThreadWork);


        }

        static void DoThreadWork()
        {
            Console.WriteLine("Thread Started");
            Thread.Sleep(1000); // 模拟线程工作
            Console.WriteLine("Thread Finished");
        }

        static void DoBackgroundWork(object? sender, DoWorkEventArgs e)
        {
            Console.WriteLine("New thread starts.");
            Thread.Sleep(1000); // 模拟线程工作
            Console.WriteLine("New thread exits.");
        }

        static void DoThreadPoolWork(object? state)
        {
            Console.WriteLine("Thread Started");
            Thread.Sleep(1000); // 模拟线程工作
            Console.WriteLine("Thread Finished");
        }
    }
}