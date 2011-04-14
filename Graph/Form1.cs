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
    public partial class Form1 : Form
    {
        
        int sel1 = -1, sel2 = -1; Graphics gdi;
        GraphBase<int, int> test = new MatrixBasedGraph<int, int>(5, false);
        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                test = new MatrixBasedGraph<int, int>(Convert.ToInt32(textBox1.Text), checkBox1.Checked);
            }
            catch (Exception eeeee) { }
            PrintGraph(test);
        }       

        void l_MouseClick(object sender, MouseEventArgs e)
        {
            int clickedindex = Convert.ToInt32((sender as Label).Text); // edge id, where mouse has been clicked
            if ((sel1 != 11) && (sel2 != -1)) sel1 = sel2 = -1;
            if (sel1 == -1) { sel1 = clickedindex; PrintGraph(test); return; }
            if (sel2 == -1) { sel2 = clickedindex; PrintGraph(test); PrintGraph(test); return; }
           
            sel1 = sel2 = -1;// what the fuck has happened?
            PrintGraph(test);
        }

        void p_MouseClick(object sender, MouseEventArgs e)
        {
            int clickedindex = Convert.ToInt32((sender as Panel).Name); // edge id, where mouse has been clicked
            if (sel1 == -1) { sel1 = clickedindex; PrintGraph(test); return; }
            if (sel2 == -1) { sel2 = clickedindex; PrintGraph(test); return; }
            sel1=sel2=-1;// what the fuck has happened?
            PrintGraph(test);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
#if DEBUG
            label7.Text += " Debug mode!!!!! Not for showcase!!";
            label7.ForeColor = Color.Red;
#endif
            gdi = panel1.CreateGraphics();
            gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            PrintGraph(test);
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((sel1 != -1) && (sel2 != -1))
            {
                test.AddEdge(sel1, sel2, 1);
                
              //  sel1 = -1;
              //  sel2 = -1;
            }
            PrintGraph(test);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PrintGraph(test);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PrintGraph(test);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((sel1 != -1) && (sel2 != -1))
            {
                test.DeleteEdge(sel1, sel2);
                //UpdateStat();
            }
            PrintGraph(test);
        }   

        private void button5_Click(object sender, EventArgs e)// applying renderinf settings
        {
            try
            {
                edgesize = Convert.ToInt32(textBox2.Text);
                linewidth = Convert.ToInt32(textBox3.Text);
                if (comboBox1.SelectedIndex == 0) gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; else gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                PrintGraph(test);
            }
            catch (Exception eee)
            {
                MessageBox.Show("Think about what u typing");
            }
        }
             

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PrintGraph(test);
        }

        private void button6_Click(object sender, EventArgs e)// set weight button
        {
            if ((sel1 != -1) &&( sel2 !=-1))
            {
                try
                {
                    int setnum = Convert.ToInt32(textBox4.Text);
                    test.SetEdgeWeight(sel1, sel2, setnum);
                }
                catch (Exception eeeee){ }
            }
        }


        private void button7_Click(object sender, EventArgs e)
        {
            if ((sel1 != -1) && (sel2 != -2)) if (test.EdgeExists(sel1, sel2)) MessageBox.Show("It exists"); else MessageBox.Show("Doesnt exist");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            test.IsDirected = (sender as CheckBox).Checked;
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            sel1 = sel2 = -1;
            PrintGraph(test);
        }
    }
}
