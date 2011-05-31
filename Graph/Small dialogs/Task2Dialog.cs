using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Graph
{
    public partial class Task2Dialog : Form
    {
        public int maxVertex;// -->in
        public bool confirmed=false; // <--- out : true if confirm button pressed
        public int selectedval=0; //<---- out
        public int distance = 0;//<---------

        public Task2Dialog()
        {
            InitializeComponent();
        }

        private void Task2Dialog_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < maxVertex; i++)
                comboBox1.Items.Add(i.ToString());
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            confirmed = true;
            selectedval = comboBox1.SelectedIndex;
            try { distance = Convert.ToInt32(textBox1.Text); }
            catch (Exception ee) { this.Close(); }
            this.Close();
        }
    }
}
