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
            var gr = new SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>(6, SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.GraphType.NotOriented, SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.GraphFormat.ListGraph);
            logBox.AppendText("M-Graph:\n");
            
            gr.InsertEdge(0, 4);
            gr.InsertEdge(0, 4);
            gr.InsertEdge(0, 5);
            gr.InsertEdge(1, 2);
            gr.InsertEdge(2, 3);
            gr.SetEdge(2, 3, new EdgeDescriptor(2.25));
            gr.SetVertex(4, new VertexDescriptor("Test"));
            gr.SetVertex(5, new VertexDescriptor("Test2"));
            gr.SetEdge(0, 4, new EdgeDescriptor(2.125));
            var test = gr.GetVertex(5);
            MessageBox.Show(test.name);
        }
    }
}
