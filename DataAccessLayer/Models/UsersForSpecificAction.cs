using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class UsersForSpecificAction
    {
        public long Id { get; set; }
        public long TargerUserId { get; set; }


        #region Navigation prop
        public long TaskActionId { get; set; }
        public TaskAction TaskAction { get; set; }
        #endregion
    }
}
