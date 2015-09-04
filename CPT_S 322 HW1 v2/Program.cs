/***************************************
Keon Sadatian
CPT_S 322 - HW - 1 : B-tree
DUE: 9/4/2015
**************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_S_322_HW1_v2
{
    class Program
    {
        static void Main(string[] args)
        {
            Tree tree = new Tree();
            //string input = "5 2 1 33 28 94 2 10 72 3 4 5 8 43 34 99 3"; // used for testing purposes
            string input;
            string[] inputArray = null;

            Console.WriteLine("Enter a collection of numbers in the range " +
                "[0, 100], separated by spaces:");
            //Console.WriteLine(input);
            input = Console.ReadLine();
            inputArray = input.Split(' ');
            foreach (string s in inputArray)
            {
                tree.addNodeToTree(Convert.ToInt16(s));
            }
            Console.WriteLine("Tree Contents: ");
            tree.printAscendingOrder();
            Console.WriteLine("\nTree Statistics:");
            Console.WriteLine("\tNumber of Nodes: " + tree.count);
            Console.WriteLine("\tNumber of Levels: " + tree.maxHeight().ToString());
            Console.WriteLine("\tMinimum number of levels that a tree with " +
                tree.count + " nodes could have = " + Convert.ToInt16(Math.Log(tree.count)).ToString());
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
    /********************************
    Node class, responsible for storing
    value of data and pointers to left
    and right nodes.
    ********************************/
    class Node
    {
        private int data;
        private object p;

        public int Data
        {
            get { return data; }
            set
            { data = value; }
        }
        public Node Left
        {
            get; set;
        }
        public Node Right
        {
            get; set;
        }

        public Node() { } // default constructor
        public Node(int data) { Data = data; }
        public Node(int data, Node Left, Node Right) { this.data = data; this.Left = Left; this.Right = Right; }

    }
    /******************************************
    Tree class contains all functions implemented
    on the nodes, such as add, traverse/print,
    count max height and check if it is empty
    ****************************************/
    class Tree
    {
        private Node root;
        public int count // property of tree, number of added nodes to the tree
        {
            get; private set;
        }

        public Tree() { }

        public Tree(Node root) { this.root = root; }

        public bool addNodeToTree(int data) // adds node to tree, doesn't add duplicates
        {
            Node node = new Node(data);

            Node pNode = new Node();
            Node cNode = new Node();
            if (this.isEmpty())
            {
                this.root = node;
                if (this.root != null)
                {
                    this.count++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                cNode = root;
                while (cNode != null)
                {
                    if (node.Data < cNode.Data)
                    {
                        pNode = cNode;
                        cNode = cNode.Left;
                    }
                    else if (node.Data > cNode.Data)
                    {
                        pNode = cNode;
                        cNode = cNode.Right;
                    }
                    else
                    {
                        return false; // value is a duplicate
                    }
                }
                cNode = node;
                if (cNode == null)
                {
                    return false;
                }
                this.count++;
                if (cNode.Data < pNode.Data)
                {
                    pNode.Left = node;
                }
                else if (cNode.Data > pNode.Data)
                {
                    pNode.Right = node;
                }
            }
            return true;
        }

        public void printAscendingOrder() // calls corresponding method using this root
        {
            printAscendingOrder(this.root);
        }
        public void printAscendingOrder(Node node) // prints the bst in ascending order
        {
            if (node != null)
            {
                printAscendingOrder(node.Left);
                Console.Write(node.Data.ToString() + " ");
                printAscendingOrder(node.Right);
            }
        }

        public int maxHeight()
        {
            return maxHeight(this.root);
        }
        public int maxHeight(Node node) // calculates the max height of the bst
        {
            if (node == null)
            {
                return -1;
            }

            int highestLeft = maxHeight(node.Left);
            int highestRight = maxHeight(node.Right);

            if (highestLeft > highestRight)
            {
                return highestLeft + 1;
            }
            else
            {
                return highestRight + 1;
            }
        }
        public bool isEmpty() // checks if tree is empty to begin with.
        {
            if (this.root == null)
            {
                return true;
            }
            return false;
        }
    }
}
