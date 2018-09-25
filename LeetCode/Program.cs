using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeetCode
{
    public class Program
    {
        static void Main(string[] args)
        {
            //string S = "leet2code3";
            //var arr = binaryGap(10);
            //Console.ReadLine();

            //var min = getWindowString("ADOBECODEBANC", "ABC");

            //Solution solution = new Solution();
            //ListNode l1 = new ListNode(2) { next = new ListNode(4) { next = new ListNode(6) } };
            //ListNode l2 = new ListNode(5) { next = new ListNode(6) { next = new ListNode(4) } };
            //var ret = solution.AddTwoNumbers(l1, l2);
            //int fib = GetFibItem(7, 1, 1);
            //int t = GetItem(8);
            //int[] arrs = new int[] { 100, 22, 35, 45, 65, 33, 34, 35, 350 };
            //var ret = QuickSort(arrs, 0, arrs.Length - 1);

            var arr1 = BubleSort(new int[] { 1, 3, 2, 5 });
            var ret = ValidParentheses("([)]");

            var arr = new List<int>() { 3, 1, 2, 4 }.ToArray();
            var IC = new IntCompare();
            var ICT = new IntComparable();
            var r1 = IC.Compare(10, 9);
            var r2 = ICT.CompareTo(10);

            //var ret = GetMax(arrs, arrs.Length - 1);

            //int ret = GetAddTotal(2, 0);

            //var ret = HanNuota(3, 'A', 'C', 'B');
            //TreeNode node = new TreeNode(0);
            //List<int> list = new List<int>() { 22, 11, 12, 14, 44, 55 };
            //list.ForEach(i => node.GetBinaryTree(i));
            //node.InOrderTree(node.Root);

            Thread t1 = new Thread(() =>
           {
               Console.WriteLine(Student.st.Value.GetHashCode() + "t1");
               //Student.st.Value = new Student();//能改变当前线程中的值
               //Console.WriteLine(Student.st.Value.GetHashCode() + "t1");
               //Student.SetStudent();//不能改变当前线程中的值
               //Student.temp = new Student();
               //Thread.Sleep(10000);
               Student.temp = new Student() { Name = "leet2code3" };
               Console.WriteLine(Student.st.Value.GetHashCode() + "t1");
           });

            Thread t2 = new Thread(() =>
            {
                //Student.SetStudent();
                //Student.temp = new Student();
                Console.WriteLine(Student.st.Value.GetHashCode() + "t2");
            });

            t1.Start();
            //Thread.Sleep(2000);
            //t2.Start();
            Console.ReadLine();
        }

        #region  leetcode

        /* 
        *
        Input: S = "leet2code3", K = 10
        Output: "o"
        Explanation: 
        The decoded string is "leetleetcodeleetleetcodeleetleetcode".
        The 10th letter in the string is "o".
        * 
        */
        public static string SearchChar(string s, int k = 5)
        {
            int length = s.Length;
            int size = 0;
            string result = "";

            for (int i = 0; i < length; i++)
            {
                char c = s[i];
                if (char.IsDigit(c))
                {
                    size *= c - '0';
                }
                else
                {
                    size++;
                }
            }

            for (int i = length - 1; i >= 0; i--)
            {
                char c = s[i];
                k %= size;
                if (k == 0 && char.IsLetter(c)) return result = char.ToString(c);

                if (char.IsDigit(c))
                {
                    size /= c - '0';
                }
                else
                {
                    size--;
                }
            }
            return result;
        }
        public static int[] binaryGap(int N)
        {
            int[] A = new int[32];
            int t = 0;
            for (int i = 0; i < 32; ++i)
                if (((N >> i) & 1) != 0)
                    A[t++] = i;


            return A;
        }
        public static string getWindowString(string S, string T)
        {
            if (string.IsNullOrEmpty(S) || string.IsNullOrEmpty(T)) return "";
            Dictionary<char, int> dictT = new Dictionary<char, int>();
            for (int i = 0; i < T.Length; i++)
            {
                if (dictT.ContainsKey(T[i]))
                {
                    dictT[T[i]] = dictT[T[i]] + 1;
                }
                else
                {
                    dictT.Add(T[i], 1);
                }
            }

            int required = dictT.Count;

            // Left and Right pointer
            int l = 0, r = 0;

            // formed is used to keep track of how many unique characters in t
            // are present in the current window in its desired frequency.
            // e.g. if t is "AABC" then the window must have two A's, one B and one C.
            // Thus formed would be = 3 when all these conditions are met.
            int formed = 0;

            // Dictionary which keeps a count of all the unique characters in the current window.
            Dictionary<char, int> windowCounts = new Dictionary<char, int>();

            // ans list of the form (window length, left, right)
            int[] ans = { -1, 0, 0 };

            while (r < S.Length)
            {
                // Add one character from the right to the window
                char c = S[r];
                //int count = windowCounts[c];
                if (windowCounts.ContainsKey(c))
                {
                    windowCounts[c] += 1;
                }
                else
                {
                    windowCounts.Add(c, 1);
                }

                // If the frequency of the current character added equals to the
                // desired count in t then increment the formed count by 1.
                if (dictT.ContainsKey(c) && windowCounts[c] == dictT[c])
                {
                    formed++;
                }

                // Try and contract the window till the point where it ceases to be 'desirable'.
                while (l <= r && formed == required)
                {
                    c = S[l];
                    // Save the smallest window until now.
                    if (ans[0] == -1 || r - l + 1 < ans[0])
                    {
                        ans[0] = r - l + 1;
                        ans[1] = l;
                        ans[2] = r;
                    }

                    // The character at the position pointed by the
                    // `Left` pointer is no longer a part of the window.
                    windowCounts[c] = windowCounts[c] - 1;
                    if (dictT.ContainsKey(c) && windowCounts[c] < dictT[c])
                    {
                        formed--;
                    }

                    // Move the left pointer ahead, this would help to look for a new window.
                    l++;
                }

                // Keep expanding the window once we are done contracting.
                r++;
            }

            return ans[0] == -1 ? "" : S.Substring(ans[1], ans[2]);
        }
        public static int HanNuota(int n, char from, char middle, char to)
        {
            if (n == 1) { Console.WriteLine(from.ToString() + to.ToString()); return 0; }
            else
            {
                HanNuota(n - 1, from, to, middle);
                HanNuota(1, from, ' ', to);
                HanNuota(n - 1, middle, from, to);
            }
            return 0;
        }
        public static void Output(int n)
        {
            if (n == 1)
            {
                Console.WriteLine(n);
                return;
            }
            else
            {
                Output(n - 1);
                Console.WriteLine(n);
            }
        }

        public static int max = 0;
        public static int GetMax(int[] arr, int index)
        {
            if (arr == null || arr.Length == 0) return 0;

            if (index >= 0)
            {
                GetMax(arr, index - 1);
                if (arr[index] > max)
                {
                    max = arr[index];
                }
            }
            return max;
        }

        public static int GetAddTotal(int idx, int sum)
        {
            if (idx > 0)
                return GetAddTotal(idx - 1, idx + sum);
            return sum;
        }

        public static int GetItem(int n)
        {
            if (n == 1 || n == 2) { return 1; }
            if (n == 3) { return 2; }
            if (n > 3)
            {
                return GetItem(n - 1) + GetItem(n - 2) + GetItem(n - 3);
            }
            return n;
        }

        public static int GetFibItem(int n, int n1, int n2)
        {
            if (n == 1) return n1;
            else
            {
                if (n > 0)
                    return GetFibItem(n - 1, n2, n1 + n2);
            }
            return n1;
        }

        public static int GetRabbitCnt(int month, int sum)
        {
            if (month == 1) return 1;
            else
            {
                while (month > 0)
                {
                    if (month % 2 == 0)
                    {
                        return GetRabbitCnt(month - 1, 1 + sum);
                    }
                    //month--;
                }
            }
            return sum;
        }

        public static int[] BubleSort(int[] arr)
        {
            if (arr == null || arr.Length < 2) return arr;
            bool isExchanged = true;
            for (int i = 0; i < arr.Length && isExchanged; i++)//每次冒出一个数，共arr.Length次
            {
                isExchanged = false;
                for (int j = 0; j < arr.Length - i - 1; j++)//减去已经冒出的个数i，冒出的数已经确定大小
                {
                    if (arr[j] > arr[j + 1])
                    {
                        var tep = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tep;
                        isExchanged = true;//没有发生交换，则说明该序列是有序的，无需继续循环
                    }
                }
            }
            return arr;
        }
        public static int[] QuickSort(int[] arr, int low, int high)
        {
            if (low >= high) return arr;

            int idx = UnitSort(arr, low, high);
            QuickSort(arr, low, idx - 1);
            QuickSort(arr, idx + 1, high);

            return arr;
        }
        public static int UnitSort(int[] arr, int low, int high)
        {
            int key = arr[low];
            while (low < high)
            {
                while (arr[high] >= key && high > low)
                    high--;
                arr[low] = arr[high];
                while (arr[low] <= key && high > low)
                    low++;
                arr[high] = arr[low];
            }
            arr[low] = key;

            return low;
        }
        public static bool ValidParentheses(string str)
        {
            Dictionary<char, char> dics = new Dictionary<char, char>() {
                {'}','{' },
                {']','[' },
                {')','(' },
            };

            if (String.IsNullOrEmpty(str)) return true;
            Stack<char> stack = new Stack<char>();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (dics.ContainsKey(c))
                {
                    char top = stack.Count == 0 ? '#' : stack.Pop();
                    if (dics[c] != top)
                    {
                        return false;
                    }
                }
                else
                {
                    stack.Push(c);
                }
            }
            return stack.Count == 0;
        }
        #endregion
        public class Student
        {
            public string Name { get; set; }
            public static ThreadLocal<Student> st = new ThreadLocal<Student>(() =>
            {
                //Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                return temp;
            });
            public static Student temp = new Student() { Name = "开始" };
            public static void SetStudent()
            {
                temp = new Student();
            }
        }

        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }

            public ListNode GetMiddleNode(ListNode node)
            {
                if (node == null) return null;
                ListNode fast;
                ListNode slow;
                fast = slow = node;
                while (fast != null && fast.next != null)
                {
                    fast = fast.next.next;
                    slow = slow.next;
                }
                return slow;
            }
        }
        public class TreeNode
        {

            public int Val { get; set; }

            public TreeNode(int val)
            {
                Val = val;
            }
            public TreeNode Left { get; set; }

            public TreeNode Right { get; set; }

            public TreeNode Root { get; set; }

            public void GetBinaryTree(int val)
            {
                TreeNode parent;
                TreeNode newNode = new TreeNode(val);
                if (Root == null)
                {
                    Root = newNode;
                }
                else
                {
                    TreeNode curr = Root;
                    while (true)
                    {
                        parent = curr;
                        if (val < curr.Val)
                        {
                            curr = curr.Left;
                            if (curr == null)
                            {
                                parent.Left = newNode;
                                break;
                            }
                        }
                        else
                        {
                            curr = curr.Right;
                            if (curr == null)
                            {
                                parent.Right = newNode;
                                break;
                            }
                        }
                    }
                }
            }

            public void GetTree(int val)
            {
                TreeNode newCode = new TreeNode(val);
                if (Root == null)
                {
                    Root = newCode;
                }
                else
                {
                    TreeNode curr = Root;
                    TreeNode parent;
                    while (true)
                    {
                        parent = curr;
                        if (newCode.Val < curr.Val)
                        {
                            curr = curr.Left;
                            if (curr == null)
                            {
                                parent.Left = newCode;
                                break;
                            }
                        }
                        else
                        {
                            curr = curr.Right;
                            if (curr == null)
                            {
                                curr.Right = newCode;
                                break;
                            }
                        }
                    }
                }
            }

            public List<int> NodeValues = new List<int>();
            public void InOrderTree(TreeNode node)
            {
                if (node == null) return;
                InOrderTree(node.Left);
                NodeValues.Add(node.Val);
                InOrderTree(node.Right);
            }
        }
        public class Solution
        {
            public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
            {   //642+ 465=11
                //246
                //564
                ListNode dummyHead = new ListNode(0);
                ListNode p = l1, q = l2, curr = dummyHead;
                int carry = 0;
                while (p != null || q != null)
                {
                    int x = (p != null) ? p.val : 0;
                    int y = (q != null) ? q.val : 0;
                    int sum = carry + x + y;
                    carry = sum / 10;
                    curr.next = new ListNode(sum % 10);
                    curr = curr.next;
                    if (p != null) p = p.next;
                    if (q != null) q = q.next;
                }
                if (carry > 0)
                {
                    curr.next = new ListNode(carry);
                }
                return dummyHead.next;
                List<int> list1 = new List<int>();
                List<int> list2 = new List<int>();
                ListNode cur = l1;
                while (cur != null)
                {
                    list1.Add(cur.val);
                    cur = cur.next;
                }
                list1.Reverse();

                cur = l2;
                while (cur != null)
                {
                    list2.Add(cur.val);
                    cur = cur.next;
                }
                list2.Reverse();
                string s1 = "";
                list1.ForEach((t) => { s1 += t.ToString(); });
                string s2 = "";
                list2.ForEach((t) => { s2 += t.ToString(); });
                long ret = Convert.ToInt64(s1) + Convert.ToInt64(s2);
                List<char> chars = ret.ToString().ToList();
                ListNode node = new ListNode(Convert.ToInt32(chars[chars.Count - 1].ToString()));
                ListNode head = node;
                for (int i = chars.Count - 2; i >= 0; i--)
                {
                    ListNode next = new ListNode(Convert.ToInt32(chars[i].ToString()));
                    head.next = next;
                    head = next;
                }
                return node;
            }
        }
        public class IntCompare : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x.CompareTo(y);
            }
        }

        public class IntComparable : IComparable<int>
        {
            public int CompareTo(int other)
            {
                return CompareTo(other);
            }
        }
    }
}