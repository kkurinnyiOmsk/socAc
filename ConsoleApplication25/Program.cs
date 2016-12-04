using BLL;
using CommonC.Helpers;
using System;
using System.Configuration;

namespace ConsoleApplication25
{
    class Program
    {
        static void Main(string[] args)
        {
            //MailHelper mh = new MailHelper();
            //mh.SendMail("WARNING");


            ProccessingService proccessingService = new ProccessingService();
            proccessingService.Start();

            Console.ReadLine();
            //}
            //static void Main(string[] args)
            //{
            //    var tasks = new List<Task>();

            //    var findJob = Task.Run(() =>
            //    {
            //        while (true)
            //        {
            //            Thread.Sleep(10000);
            //            //получить активных клиентов
            //            tasks.Add(Task.Run(() => Console.WriteLine("sdfsdf")));
            //        }
            //    });

            //    for (int i = 0; i < 50; i++)
            //    {
            //        tasks.Add(Task.Run(() =>
            //        {
            //            for (int j = 0; j < 300; j++)
            //            {
            //                Thread.Sleep(20000);
            //            }
            //        }));
            //    }

            //    Task.WaitAll(findJob);
            //    Console.ReadLine();
            //}
        }
    }
}
