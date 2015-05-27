using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _project
{
    class Task3
    {
        public static Int32 counter = 0;
        static Int32 split<T>(T[] arr, Int32 a, Int32 b) where T:IComparable<T>
        {
            T x = arr[b];
            Int32 i = a - 1;
            for (Int32 j = a; j < b; ++j)
            {
                ++counter;
                if (arr[j].CompareTo(x) < 1)
                {
                    ++i;
                    T temp = arr[j];
                    arr[j] = arr[i];
                    arr[i] = temp;
                }
            }
            arr[b] = arr[i + 1];
            arr[i + 1] = x;
            return i + 1;
        }
        public static void Sort<T>(T[] arr, Int32 a, Int32 b) where T : IComparable<T>
        {
            if (a >= b)
                return;
            Int32 m = split<T>(arr, a, b);
            Sort(arr, a, m - 1);
            Sort(arr, m + 1, b);
        }
        static void swap<T>(T[] arr, Int32 a, Int32 b)
        {
            T temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
        public static void SortFirst<T>(T[] arr, Int32 a, Int32 b) where T : IComparable<T>
        {
            if (a >= b)
                return;
            swap(arr, a, b);

            Int32 m = split<T>(arr, a, b);
            SortFirst(arr, a, m - 1);
            SortFirst(arr, m + 1, b);
        }
        static Int32 getMed<T>(T[] arr, Int32 a, Int32 b) where T : IComparable<T>
        {
            Int32 counterBackup = counter;
            T[] temp = { arr[a], arr[b], arr[(a + b) / 2] };
            Sort(temp, 0, 2);
            counter = counterBackup;
            if (temp[1].CompareTo(arr[a]) == 0)
                return a;
            else if (temp[1].CompareTo(arr[b]) == 0)
                return b;
            else return (a + b) / 2;
        }
        public static void SortMed<T>(T[] arr, Int32 a, Int32 b) where T : IComparable<T>
        {
            if (a >= b)
                return;
            Int32 ind = getMed(arr, a, b);
            swap(arr, ind, b);
            Int32 m = split<T>(arr, a, b);
            SortMed(arr, a, m - 1);
            SortMed(arr, m + 1, b);
        }
    }
}
