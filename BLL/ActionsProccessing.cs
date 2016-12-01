using DataAccessLayer.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BLL
{
    public class ActionsProccessing
    {
        private static NLog.Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IInstagramWorker instagramWorker;
        private User user;
        private long currenActionCount;
        private long expectedActionCount;

        public ActionsProccessing(IInstagramWorker instagramWorker, User user, long expectedActionCount)
        {
            this.instagramWorker = instagramWorker;
            this.user = user;
            this.expectedActionCount = expectedActionCount;
            currenActionCount = 0;
        }

        public void Proccessing()
        {
            var loginInfo = instagramWorker.Login(user.Login, user.Password);
            if(!loginInfo.IsSuccess)
            {
                Logger.Error("login failed for user with id: {0} because {1}", user.Id, loginInfo.ErrorMessage);
                //todo send email for admin
                return;
            }
            var targetUsers = user.Actions.FirstOrDefault(action => action.IsActive).TargetUsers;
            var targetUsersId = new Queue<long>(targetUsers.Select(user => user.TargerUserId));

            while (currenActionCount < expectedActionCount)
            {
                var followResult = instagramWorker.Follow(loginInfo, targetUsersId.Dequeue());
                if(followResult == false)
                {
                    //todo send email for admin
                    Logger.Error("following failed for user with id: {0}", user.Id);
                    break;
                }
                currenActionCount++;
                var rand = new Random();
                var randomSleep = rand.Next(30000, 40000); //todo need exact time
                Thread.Sleep(randomSleep);
            }
            //task successfully ended for user
            Logger.Info("task successfully ended for user with id: {0}", user.Id);
        }
    }
}
