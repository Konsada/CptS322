/******************************************
Keon Sadatian
CS 322 - HW 2
******************************************/


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPTS322
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Collections.Generic.List<int> randList = new List<int>();

            Random r = new Random();
            for (int i = 0; i < 10000; i++)
            {
                randList.Add(r.Next(0, 20000));
            }
            /*************PART 1*********************/
            HashSet<int> hash = new HashSet<int>();
            foreach (int i in randList)
            {
                hash.Add(i);
            }

            /******************PART 2****************/
            int repeated = 0;
            for (int i = 0; i < 10000; i++)
            {
                bool alreadyFound = false;
                // checks first to see if this value was already counted
                for (int k = i-1; k >= 0; k--)
                {
                    if (randList[k] == randList[i])
                    {
                        alreadyFound = true;
                        break;
                    }
                }
                // checks for how many times this value is repeated in the list
                for (int j = i+1; (j < 10000) && (!alreadyFound); j++)
                {
                    if (randList[i] == randList[j])
                    {
                        repeated++;
                    }
                }
            }
            /**************PART 3***************************/
            randList.Sort();

            int distinctNum = 1;
            for (int i = 1; i < 10000; i++)
            {
                if (randList[i - 1] != randList[i])
                {
                    distinctNum++;
                }
            }
            int distinct = 10000 - repeated;
            string sb = "1.)HashSet Method: " + hash.Count.ToString() + " distinct numbers \r\n\r\n" + 
                "Note: \"Adding the element to the hash table is done in O(n) time, \r\nthis is due to the " + 
                "fact that each element of the list of size \r\n'n' is added to the hashset and it is" + 
                "added in constant time \r\nsince the element is placed based off of its 'key'," + 
                "which is \r\nthe data value held by the list element.\"\r\n\r\n" +
                "2.)Algorithm Method: " + distinct.ToString() + " distinct numbers\r\n\r\n" +
                "3.)Sorted Method: " + distinctNum.ToString() + " distinct numbers\n";

            output.Text = sb;
        }
    }
}
