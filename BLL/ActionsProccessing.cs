using DataAccessLayer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BLL
{
    public class ActionsProccessing
    {
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
            var targetUsers = user.Actions.FirstOrDefault(action => action.IsActive).TargetUsers;
            Queue<long> targetUsersId = new Queue<long>(targetUsers.Select(user => user.TargerUserId));

            while (currenActionCount < expectedActionCount)
            {
                instagramWorker.Follow(loginInfo, targetUsersId.Dequeue());
                currenActionCount++;
                var rand = new Random();
                var randomSleep = rand.Next(30000, 40000);
                Thread.Sleep(randomSleep);
            }
        }
    }
}
