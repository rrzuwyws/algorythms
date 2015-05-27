using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace _project
{
    class Task8
    {
        readonly int elements;
        int[] vertex = null;
        List<int>[] edges = null;
        public Task8(int e, string path)
        {
            elements = e;
            vertex = new int[e];
            edges = new List<int>[e];
            for (int i = 0; i < e; ++i)
                edges[i] = new List<int>();
            StreamReader sr = new StreamReader(path);
            string s = null;
            while ((s = sr.ReadLine()) != null)
            {
                add(int.Parse(s.Split(' ')[0]), int.Parse(s.Split(' ')[1]));
            }
            sr.Close();
            
        }
        void add(int a, int b)
        {
            edges[a - 1].Add(b);
        }
        public string[] graphAsStrings(bool DFSs = false)
        {
            string[] ss = new string[edges.Length];
            for (int i = 0; i < edges.Length; ++i)
            {
                string s = (i + 1) + ": ";
                foreach (var v in edges[i])
                {
                    s += (DFSs ? vertex[v - 1] : v) + " ";
                }
                ss[i] = s;
            }
            return ss;
        }
        public void DFSStart()
        {
            int t = 1;
            for (int i = 0; i < vertex.Length; ++i)
            {
                if (vertex[i] == 0)
                {
                    t = DFS(i + 1, t);
                }
            }
        } 
        private int DFS(int v, int t)
        {
            int k = t;
            vertex[v - 1] = -1;
            for (int i = 0; i < edges[v - 1].Count; ++i)
            {
                if (vertex[edges[v - 1][i] - 1] == 0)
                    k = DFS(edges[v - 1][i], k);
            }
            vertex[v - 1] = k;
            return k + 1;
        }
        public void transpon()
        {
            List<int>[] temp = edges;
            edges = new List<int>[elements];
            for (int i = 0; i < elements; ++i)
                edges[i] = new List<int>();
            for (int i = 0; i < temp.Length; ++i)
                for (int j = 0; j < temp[i].Count; ++j)
                {
                    add(temp[i][j], i + 1);
                }
        }
        public string strongs()
        {
            transpon();
            Console.WriteLine("transponented");
            int[] order = new int[elements];
            for (int i = 0; i < elements; ++i)
            {
                order[elements - vertex[i]] = i ; 
            }
            vertex = new int[elements];
            int k = 1;
            int count = 1;
            StringBuilder sb = new StringBuilder();
            //string s = "";
            for (int i = 0; i < order.Length; ++i)
            {
                count = k;
                if (vertex[order[i]] == 0)
                    k = DFS(order[i] + 1, k);
                count = k - count;
                if (count != 0)
                {
                    sb.Append(count);
                    sb.Append(" ");
                    //s += count + " "; LOOOOOONG
                }
            }
            return sb.ToString();
        }
        //MAIN 
        //static void threaddelegate()
        //{
        //    Task8 t = new Task8(875714, "..\\..\\tests\\Task8\\input_08.txt");
        //    Console.WriteLine("Created");
        //    t.DFSStart();
        //    Console.WriteLine("Counted");
        //    string s = t.strongs();
        //    Console.WriteLine("Counted Strongs");
        //    int[] ints = new int[s.Split(' ').Length];
        //    int i = 0;
        //    foreach (var v in s.Split(' '))
        //    {
        //        if (v != "") ints[i] = int.Parse(v);
        //        ++i;
        //    }
        //    Array.Sort(ints, new Comparison<int>((a, b) => b.CompareTo(a)));
        //    foreach (var v in ints)
        //    {
        //        Console.Write(v + " ");
        //        Console.ReadKey();
        //    }
        //}
        //static void Main(string[] args)
        //{


        //    Thread T = new Thread(threaddelegate, 28022848);
        //    T.Start();
        //    //Console.WriteLine(ints[0] + " " +
        //    //                  ints[1] + " " +
        //    //                  ints[2] + " " +
        //    //                  ints[3] + " " +
        //    //                  ints[4]);
        //    //Console.WriteLine(ints[ints.Length - 0] + " " +
        //    //                  ints[ints.Length - 1] + " " +
        //    //                  ints[ints.Length - 2] + " " +
        //    //                  ints[ints.Length - 3] + " " +
        //    //                  ints[ints.Length - 4]);
        //    Console.ReadKey();

        //}
    }
}
