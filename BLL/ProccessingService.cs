using Common;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class ProccessingService
    {
        public void Start()
        {
            var actionTasks = new List<Task>();
            List<User> usersForProccessing = new List<User>();

            foreach (var user in usersForProccessing)
            {
                actionTasks.Add(Task.Run(() =>
                {
                    var actionsProccessing = new ActionsProccessing(new InstagramWorker(), user, 1000);
                    actionsProccessing.Proccessing();
                }));
            }

            var findJob = Task.Run(() =>
            {
                while (true)
                {
                    //получить активных клиентов
                    Thread.Sleep(1000 * 60 * 5);
                    actionTasks.Add(Task.Run(() => Console.WriteLine("новая задача")));
                }
            });

            Task.WaitAll(findJob);
        }

        private List<User> GenerateStabUsers()
        {
            return new List<User>
            {
                new User
                {
                    Id = 1,
                    Login = "boolean1515",
                    Password = "bOOlean200",
                    IsActive = true,
                    IsInProccessing = false,
                    Actions = new List<TaskAction>
                    {
                        new TaskAction
                        {
                            Id = 1,
                            IsActive = true,
                            TaskType = TaskType.Follow,
                            UserId = 1,
                            TargetUsers = new List<UsersForSpecificAction>
                            {
                               new UsersForSpecificAction
                               {
                                   Id = 1,
                                   TargerUserId = 12312312
                               },
                                 new UsersForSpecificAction
                               {
                                   Id = 1,
                                   TargerUserId = 12312312
                               }
                            }
                        }
                    }
                }
            };
        }
    }
}
