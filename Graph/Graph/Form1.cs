using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {       
        int sel1 = -1, sel2 = -1; Graphics gdi; int lmx=0, lmy=0,dx=0,dy=0;
        GraphController<int, int> test = new GraphController<int,int>(5,true, false);
        
        public Form1()
        {
            InitializeComponent();            
            CheckForIllegalCrossThreadCalls = false;          
        }
     

        private void button3_Click(object sender, EventArgs e)// create new graph
        {
            msx = 0;
            try
            {
                ctrls = new Control[Convert.ToInt32(textBox1.Text)];
                if (comboBox2.SelectedIndex == 0) test = new GraphController<int, int>(Convert.ToInt32(textBox1.Text), true, checkBox1.Checked);
                else test = new GraphController<int, int>(Convert.ToInt32(textBox1.Text), false, checkBox1.Checked);
                test.GenerateRandomLinks(Convert.ToInt32(textBox5.Text),1);
                SetLastOperationState(true, "Created");
                AddToReplay(2);
            }
            catch (Exception eeeee) { SetLastOperationState(false, "Bad parametrs"); }
            DrawFull(0);  
        }       


        private void Form1_Load(object sender, EventArgs e)
        {            
            en= new Engine(UpdateStat, l_MouseClick, p_MouseClick,l_MouseDown,null);
            selected = GetImageFromPath("layout\\SelectedEdge.png");
            normal = GetImageFromPath("layout\\NormalEdge.png");
            iterator = GetImageFromPath("layout\\IteratorEdge.png");
            gdi = panel1.CreateGraphics();
            gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gdi.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            DrawFull(0);
            AddToReplay(-1);
            comboBox2.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)// add edge
        {
            if ((sel1 != -1) && (sel2 != -1))
            {
                if (test.AddEdge(sel1, sel2, 1))
                {
                    SetLastOperationState(true, "Edge added");
                    AddToReplay(0);
                }
                else SetLastOperationState(false, "Cant add edge");            
            }
            DrawIncremental();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawIncremental();
        }

        private void button4_Click(object sender, EventArgs e)//refresh
        {
            DrawFull(0);    
        }

        private void button2_Click(object sender, EventArgs e)// delete edge
        {
            if ((sel1 != -1) && (sel2 != -1))
            {

                if (test.DeleteEdge(sel1, sel2))
                {
                    SetLastOperationState(true, "Deleted");
                    AddToReplay(1);
                }
                else SetLastOperationState(false, "Failed to delete");                
            }
            DrawIncremental();
        }   

        private void button5_Click(object sender, EventArgs e)// applying renderingsettings
        {
            try
            {
                edgesize = Convert.ToInt32(textBox2.Text);
                linewidth = Convert.ToInt32(textBox3.Text);
                if (comboBox1.SelectedIndex == 0) gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed; else gdi.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                DrawIncremental();
            }
            catch (Exception eee)
            {
                MessageBox.Show("Think about what u typing");
            }
        }
             

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawIncremental();
        }

        private void button6_Click(object sender, EventArgs e)// set weight button
        {
            if ((sel1 != -1) &&( sel2 !=-1))
            {
                try
                {
                    int setnum = Convert.ToInt32(textBox4.Text);
                    test.SetEdgeWeight(sel1, sel2, setnum);
                    SetLastOperationState(true, "Assigned");
                    DrawIncremental();
                }
                catch (Exception eeeee) { SetLastOperationState(false, "Failed to assign"); }
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

        private void panel1_Click(object sender, EventArgs e)// panel click
        {
            sel1 = sel2 = -1;
            DrawIncremental();
        }

        private void button9_Click(object sender, EventArgs e)//reset iterator
        {
            test.Reset();
            DrawIncremental();
            SetLastOperationState(true, "Iterator reset");
        }

        private void button10_Click(object sender, EventArgs e)//next
        {
            if (test.MoveNext()) SetLastOperationState(true, "Moved next"); else SetLastOperationState(false, "Cant move");
            DrawIncremental();
        }

        private void button11_Click(object sender, EventArgs e)//last button
        {
            if (test.MoveLast()) SetLastOperationState(true, "Set to last"); else SetLastOperationState(false, "No last element");
            DrawIncremental();            
        }   
     

        private void radioButton1_CheckedChanged(object sender, EventArgs e)//fast
        {
            if ((sender as RadioButton).Checked) diffspeed = 50;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)//mid
        {
            if ((sender as RadioButton).Checked) diffspeed = 20;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)//slow
        {
            if ((sender as RadioButton).Checked) diffspeed = 8;
        }

        private void button14_Click(object sender, EventArgs e)//right move
        {
            xcenter -= diffspeed;
            DrawFull(0);
        }

        private void button12_Click(object sender, EventArgs e)// left move
        {
            xcenter += diffspeed;
            DrawFull(0);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ycenter += diffspeed;
            DrawFull(0);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ycenter -= diffspeed;
            DrawFull(0);
        }

        private void button17_Click(object sender, EventArgs e)//plus
        {
            xk += 50;
            yk += 50;
            edgesize += 10;
            DrawFull(0);
        }

        private void button16_Click(object sender, EventArgs e)//minus
        {
            xk -= 50;
            yk -= 50;
            edgesize -= 10;
            DrawFull(0);

        }
      

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {     
            dx = 0;
            dy = 0;
            lmx = msx = e.X;
            lmy = msy = e.Y;      
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            int diifx = (e.X - msx);
            int diffy = (e.Y - msy);
           
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {                
                xcenter += diifx;
                ycenter += diffy;                   
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {    
                medge = "";              
                return;
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                medge = "";
                xk -= diffy/2;
                yk -= diffy/2;
                edgesize -= diffy/10;
            }           
            msx = msy = -1;
            DrawIncremental();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {          
            if (msx == -1) return;
            dx = e.X - lmx;
            dy = e.Y - lmy;
            lmx = e.X;
            lmy = e.Y;
            if (e.Button==System.Windows.Forms.MouseButtons.Left) DrawFastMove();
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Middle)
                {

                    if (dx!=-1)(en.GetControlbyName(medge, ctrls)).Location = (en.GetControlbyName(medge, ctrls)).Location + new Size(dx, dy);
                    DrawIncrementalFast();
                }
            }
                
            catch(Exception eee){}
           
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Replayer_player player = new Replayer_player(ref test);
            player.history = history;
            player.selected = selected;
            player.normal = normal;
            player.ShowDialog();
            test = player.GetActualState();
            DrawIncremental();
        }

     
        private void button19_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("You doing this at your own risk! Are you sure you want to move on?", "Warning!!!", MessageBoxButtons.YesNo);
            if (dr==System.Windows.Forms.DialogResult.No) return;
            groupBox7.Enabled = true;
            button19.Enabled = false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            test.Begin();
            DrawIncremental();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            DrawFull(0);
            try
            {
                TasksOne t1 = new TasksOne();
                t1.Set(test);
                t1.Solve();
                for (int i = 0; i < test.VertexCount; i++)
                {
                    (en.GetControlbyName(i.ToString(), ctrls)).BackColor = test.GetVertex(i)._inner;
                }
                DrawFull(1);
                for (int i = 0; i < test.VertexCount; i++)
                {
                    if (test.GetVertex(i)._inner == Color.Black) (en.GetControlbyName(i.ToString(), ctrls)).BackgroundImage = iterator; else (en.GetControlbyName(i.ToString(), ctrls)).BackgroundImage = normal;
                }
                AddToReplay(3);
                SetLastOperationState(true, "Task executed");
            }

            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "\nThis will not affect app work", "error during solution search", MessageBoxButtons.OK);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
           // { if (msx == -1) DrawIncrementalFast(); }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            test.Convert2anothertype();
            DrawFull(0);
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {
            label12.Visible = true;
        }

        private void groupBox5_Leave(object sender, EventArgs e)
        {
            label12.Visible = false;
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = "explorer.exe";
            prc.StartInfo.Arguments = "http://nsoft.ucoz.ru";
            prc.Start();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            label12.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            DrawFull(0);
            try
            {
                Task2Dialog td = new Task2Dialog();
                td.maxVertex = test.VertexCount;
                td.ShowDialog();
                if (!td.confirmed) return;
                TaskTwo tt = new TaskTwo();
                tt.Set(test, td.selectedval);
                tt.Solve();
                tt = new TaskTwo();                
                for (int i = 0; i < test.VertexCount; i++)
                {
                    Misc.Vertex<int> cv = test.GetVertex(i);
                    if (cv.value == td.distance) en.GetControlbyName(i.ToString(), ctrls).BackgroundImage = iterator;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message + "\nThis will not affect app work", "error during solution search", MessageBoxButtons.OK);
            }         
                
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Replayer_player player = new Replayer_player(ref test);
            player.history = history;
            player.selected = selected;
            player.normal = normal;
            player.ShowDialog();
            test = player.GetActualState();
            DrawIncremental();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            RandomWeights rndw = new RandomWeights(test);
            rndw.ShowDialog();
            DrawIncremental();
        }
        
        
    }
}
