using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _project
{
    class Task2
    {
        private static Int32 counter = 0;
        public static Int32 countInv(String dataPath, Int32 User1, Int32 User2)
        {
            StreamReader sr =
                new StreamReader(dataPath);
            String str = "";
            str = sr.ReadLine();
            Int32[] arr = new Int32[Int32.Parse(str.Split()[1])]; 
            String u1 = null,
                   u2 = null;
            while((str = sr.ReadLine()) != null)
            {
                if (str.Split()[0].Equals(User1.ToString()))
                    u1 = str;
                else if (str.Split()[0].Equals(User2.ToString()))
                    u2 = str;
                if(u1 != null && u2 != null)
                    break;
            }
            String[] arrU1 = u1.Split(),
                     arrU2 = u2.Split();
            printArr(arrU1, "user1");
            printArr(arrU2, "user2");
            for (Int32 i = 1; i < arrU1.Length; ++i)
            {
                arr[Int32.Parse(arrU1[i]) - 1] = Int32.Parse(arrU2[i]);
            }
            counter = 0;
            //SORT AND COUNT
            printArr(arr, "unsorted");
            sortAndCount(arr, 0, arr.Length - 1);
            printArr(arr, "sorted");
            return counter;
        }
        public static void printArr<T>(T[] arr, String title = "----------------------")
        {
            Console.WriteLine(title);
            foreach (var v in arr)
            {
                Console.Write(v + " ");
            }
            Console.WriteLine();
        }
        public static Int32[] sortAndCount(Int32[] s, Int32 begin, Int32 end)
        {
            if (begin == end)
                return s;
            Int32 split = (begin + end) / 2;
            Int32[] Left = new Int32[split - begin + 2];
            copy(sortAndCount(s, begin, split), Left, begin, split);
            Int32[] Right = new Int32[end - split + 1];
            copy(sortAndCount(s, split + 1, end), Right, split + 1, end);
            Left[Left.Length - 1] = Int32.MaxValue;
            Right[Right.Length - 1] = Int32.MaxValue;
            Int32 jL = 0, jR = 0;
            for (Int32 i = begin; i <= end; ++i)
            {
                if (Left[jL] < Right[jR])
                {
                    s[i] = Left[jL];
                    ++jL;
                }
                else
                {
                    s[i] = Right[jR];
                    ++jR;
                    counter += Left.Length - jL - 1;
                }
            }
            return s;
        }
        private static void copy(Int32[] arr1, Int32[] arrDist,
                                Int32 begin, Int32 end)
        {
            for (Int32 i = begin; i <= end; ++i)
                arrDist[i - begin] = arr1[i];
        }
    }
}
