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
                Logger.Warn("login failed for user {0} {1} because {2}", user.Id, user.Login, loginInfo.ErrorMessage);
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
                    break;
                }
                currenActionCount++;
                var rand = new Random();
                var randomSleep = rand.Next(30000, 40000); //todo need exact time
                Thread.Sleep(randomSleep);
            }
        }
    }
}
