using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CPT_S_322_HW1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            string[] inputArray = null;
            List<int> entry = new List<int>();
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Root = new BinaryTreeNode<int>();
            tree.Root.Value = -1;

            
            Console.WriteLine("Enter a collect of numbers in the range [0,100], separated by spaces:");
            input = Console.ReadLine();
            inputArray = input.Split(' ');
            foreach (string s in inputArray)
            {
                tree.Add(Convert.ToInt16(s));
            }
            Console.Write("Tree contents: ");
            tree.traverseTree();
            Console.Write("\n");
            Console.WriteLine("Tree Statistics:");
            Console.WriteLine("\tNumber of nodes: " + tree.count.ToString());
            int minHeight = (int)Math.Log(tree.count);
            Console.WriteLine("\tNumber of levels: " + tree.findHeight().ToString());
            Console.WriteLine("\tMinimum number of levels that a tree with " + tree.count.ToString() +
                " nodes could have = " + minHeight.ToString());
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
    /* Used "An Extensive Examination of Data Structures Using C# 2.0" for guidance*/
    /* https://msdn.microsoft.com/en-US/library/ms379572(v=VS.80).aspx */

    public class Node<T>
    {
        private T data;
        private NodeList<T> neighbors = null;
        public Node() { }
        public Node(T data) : this(data, null) { }

        public Node(T data, NodeList<T> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }
        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }
        protected NodeList<T> Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                this.neighbors = value;
            }
        }
    }
    public class NodeList<T> : Collection<Node<T>>
    {
        public NodeList() : base() { }

        public NodeList(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                base.Items.Add(default(Node<T>));
            }
        }

        public Node<T> FindByValue(T Value)
        {
            foreach (Node<T> node in Items)
            {
                if (node.Value.Equals(Value))
                {
                    return node;
                }
            }
            return null;
        }
    }

    public class BinaryTreeNode<T> : Node<T>
    {
        public BinaryTreeNode() : base() { }
        public BinaryTreeNode(T data) : base(data, null) { }
        public BinaryTreeNode(T data, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            base.Value = data;
            NodeList<T> children = new NodeList<T>(2);
            children[0] = left;
            children[1] = right;

            base.Neighbors = children;
        }

        public BinaryTreeNode<T> Left
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (BinaryTreeNode<T>)base.Neighbors[0];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeList<T>(2);

                base.Neighbors[0] = value;
            }
        }

        public BinaryTreeNode<T> Right
        {
            get
            {
                if (base.Neighbors == null)
                    return null;
                else
                    return (BinaryTreeNode<T>)base.Neighbors[1];
            }
            set
            {
                if (base.Neighbors == null)
                    base.Neighbors = new NodeList<T>(2);

                base.Neighbors[1] = value;
            }
        }
    }


    public class BinaryTree<T>
    {
        private BinaryTreeNode<T> root;
        public int count { get; set; }
        public BinaryTree()
        {
            root = null;
        }

        public virtual void Clear()
        {
            root = null;
        }

        public BinaryTreeNode<T> Root
        {
            get
            {
                return root;
            }
            set
            {
                this.root = value;
            }
        }

        public virtual bool Add(T data)
        {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(data);
            int result = 0;

            BinaryTreeNode<T> current = root, parent = null;
            if(Convert.ToInt16(current.Value) < 0)
            {
                root.Value = data;
                return true;
            }
            while(current != null)
            {
                result = Convert.ToInt16(current.Value) - Convert.ToInt16(data);
                if (result < 0) // data is greater than current.Value
                {
                    parent = current;
                    current = current.Right;
                }
                else if (result > 0) // data is less than current.Value
                {
                    parent = current;
                    current = current.Left;
                }
                else // (result == 0) data is equal to current.Value
                {
                    return false;
                }
            }

           count++;

           if (parent == null)
            {
                root = node;
            }
            else
            {
                result = Convert.ToInt16(data) - Convert.ToInt16(parent.Value);
                if(result > 0)
                {
                    parent.Right = node; // parent.Value is less than data
                }
                else
                {
                    parent.Left = node; // parent.Value is greater than data
                }
            }
            return true;
        }
        public void traverseTree()
        {
            traverseTree(root);
        }

        public void traverseTree(BinaryTreeNode<T> node) // ***needs work***
        {
            if (null != node)
            {
                traverseTree(node.Left);

                Console.Write(node.Value.ToString() + " ");

                traverseTree(node.Right);
            }
            
        }
        public bool isEmpty()
        {
            if (this.root == null)
                return true;
            else
                return false;
        }
        public int findHeight()
        {
            return findHeight(root);
        }
        protected int findHeight(BinaryTreeNode<T> node)
        {
            if(node == null)
            {
                return -1;
            }
            int leftHeight = findHeight(node.Left);
            int rightHeight = findHeight(node.Right);

            if(leftHeight > rightHeight)
            {
                return leftHeight + 1;
            }
            else
            {
                return rightHeight + 1;
            }
        }
    }
}
