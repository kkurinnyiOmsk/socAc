using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IInstagramWorker
    {
        LoginInfo Login(string login, string password);

        bool Follow(LoginInfo loginInfo, long targetUser);
    }
}
