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
        int edgesize = 32; int linewidth = 2;
        int xk = 140, yk = 125;
        Image GetImageFromPath(string path)
        {
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open);
            Image img = Image.FromStream(fs);
            fs.Close();
            return img;
        }

        void UpdateStat(GraphBase<int, int> t)
        {

            if ((sel1 != -1) && (sel2 != -1))
            {
                try
                {
                    label6.Text = "Info: Vertex: " + t.VertexCount + " Edge: " + t.EdgeCount;
                    if (t.EdgeExists(sel1, sel2)) panel2.BackgroundImage = GetImageFromPath("layout\\edgeexist.png"); else panel2.BackgroundImage = GetImageFromPath("layout\\edgenotexist.png");
                    textBox4.Text = test.GetEdgeWeight(sel1, sel2).ToString();
                }
                catch (Exception eeee) { textBox4.Text = "N/a"; return; }
            }
        }

        Control GetControlbyName(string name)
        {
            foreach (Control cntrl in panel1.Controls)
            {
                if (cntrl.Name == name) return cntrl;
            }
            throw new Exception("UIerror");
        }


        void PrintGraph(GraphBase<int, int> g)
        {
            List<Rectangle> points = new List<Rectangle>();// store end of line point coordinates,which indicates direction;
            UpdateStat(g);//refresh indicators
            double divconst = g.VertexCount / 2d;
            gdi.Clear(Color.Gray);
            panel1.Controls.Clear();
            int currx = 0, curry = 0;
            //drawing vertexes
            for (int i = 0; i < g.VertexCount; i++)
            {
                Label l = new Label();
                l.Text = i.ToString();
                l.Size = new System.Drawing.Size(edgesize, edgesize);
                l.TextAlign = ContentAlignment.MiddleCenter;
                l.MouseClick += new MouseEventHandler(l_MouseClick);
                Panel p = new Panel();
                p.Controls.Add(l);
                p.Name = i.ToString();
                if (i == sel1 || i == sel2) p.BackgroundImage = GetImageFromPath("layout\\SelectedEdge.png"); else p.BackgroundImage = GetImageFromPath("layout\\NormalEdge.png");
                p.BackgroundImageLayout = ImageLayout.Zoom;
                p.BackColor = Color.Transparent;
                p.MouseClick += new MouseEventHandler(p_MouseClick);
                p.Size = new System.Drawing.Size(edgesize, edgesize);
                currx = (int)(170 + Math.Cos((Math.PI / divconst) * i) * xk);
                curry = (int)(130 - Math.Sin((Math.PI / divconst) * i) * yk);
                p.Location = new Point(currx, curry);
                panel1.Controls.Add(p);
            }
            //drawing edges      
            Pen pen = new Pen(Brushes.Red, 2);
            Brush br = Brushes.AliceBlue;
            for (int i = 0; i < g.VertexCount; i++)
                for (int j = 0; j < g.VertexCount;j++)
                {
                    if (!g.EdgeExists(i,j)) continue;
                    Control c1 = GetControlbyName(i.ToString());
                    Control c2 = GetControlbyName(j.ToString());
                    Point p1 = c1.Location + new Size(c1.Size.Width / 2, c1.Size.Height / 2);
                    Point p2 = c2.Location + new Size(c2.Size.Width / 2, c2.Size.Height / 2);
                    gdi.DrawLine(pen, p1, p2);
                    points.Add(new Rectangle(p2 - new Size((int)(0.199 * (p2.X - p1.X)) + 5, (int)(0.199 * (p2.Y - p1.Y)) + 5), new Size(10, 10)));
                }
            if (g.IsDirected)
            {
                foreach (Rectangle r in points)
                {
                    gdi.FillEllipse(br, r);
                }
            }
        }
        

    }
}
