using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graph
{
   
    public partial class Edge_Extension : Form
    {
        public string text { set{label1.Text = value;}}
        public bool IteratorReset { get; protected set;}
        public Edge_Extension()
        {
            InitializeComponent();
        }

        private void Edge_Extension_Load(object sender, EventArgs e)
        {
            IteratorReset = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IteratorReset = true;
            this.Close();
        }
    }
}
