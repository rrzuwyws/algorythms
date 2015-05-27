using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _project
{
    class Task4
    {
        static int[] count = new int[26];
        /// <summary>
        /// only eng letters, low case, exactly 3 chars per string
        /// </summary>
        /// <param name="arrayToSort"></param>
        /// <returns></returns>
        static public string[] CountingSort(string[]arrayToSort)
        {
            for (int i = 0; i < count.Length; ++i)
            {
                count[i] = 0;
            }
            string[] outArray = new string[arrayToSort.Length];
            for (int f = 2; f > -1; --f)
            {
                int[] iterationCount = new int[26];
                for (int i = 0; i < arrayToSort.Length; ++i)
                {
                    count[arrayToSort[i][f] - 'a']++;
                    iterationCount[arrayToSort[i][f] - 'a']++;
                }
                for (int i = 1; i < 26; ++i)
                {
                    iterationCount[i] += iterationCount[i - 1];
                }
                for (int i = arrayToSort.Length - 1; i > -1; --i)
                {
                    outArray[iterationCount[arrayToSort[i][f] - 'a'] - 1] = arrayToSort[i];
                    iterationCount[arrayToSort[i][f] - 'a']--;
                }
                outArray.CopyTo(arrayToSort, 0);
            }
            return outArray;
        }
        static public string pass(string path)
        {
            StreamReader sr = new StreamReader(path);
            String str = "";
            List<string> strs = new List<string>();
            while ((str = sr.ReadLine()) != null)
            {
                strs.Add(str);
            }
            sr.Close();
            var arr = CountingSort(strs.ToArray());
            return arr[0] + Max + arr[arr.Length - 1];
        }
        public static char Max
        {
            get
            {
                int i = count.Max();
                for (int k = 0; k < count.Length; ++k)
                {
                    if (count[k] == i)
                        return (char)(k + 'a');
                }
                return '_';
            }
        }
    }
}
