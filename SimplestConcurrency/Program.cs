

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
            //ʹ��Thread�ࣺThread����C#��������Ķ��߳��࣬ͨ��ʵ����Thread�ಢ����һ��ί����Ϊ�̺߳����������߳�
            Thread t = new Thread(DoThreadWork);
            t.Start();


            //BackgroundWorker��ʹ���ں�̨�߳�ִ���첽����������
            BackgroundWorker worker = new BackgroundWorker();
            // ���첽����
            worker.DoWork += new DoWorkEventHandler(DoBackgroundWork);
            // ��ʼ�첽����
            worker.RunWorkerAsync();


            //ʹ��ThreadPool�ࣺThreadPool����һ���̳߳أ�����������̵߳Ĵ����ͻ��գ��Ӷ����Ӹ�Ч������ϵͳ��Դ��
            //��Ҫʹ��ThreadPool.QueueUserWorkItem����������������̳߳ء�
            ThreadPool.QueueUserWorkItem(DoThreadPoolWork);

            //ʹ��Task�ࣺTask����C# 4.0����������ԣ����ṩ��һ�ָ��Ӽ򵥺͸�Ч�Ĺ�����̵߳ķ�ʽ��
            //����ʹ��Task.Run�����������첽����������
            await Task.Run(() => DoThreadWork());

            //ʹ��Parallel�ࣺParallel���ṩ��һ�ּ򻯲��б�̵ķ������������Զ�������ַ�������߳���ִ��
            Parallel.Invoke(DoThreadWork, DoThreadWork, DoThreadWork);


        }

        static void DoThreadWork()
        {
            Console.WriteLine("Thread Started");
            Thread.Sleep(1000); // ģ���̹߳���
            Console.WriteLine("Thread Finished");
        }

        static void DoBackgroundWork(object? sender, DoWorkEventArgs e)
        {
            Console.WriteLine("New thread starts.");
            Thread.Sleep(1000); // ģ���̹߳���
            Console.WriteLine("New thread exits.");
        }

        static void DoThreadPoolWork(object? state)
        {
            Console.WriteLine("Thread Started");
            Thread.Sleep(1000); // ģ���̹߳���
            Console.WriteLine("Thread Finished");
        }
    }
}