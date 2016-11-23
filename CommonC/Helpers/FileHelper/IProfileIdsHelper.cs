using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Helpers.FileHelper
{
    interface IProfileIdsHelper
    {
        IEnumerable<long> GetFromSource(string path);
    }
}
