using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Graphs
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var gr = new SimpleStaticGraph<int, int>(6, SimpleStaticGraph<int,int>.GraphType.NotOriented, SimpleStaticGraph<int,int>.GraphFormat.ListGraph);
            logBox.AppendText("M-Graph:\n");

            
            gr.InsertEdge(0, 4);
            gr.InsertEdge(0, 4);
            gr.InsertEdge(0, 5);
            gr.InsertEdge(1, 2);
            gr.InsertEdge(2, 3);
        }
    }
}
