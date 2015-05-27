using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _project
{
    class Task6
    {
        const int c = 65535;
        List<Int64> [] table = new List<Int64>[c]; //2^21
        public Task6()
        {
            for (int i = 0; i < c; ++i)
            {
                table[i] = new List<long>();
            }
        }
        public int hash1(Int64 key)
        {
            return (int)((key & 2147418112L) >> 16);
        }
        public bool hasKey(Int64 key)
        {
            int h1 = hash1(key);
            return table[h1].Contains(key);
        }
        public void add(Int64 key)
        {
            int h1 = hash1(key);
            table[h1].Add(key);
        }
    }
}
