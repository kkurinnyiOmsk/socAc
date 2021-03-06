﻿using Common;
using Common.Helpers.FileHelper;
using DataAccessLayer.Models;
using NLog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class ProccessingService
    {
        private static NLog.Logger Logger = LogManager.GetCurrentClassLogger();

        public void Start()
        {
            Logger.Info("Start proccessing");
            var actionTasks = new List<Task>();
            var usersForProccessing = GenerateStabUsers();

            foreach (var user in usersForProccessing)
            {
                actionTasks.Add(Task.Run(() =>
                {
                    Logger.Info("Start proccessing job for user with id: {0}", user.Id);
                    var actionsProccessing = new ActionsProccessing(new InstagramWorker(), user, 1000);
                    actionsProccessing.Proccessing();
                }));
            }

            //var findJob = Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        //получить активных клиентов
            //        Thread.Sleep(1000 * 60 * 5);
            //        Logger.Info("Add new user task");
            //        actionTasks.Add(Task.Run(() => Console.WriteLine("новая задача")));
            //    }
            //});

            //Task.WaitAll(findJob);
            Task.WaitAll(actionTasks.ToArray());
        }

        //метод расширения
        private List<User> GenerateStabUsers()
        {
            Logger.Debug("Get stub users");
            var result = new List<User>();
            var targetIds = ProfileIdsTxtHelper.GetFromSource("ids.txt");
            var clients = ProfileIdsTxtHelper.GetLoginInfoFromSource("clients.txt");

            foreach (var stubClient in clients)
            {
                var client = CreateStabUser(stubClient.Key, stubClient.Value);
                FillTargetUsers(targetIds, client);
                result.Add(client);
            }


            return result;
        }

        private static void FillTargetUsers(IEnumerable<long> targetIds, User user)
        {
            user.Actions = new List<TaskAction>
                {
                     new TaskAction
                        {
                            Id = 1,
                            IsActive = true, //иначе не стартанёт джоба
                            TaskType = TaskType.Follow,
                            UserId = 1
                     }
                };
            int i = 0;
            user.Actions.ToList()[0].TargetUsers = new List<UsersForSpecificAction>();

            foreach (var targetId in targetIds)
            {
                user.Actions.ToList()[0].TargetUsers.Add(
                    new UsersForSpecificAction
                    {
                        Id = i++,
                        TargerUserId = targetId
                    });
            }
        }

        private static User CreateStabUser(string login, string password)
        {
            return
                 new User
                 {
                     Id = 1,
                     Login = login,
                     Password = password,
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
                 };
        }
    }
}
