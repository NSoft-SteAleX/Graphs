using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Graph
{
    public partial class Replayer_player : Form
    {
        
        public List<Form1.Replay> history;
        Form1.Replay curr;
        public Image selected, normal;//////////
        int cs=0;
        Engine e = new Engine(null, null, null,null,null);
        Graphics gdi;

        public Replayer_player()
        {
            InitializeComponent();
            gdi = panel1.CreateGraphics();
        }

        void MergeObjects()
        {
            panel1.Controls.Clear();
            for (int i = 0; i < curr.ctrls.Length; i++)
                panel1.Controls.Add(curr.ctrls[i]);
        }

        void DrawSnap()
        {
            MergeObjects();
            //e.DrawIncremental(curr.state, -1, -1,32, gdi, curr.ctrls, normal, null, selected);
            e.DrawFull(curr.state, -1, -1,0, gdi, curr.ctrls, panel1, 32, 350, 200, 200, 200, normal, null, selected);
            if (cs == 0) panel2.Visible = false; else panel2.Visible = true;
            if (cs == history.Count-1 ) panel3.Visible = false; else  panel3.Visible = true;
            panel5.Width = ((cs+1) * 100 / history.Count) * panel4.Width / 100;
            label1.Text = (cs + 1).ToString() +" out of " + history.Count;
            if (curr.opid == -1) label2.Text = "Initial state of graph";
            if (curr.opid == 0) label2.Text = "New link has been added";
            if (curr.opid == 1) label2.Text = "The link has been deleted";
        }

        private void Replayer_player_Load(object sender, EventArgs e)
        {
            if (history.Count == 0) this.Close();
            else
            {
                curr = history[0];
                DrawSnap();                
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            if (cs < history.Count-1) cs++;
            curr = history[cs];
            DrawSnap();

        }

        private void panel2_Click(object sender, EventArgs e)
        {
          

        }

        private void panel2_Click_1(object sender, EventArgs e)
        {
            if (cs > 0) cs--;
            curr = history[cs];
            DrawSnap();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            DrawSnap();
        }

      
    }
}
