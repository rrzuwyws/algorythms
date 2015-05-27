using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _project
{
    class Task9
    {
        public class Heap
        {
            int _count;
            int[,] nums;
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
            public Heap(int count)
            {
                _count = 0;
                nums = new int[count, 2];
            }
            public void add(int priority, int value)
            {
                nums[_count, 0] = priority;
                nums[_count, 1] = value;
                int index = _count;
                while (index > 0 && nums[index, 0] < nums[parentIndex(index), 0]) //!!
                {
                    int temp;

                    temp = nums[index, 0];
                    nums[index, 0] = nums[parentIndex(index), 0];
                    nums[parentIndex(index), 0] = temp;

                    temp = nums[index, 1];
                    nums[index, 1] = nums[parentIndex(index), 1];
                    nums[parentIndex(index), 1] = temp;

                    index = parentIndex(index);

                }
                ++_count;
            }
            public int[] pop()
            {
                int head = nums[0, 1];
                int key = nums[0, 0];
                --_count;
                nums[0, 0] = nums[_count, 0];
                nums[0, 1] = nums[_count, 1];
                int cIndex = 0;
                int lIndex = leftChildIndex(0);
                int rIndex = RightChildIndex(0);
                while ((lIndex < _count && nums[cIndex, 0] > nums[lIndex, 0]) ||   //!!
                        (rIndex < _count && nums[cIndex, 0] > nums[rIndex, 0]))   //!!
                {
                    int tempP = nums[cIndex, 0];
                    int tempV = nums[cIndex, 1];
                    if (lIndex >= _count || nums[lIndex, 0] > nums[rIndex, 0])   //!!
                    {
                        nums[cIndex, 0] = nums[rIndex, 0];
                        nums[rIndex, 0] = tempP;

                        nums[cIndex, 1] = nums[rIndex, 1];
                        nums[rIndex, 1] = tempV;

                        cIndex = rIndex;
                    }
                    else
                    {
                        nums[cIndex, 0] = nums[lIndex, 0];
                        nums[lIndex, 0] = tempP;

                        nums[cIndex, 1] = nums[lIndex, 1];
                        nums[lIndex, 1] = tempV;
                        
                        cIndex = lIndex;
                    }
                    rIndex = RightChildIndex(cIndex);
                    lIndex = leftChildIndex(cIndex);
                }
                return new int[] { key, head };
            }
            public bool decreaseKey(int value, int newKey)
            {
                int i = 0;
                while (i < nums.Length / 2 &&  nums[i, 1] != value)
                    ++i;
                if (!(i < nums.Length / 2) || newKey >= nums[i, 0])
                    return false;
                nums[i, 0] =  newKey;
                while (i > 0 && nums[parentIndex(i), 0] > nums[i, 0])
                {
                    int temp = nums[parentIndex(i), 0];
                    nums[parentIndex(i), 0] = nums[i, 0];
                    nums[i, 0] = temp;

                    temp = nums[parentIndex(i), 1];
                    nums[parentIndex(i), 1] = nums[i, 1];
                    nums[i, 1] = temp;

                    i = parentIndex(i);
                }
                return true;
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
                    return nums[i, 1];
                }
            }
        }
        public int[,] edges = null;
        BinaryHeap vertexs = null;
        public Task9(string path)
        {
            StreamReader sr = new StreamReader(path);
            string s = sr.ReadLine();
            int vertex = int.Parse(s.Split(' ')[0]);
            int edge = int.Parse(s.Split(' ')[1]);
            int indexer = 0;
            edges = new int[edge, 3];
            while ((s = sr.ReadLine()) != null)
            {
                edges[indexer, 0] = int.Parse(s.Split(' ')[0]);
                edges[indexer, 1] = int.Parse(s.Split(' ')[1]);
                edges[indexer, 2] = int.Parse(s.Split(' ')[2]);
                ++indexer;
            }
            sr.Close();
            vertexs = new BinaryHeap(vertex);
            sort(0, edge - 1);
            for (int i = 0; i < vertex; ++i)
                vertexs.Add(i + 1, int.MaxValue);
        }
        public void sort(int a, int b)
        {
            if (a >= b - 1)
                return;
            int[] x = { edges[b, 0], edges[b, 1], edges[b, 2] };
            int j = a - 1;
            for (int i = a; i < b; ++i)
            {
                if (edges[i, 0] < x[0])
                {
                    ++j;
                    int[] temp = { edges[i, 0], edges[i, 1], edges[i, 2] };
                    edges[i, 0] = edges[j, 0];
                    edges[i, 1] = edges[j, 1];
                    edges[i, 2] = edges[j, 2];
                    edges[j, 0] = temp[0];
                    edges[j, 1] = temp[1];
                    edges[j, 2] = temp[2];
                }
            }
            edges[b, 0] = edges[j + 1, 0];
            edges[b, 1] = edges[j + 1, 1];
            edges[b, 2] = edges[j + 1, 2];
            edges[j + 1, 0] = x[0];
            edges[j + 1, 1] = x[1];
            edges[j + 1, 2] = x[2];
            sort(a, j);
            sort(j + 2, b);
        }
        public int binaryGetIndex(int value)
        {
            int index = edges.Length / 6;
            int a = 0;
            int b = edges.Length / 3 - 1;
            while (edges[index, 0] != value && a != b)
            {
                if (edges[index, 0] > value)
                {
                    b = index - 1;
                }
                else
                {
                    a = index + 1;
                }
                index = (a + b) / 2;
            }
            while (index > 0 && edges[index, 0] == value)
                --index;
            return index + 1;
        }
        public int dijkstra(int from, int to)
        {
            vertexs.DecreasePriority(from, 0);
            int v = vertexs.ExtractMin();
            int[,] prev = new int[vertexs.Count, 2];
            for (int i = 0; i < prev.Length / 2; ++i){
                prev[i, 0] = -1;
                prev[i, 1] = 0;
            }
            prev[from - 1, 0] = from;
            while (vertexs.Count != 0 && v != to)
            {
                int index = binaryGetIndex(v);
                while (index <  edges.Length / 3 &&  edges[index, 0] == v)
                {
                    vertexs.DecreasePriority(edges[index, 1], prev[prev[v - 1, 0] - 1, 1] + edges[index, 2]);
                    prev[v - 1, 1] = edges[index, 2];
                    prev[v - 1, 0] = edges[index, 1];
                    ++index;
                }
                v = vertexs.ExtractMin();
            }
            if (prev[to - 1, 0] == -1)
                return -1;
            int sum = 0;
            v = to;
            do
            {
                sum += prev[v - 1, 1];
            } while ((v = prev[v - 1, 0]) != from);
            return sum;
        }
        public class KeyValue : IComparable<KeyValue>
        {
            public int key;
            public int value;
            public int CompareTo(KeyValue other)
            {
                if (this.key.CompareTo(other.key) == 0)
                    return this.value.CompareTo(other.value);
                return this.key.CompareTo(other.key);
            }
            public KeyValue(int value, int key)
            {
                this.key = key;
                this.value = value;
            }
            public KeyValue(int value) : this(value, int.MaxValue)
            { }
        }
        public class BinaryHeap
        {
            readonly int[] data;
            readonly int[] priorities;
            int count;

            public BinaryHeap(int capacity)
            {
                data = new int[capacity];
                priorities = new int[capacity];
                count = 0;
            }
            public void Add(int item, int priority)
            {
                if (count == data.Length)
                    throw new Exception("Heap capacity exceeded");
                int position = count++;
                data[position] = item;
                priorities[position] = priority;
                MoveUp(position);

            }
            public int ExtractMin()
            {
                int minNode = data[0];
                Swap(0, count - 1);
                count--;
                MoveDown(0);
                return minNode;
            }
            public void DecreasePriority(int n, int priority)
            {
                int i = 0;
                while (i < data.Length && data[i] != n)
                    ++i;
                if (i == data.Length)
                    return;

                int position = i;
                if (priorities[position] <= priority)
                    return;
                while ((position > 0) && (priorities[Parent(position)] > priority))
                {
                    int original_parent_pos = Parent(position);
                    Swap(original_parent_pos, position);
                    position = original_parent_pos;
                }
                priorities[position] = priority;
            }
            void MoveUp(int position)
            {
                while ((position > 0) && (priorities[Parent(position)] > priorities[position]))
                {
                    int original_parent_pos = Parent(position);
                    Swap(position, original_parent_pos);
                    position = original_parent_pos;
                }
            }
            void MoveDown(int position)
            {
                int lchild = LeftChild(position);
                int rchild = RightChild(position);
                int largest = 0;
                if ((lchild < count) && (priorities[lchild] < priorities[position]))
                {
                    largest = lchild;
                }
                else
                {
                    largest = position;
                }
                if ((rchild < count) && (priorities[rchild] < priorities[largest]))
                {
                    largest = rchild;
                }
                if (largest != position)
                {
                    Swap(position, largest);
                    MoveDown(largest);
                }
            }

            public int Count
            {
                get
                {
                    return count;
                }
            }
            void Swap(int position1, int position2)
            {
                int temp = data[position1];
                data[position1] = data[position2];
                data[position2] = temp;

                int temp2 = priorities[position1];
                priorities[position1] = priorities[position2];
                priorities[position2] = temp2;
            }
            static int Parent(int position)
            {
                return (position - 1) / 2;
            }
            static int LeftChild(int position)
            {
                return 2 * position + 1;
            }
            static int RightChild(int position)
            {
                return 2 * position + 2;
            }
            public void TestHeapValidity()
            {
                for (int i = 1; i < count; i++)
                    if (priorities[Parent(i)] > priorities[i])
                        throw new Exception("Heap violates the Heap Property at position " + i);
            }
        }
    }
}
