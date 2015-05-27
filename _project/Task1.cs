using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Numerics;

namespace _project
{
    static class Task1
    {
        static public int Counter = 0;
        static public int SomethToFind = 0;
        delegate String pad(String s);
        static public BigInteger Multiple(BigInteger X, BigInteger Y)
        {
            if (BigInteger.Abs(X) < 10 && BigInteger.Abs(Y) < 10)
                return X * Y;
            string stX = BigInteger.Abs(X).ToString();
            string stY = BigInteger.Abs(Y).ToString();
            if (stX.Length % 2 != 0) stX = "0" + stX;
            if (stY.Length % 2 != 0) stY = "0" + stY;
            if (stX.Length < stY.Length)
                stX = stX.PadLeft(stY.Length, '0');
            else
                stY = stY.PadLeft(stX.Length, '0');
            string a = stX.Substring(0, stX.Length / 2),
                   b = stX.Substring(stX.Length / 2),
                   c = stY.Substring(0, stY.Length / 2),
                   d = stY.Substring(stY.Length / 2);
            BigInteger ac = Multiple(BigInteger.Parse(a), BigInteger.Parse(c)),
                       bd = Multiple(BigInteger.Parse(b), BigInteger.Parse(d));
            BigInteger abcd = Multiple(BigInteger.Parse(a) + BigInteger.Parse(b),
                            BigInteger.Parse(c) + BigInteger.Parse(d)) - ac - bd;
            if (abcd == SomethToFind)
                ++Counter;
            return ac   * BigInteger.Parse("1".PadRight(stX.Length + 1    , '0')) +
                   abcd * BigInteger.Parse("1".PadRight(stX.Length / 2 + 1, '0')) +
                   bd;
        }
        static BigInteger mult(BigInteger s1, BigInteger s2)
        {
            return s1*s2;
        }
    }
    class longNum
    {
        public static readonly int Base = 1;
        bool signPositive = true;
        List<int> _num = new List<int>();
        public String Internal 
        { 
            get
            {
                string s = "";
                foreach (int a in _num)
                    s += a.ToString().PadLeft(Base, '0');
                while (s[0] == '0' && s.Length != 1)
                    s = s.Substring(1);
                return (signPositive ? "" : "-") + s;
            }
        }
        public static longNum operator +(longNum a, longNum b)
        {
            if(!b.signPositive){
                b.signPositive = true;
                return a - b;
            }
            if (!a.signPositive)
            {
                a.signPositive = true;
                return b - a;
            }

            longNum ret = new longNum();
            if (a._num.Count > b._num.Count)
            {
                ret = a;
                a = b;
                b = ret;
                ret = new longNum();
            }
            int over = 0;
            int i = 0;
            for (i = 0; i < a._num.Count; ++i)
            {
                int temp = a._num[i] + b._num[i] + over;
                if (temp.ToString().Length > Base)
                {
                    over = 1;
                    temp %= Int32.Parse("1".PadRight(Base + 1, '0'));
                }
                else over = 0;
                ret._num.Insert(0, temp);
            }
            for (; i < b._num.Count; ++i)
            {
                int temp = b._num[i] + over;
                if (temp.ToString().Length > Base)
                {
                    over = 1;
                    temp %= Int32.Parse("1".PadRight(Base, '0'));
                }
                else over = 0;
                ret._num.Insert(0, temp);
            }
            if (over == 1)
                ret._num.Insert(0, 1);
            return ret;
        }
        public static longNum operator -(longNum a, longNum b)
        {
            if (!a.signPositive)
            {
                a.signPositive = true;
                longNum ln = a + b;
                ln.signPositive = false;
                return ln;
            }
            if (a < b)
            {
                longNum ln = b - a;
                ln.signPositive = false;
                return ln;
            }

            longNum ret = new longNum();
            int over = 0;
            int i = 0;
            for (; i < b._num.Count; ++i)
            {
                int ins = a._num[i] - b._num[i] - over;
                if (ins < 0)
                {
                    ins = Int32.Parse("1".PadRight(Base + 1, '0')) + ins;
                    over = 1;
                }
                else over = 0;
                ret._num.Insert(0, ins);
            }
            for (; i < a._num.Count; ++i)
            {
                int ins = a._num[i] - over;
                if (ins < 0)
                {
                    ins = Int32.Parse("1".PadRight(Base + 1, '0')) + ins;
                    over = 1;
                }
                else over = 0;
                ret._num.Insert(0, ins);
            }
            return ret;
        }
        public static bool operator <(longNum a, longNum b)
        {
            if (a._num.Count != b._num.Count)
                return a._num.Count < b._num.Count;
            if (a._num[0] == b._num[0])
            {
                if (a._num.Count > 1)
                {
                    return new longNum(a.Internal.Substring(Base))
                        < new longNum(b.Internal.Substring(Base));
                }
                else return false;
            }
            return a._num[0] < b._num[0];
        }
        public static bool operator >(longNum a, longNum b)
        {
            return b < a;
        }
        private longNum() { }
        public longNum(Int32 i) : this(i.ToString()) { }
        public longNum(string num)
        {
            if (num[0] == '-')
            {
                signPositive = false;
                num = num.Substring(1);
            }
            else signPositive = true;
            int i = 0;
            for (i = num.Length - Base; i > 0; i -= Base)
                _num.Add(Int32.Parse(num.Substring(i, Base)));
            _num.Add(Int32.Parse(num.Substring(0, Base + i)));
        }
        public override string ToString()
        {
            return this.Internal;
        }
    }
}
