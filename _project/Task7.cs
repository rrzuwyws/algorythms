using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _project
{
    class Task7
    {
        public class Node
        {
            public int Value { get; set; }
            public Node Left;
            public Node Right;
            public Node Parent;
            public Node(int val)
            {
                Value = val;
            }
        }
        public Node root = null;
        public Task7()
        {
        }
        public void initWrite(int[] values, ref int i, ref Node node)
        {

            if (!(i < values.Length))
                return;
            if (values[i] == 0)
            {
                node = null;
                return;
            }
            node = new Node(values[i]);
            ++i;
            initWrite(values, ref i, ref node.Left);
            ++i;
            initWrite(values, ref i, ref node.Right);
            if (node.Left != null)
                node.Left.Parent = node;
            if (node.Right != null)
                node.Right.Parent = node;
        }
        public void inOrderWrite(int[] array, ref int i, Node node)
        {
            if (!(i < array.Length) || node == null)
                return;
            if (node.Left != null)
                inOrderWrite(array, ref i, node.Left);
            node.Value = array[i];
            ++i;
            if (node.Right != null)
                inOrderWrite(array, ref i, node.Right);
        }
        public int[] inOrderArray(Node node)
        {
            if (node == null)
                return new int[0];
            int [] left = inOrderArray(node.Left);
            int [] right = inOrderArray(node.Right);
            int[] toReturn = new int[left.Length
                                     + 1
                                     + right.Length];
            int i = 0;
            foreach (var v in left)
                toReturn[i++] = v;
                toReturn[i++] = node.Value;
            foreach (var v in right)
                toReturn[i++] = v;
            if (i == 1 && gettingLeaves)
                leaves.AddLast(node.Value);
            return toReturn;
        }
        public LinkedList<int> getLeaves()
        {
            gettingLeaves = true;
            leaves = new LinkedList<int>();
            inOrderArray(root);
            gettingLeaves = false;
            return leaves;
        }
        bool gettingLeaves = false;
        LinkedList<int> leaves;
        public string inOrderString()
        {
            string s = "";
            foreach (var v in inOrderArray(root))
                s += v + " ";
            return s;
        }
        public int rootValue()
        {
            return root.Value;
        }
        public string[] getSums(int s, Node N)
        {
            List<string> l = new List<string>();
            string[] g = {""};
            if (N.Left != null)
                g = getSums(s, N.Left);
            foreach (var vg in g)
            {
                foreach (var subs in vg.Split('x'))
                {
                    if (l.IndexOf(subs) == -1)
                        l.Add(subs);
                }
            }
            g = new string[1];
            g[0] = getSumFromNode(s, N);
            foreach (var subs in g[0].Split('x'))
            {
                if (l.IndexOf(subs) == -1)
                    l.Add(subs);
            }
            if (N.Right != null)
                g = getSums(s, N.Right);
            foreach (var vg in g)
            {
                foreach (var subs in vg.Split('x'))
                {
                    if (l.IndexOf(subs) == -1)
                        l.Add(subs);
                }
            }
            return l.ToArray();
        }
        public string getSumFromNode(int s, Node n)
        {
            if (n.Value == s)
                return n.Value.ToString();
            if (n.Value > s)
                return "";
            string str1 = "";
            string str2 = "";
            if (n.Left != null) str1 = getSumFromNode(s - n.Value, n.Left);
            if (n.Right != null) str2 = getSumFromNode(s - n.Value, n.Right);
            if (str1.Equals(str2) && "".Equals(str1))
                return "";
            string strToRet = "";
            if (!"".Equals(str1))
            {
                foreach (var v in str1.Split('x'))
                    strToRet += n.Value + " " + v + "x";
            }
            if (!"".Equals(str2))
            {
                foreach (var v in str2.Split('x'))
                    strToRet += n.Value + " " + v + "x";
            }
            if (strToRet.Length != 0 && strToRet[strToRet.Length - 1] == 'x')
                strToRet = strToRet.Substring(0, strToRet.Length - 1);
            return strToRet;
        }
    }
}
/*** main
            StreamReader sr = new StreamReader("..\\..\\tests\\Task7\\input_1000a.txt");
            string [] s = sr.ReadLine().Split(' ');
            sr.Close();
            int[] vals = new int[s.Length];
            for (int i = 0; i < vals.Length; ++i)
            {
                vals[i] = int.Parse(s[i]);
            }
            Task7 t = new Task7();
            int j = 0;
            t.initWrite(vals, ref j, ref t.root);
            Console.WriteLine("Init: " + t.inOrderString());
            j = 0;
            vals = t.inOrderArray(t.root);
            Array.Sort(vals);//, new Comparison<int>((a, b) => b.CompareTo(a)));
            t.inOrderWrite(vals, ref j, t.root);
            Console.WriteLine("Sorted: " + t.inOrderString());
            Console.WriteLine(t.getLeaves().ElementAt(0) + " "
                            + t.getLeaves().ElementAt(1) + " "
                            + t.getLeaves().ElementAt(2) + "\n"
                            + t.rootValue() + "\n"
                            + t.getLeaves().ElementAt(t.getLeaves().Count - 1) + " "
                            + t.getLeaves().ElementAt(t.getLeaves().Count - 2) + " "
                            + t.getLeaves().ElementAt(t.getLeaves().Count - 3));


            Console.WriteLine(Environment.NewLine + Environment.NewLine + "_______________");
            foreach (var v in t.getSums(1940, t.root))
            {
                Console.WriteLine(v);
            }
            Console.ReadKey();
***/