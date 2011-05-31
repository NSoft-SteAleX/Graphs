using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary; 

namespace Graph
{
    public partial class Form1 : Form
    {
        public Image selected, normal,iterator;
        int edgesize = 32; int linewidth = 2;//rendering parameters
        int xk = 140, yk = 125,xcenter=170,ycenter=130,diffspeed=50;//rendering parameters
        int msx=-1, msy=-1;//mouse movement related variables
        Control[] ctrls = new Control[5];
        Engine en;
        List<Replay> history = new List<Replay>();
        string medge;

       public  class Replay
        {
           public  Control []ctrls;
           public  GraphController<int,int> state;
           public  int opid;
        };

        void AddToReplay(int code)
        {
            Replay rp = new Replay();
            rp.ctrls = new Control[ctrls.Length];           
            rp.state = (GraphController<int,int>)CopyEverything.Copy(test);
            rp.opid = code;
            history.Add(rp);
        }
        /// <summary>
        /// This copy function is written by Ashkan Ghodrat 
        /// </summary>
        public static class CopyEverything// perform deep object copy in order to save current state for replay
        {
            public static object Copy(object source)
            {
                if (source == null)
                    return null;
                else
                {
                    IFormatter formatter = new BinaryFormatter();
                    Stream stream = new MemoryStream();

                    using (stream)
                    {
                        formatter.Serialize(stream, source);
                        stream.Seek(0, SeekOrigin.Begin);
                        return formatter.Deserialize(stream);
                    }
                }
            }
        }
        void SetLastOperationState(bool isgood, string comment)
        {
            if (isgood) label7.BackColor = Color.Green; else label7.BackColor = Color.Red;
            label7.Text = comment;

        }

        Image GetImageFromPath(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            Image img = Image.FromStream(fs);
            fs.Close();
            return img;
        }
        void DrawFull(int formatter)
        {
            en.DrawFull(test, sel1, sel2,formatter, gdi, ctrls, panel1, edgesize, xcenter, ycenter, xk, yk, normal, iterator, selected);
        }
       void  DrawWithoutColorOverriding(int formatter)
        {
            en.DrawWithoutColorOverriding(test, sel1, sel2, formatter, gdi, ctrls, panel1, edgesize, xcenter, ycenter, xk, yk, normal, iterator, selected);
        }
        void DrawIncremental()
        {
            en.DrawIncremental(test, sel1, sel2,edgesize, gdi, ctrls, normal, iterator, selected);
        }
        void DrawIncrementalFast()
        {
            en.DrawIncrementalFast(test, sel1, sel2, edgesize, gdi, ctrls, normal, iterator, selected);
        }
        void DrawFastMove()
        {
            en.DrawFastMove(test, dx, dy, panel1, gdi, ctrls);
        }


            void UpdateStat(GraphController<int, int> t)// updates bottom pannel information
            {
                int iteratorpointer = -1;
                try
                {
                    label10.Text = test.LastVertex.ToString();
                    if (test.IsValid()) panel3.BackColor = Color.LightGreen; else panel3.BackColor = Color.Red;
                    try { iteratorpointer = test.Current(); }
                    catch (Exception e) { iteratorpointer = -2; panel3.BackColor = Color.Red; }                   
                    label6.Text = "Info: Vertices: " + t.VertexCount + "   Edges: " + t.EdgeCount + "  Type: ";
                    if (!test.IslistBased()) label6.Text += "MatrixBased"; else label6.Text += "ListBased";
                    label6.Text+=" K: " + test.Getk;                    
                    if (t.EdgeExists(sel1, sel2)) panel2.BackgroundImage = GetImageFromPath("layout\\edgeexist.png"); else panel2.BackgroundImage = GetImageFromPath("layout\\edgenotexist.png");
                    textBox4.Text = test.GetEdgeWeight(sel1, sel2).ToString();
                }
                catch (Exception eeee) { textBox4.Text = "N/a"; return; }
            }
      

 
//--------------------------OBJECT DEPENDENT CODE--------------------------------------//
        void l_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int clickedindex = Convert.ToInt32((sender as Label).Text); // edge id, where mouse has been clicked
                if ((sel1 != -1) && (sel2 != -1)) sel1 = sel2 = -1;
                if (sel1 == -1) { sel1 = clickedindex; DrawIncremental(); return; }
                if (sel2 == -1) { sel2 = clickedindex; DrawIncremental(); return; }
                sel1 = sel2 = -1;// what the fuck has happened?                
            }
           // if (e.Button == System.Windows.Forms.MouseButtons.Middle) { emx = e.X; emy = e.Y; } 
            if (e.Button==System.Windows.Forms.MouseButtons.Right)
            {
                Control snder = sender as Control;
                Edge_Extension ex = new Edge_Extension();
                ex.text = "Edge# " + snder.Text;
                ex.StartPosition = FormStartPosition.Manual;
                ex.Location = new Point(this.Location.X+panel1.Location.X+snder.Location.X+snder.Parent.Location.X + edgesize * 2, this.Location.Y+panel1.Location.Y+snder.Location.Y+snder.Parent.Location.Y);
                ex.ShowDialog();
                if (ex.IteratorReset) test.CreateIterator(Convert.ToInt32(snder.Text));
            }
            DrawIncremental();
        }

        void p_MouseClick(object sender, MouseEventArgs e)
        {
            int clickedindex = Convert.ToInt32((sender as Panel).Name); // edge id, where mouse has been clicked
            if (sel1 == -1) { sel1 = clickedindex; DrawIncremental(); return; }
            if (sel2 == -1) { sel2 = clickedindex; DrawIncremental(); return; }
            sel1 = sel2 = -1;// what the fuck has happened?
            DrawIncremental();
        }

        void l_MouseDown(object sender, MouseEventArgs e)
        {
            medge =(sender as Label).Text;         
        }
    

        

    }
}
