using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _project
{
    class Task5
    {
        private class Heap
        {
            bool low;
            int _count;
            int[] nums;
            delegate bool comparer(int a, int b);
            int parentIndex(int index)
            {
                return (index + 1) / 2 - 1;
            }
            int leftChildIndex(int index)
            {
                return (index + 1) * 2 - 1;
            }
            int RightChildIndex(int index)
            {
                return (index + 1) * 2;
            }
            public Heap(int count, bool low = true)
            {
                this.low = low;
                _count = 0;
                nums = new int[count];
            }
            public void add(int elem)
            {
                nums[_count] = elem;
                int index = _count;
                comparer c;
                if (low)
                { c = (a, b) => a < b; }
                else { c = (a, b) => a > b; }
                while (index > 0 && !c(nums[index], nums[parentIndex(index)]))
                {
                    int temp;

                    temp = nums[index];
                    nums[index] = nums[parentIndex(index)];
                    nums[parentIndex(index)] = temp;

                    index = parentIndex(index);

                }
                ++_count;
            }
            public int pop()
            {
                int head = nums[0];
                nums[0] = nums[--_count];
                comparer c;
                if (low)
                { c = (a, b) => a < b; }
                else { c = (a, b) => a > b; }
                int cIndex = 0;
                int lIndex = leftChildIndex(0);
                int rIndex = RightChildIndex(0);
                while ( (lIndex < _count && c(nums[cIndex], nums[lIndex])) ||
                        (rIndex < _count && c(nums[cIndex], nums[rIndex])) )

                {
                    int temp = nums[cIndex];
                    if (lIndex >= _count || c(nums[lIndex], nums[rIndex]))
                    {
                        nums[cIndex] = nums[rIndex];
                        nums[rIndex] = temp;
                        cIndex = rIndex;
                    }
                    else
                    {
                        nums[cIndex] = nums[lIndex];
                        nums[lIndex] = temp;
                        cIndex = lIndex;
                    }
                    rIndex = RightChildIndex(cIndex);
                    lIndex = leftChildIndex(cIndex);
                }
                return head;
            }
            public int Count
            {
                get
                {
                    return _count;
                }
            }
            public int this[int i]
            {
                get
                {
                    if (i >= _count)
                        throw new ArgumentOutOfRangeException();
                    return nums[i];
                }
            }
        }
        private Heap lHeap;
        private Heap hHeap;
        public Task5(int count)
        {
            lHeap = new Heap(count, true);
            hHeap = new Heap(count, false);
        }
        public void add(int elem)
        {
            if (lHeap.Count != 0 && lHeap[0] > elem)
                lHeap.add(elem);
            else
                hHeap.add(elem);
            if (Math.Abs(lHeap.Count - hHeap.Count) > 1)
            {
                if (lHeap.Count > hHeap.Count)
                    hHeap.add(lHeap.pop());
                else
                    lHeap.add(hHeap.pop());
            }
        }
        public string medians()
        {
            if (lHeap.Count == hHeap.Count)
                return lHeap[0] + " " + hHeap[0];
            else
            {
                if (lHeap.Count > hHeap.Count)
                    return "" + lHeap[0];
                else
                    return "" + hHeap[0];
            } 
        }
        public int this[int i, int j]
        {
            get
            {
                if (i != 0 && i != 1)
                    throw new ArgumentOutOfRangeException();
                if (i == 0)
                    return lHeap[j];
                else
                    return hHeap[j];
            }
        }
    }
}
