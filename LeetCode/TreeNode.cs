using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
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
}
