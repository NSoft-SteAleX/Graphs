using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Graph
{
    public partial class RandomWeights : Form
    {
        GraphController<int, int> state;
        public RandomWeights(GraphController<int,int> current)
        {
            state = current;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int ub=2, lb=0;
            Random rnd = new Random();
            try
            {
                lb = Convert.ToInt32(textBox1.Text);
                ub = Convert.ToInt32(textBox2.Text);
            }
            catch (Exception ee) { }
            for (int i=0;i<state.VertexCount;i++)
            {
                for (int j=0;j<state.VertexCount;j++)
                {
                    if (state.EdgeExists(i,j)) state.SetEdgeWeight(i,j,rnd.Next(lb,ub));
                }
            }
            this.Close();

        }
        
    }
}
