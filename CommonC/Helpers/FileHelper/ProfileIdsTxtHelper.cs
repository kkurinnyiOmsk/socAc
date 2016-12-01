using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Helpers.FileHelper
{
    public class ProfileIdsTxtHelper : IProfileIdsHelper
    {
        public static IEnumerable<long> GetFromSource(string path)
        {
            List<long> result = new List<long>();
            foreach (string line in File.ReadLines(path))
            {
                result.Add(long.Parse(line));
            }

                return result;
        }

        public static Dictionary<string, string> GetLoginInfoFromSource(string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (string line in File.ReadLines(path))
            {
                var loginPasswordMass = line.Split(new char[] { ' ' });
                result.Add(loginPasswordMass[0], loginPasswordMass[1]);
            }

            return result;
        }
    }
}
