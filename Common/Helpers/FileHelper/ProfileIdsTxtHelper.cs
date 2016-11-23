using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Helpers.FileHelper
{
    public class ProfileIdsTxtHelper : IProfileIdsHelper
    {
        public IEnumerable<long> GetFromSource(string path)
        {
            List<long> result = new List<long>();
            foreach (string line in File.ReadLines(path))
            {
                result.Add(long.Parse(line));
            }

            return result;
        }
    }
}
