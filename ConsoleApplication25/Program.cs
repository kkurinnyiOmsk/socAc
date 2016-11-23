using BLL;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication25
{
    class Program
    {
        static void Main(string[] args)
        {
            IInstagramWorker instagramWorker = new InstagramWorker();
            var start = DateTime.Now;
            var loginInfo = instagramWorker.Login("boolean1515", "bOOlean200");
            var followInfo = instagramWorker.Follow(loginInfo, 2236958557);
            var end = DateTime.Now;

            Console.WriteLine(end - start);
            Console.ReadLine();
        }
        //static void Main(string[] args)
        //{
        //    var tasks = new List<Task>();
        //    for (int i = 0; i < 500; i++)
        //    {
        //        tasks.Add(Task.Run(() =>
        //        {
        //            for (int j = 0; j < 300; j++)
        //            {
        //                Thread.Sleep(20000);
        //            }
        //        }));
        //    }
    }
}
