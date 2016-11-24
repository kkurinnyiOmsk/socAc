using Common;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class TaskAction
    {
        public long Id { get; set; }
        public TaskType TaskType { get; set; }
        public List<UsersForSpecificAction> TargetUsers { get; set; }
        public bool IsActive { get; set; }

        #region Navigation prop
        public long UserId { get; set; }
        public User User { get; set; }
        #endregion
    }
}
