using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;


namespace Graph
{
    class Engine
    {
       public  delegate void UpdateStatus(GraphController<int, int> g);
       public  delegate void LabelClickHandler(object sender, MouseEventArgs e);
       public  delegate void PanelClickHandler(object sender, MouseEventArgs e);
       public delegate void EdgeMouseDown(object sender, MouseEventArgs e);
       public delegate void EdgeMouseUp(object sender, MouseEventArgs e);
        UpdateStatus UpdateState;
        LabelClickHandler l_MouseClick;
        PanelClickHandler p_MouseClick;
        EdgeMouseDown l_MouseDown;
        EdgeMouseUp l_MouseUp;
       public  Engine(UpdateStatus us,LabelClickHandler l,PanelClickHandler p,EdgeMouseDown md, EdgeMouseUp mu)
        {
            UpdateState = us;
            l_MouseClick = l;
            p_MouseClick = p;
            l_MouseDown = md;
            l_MouseUp = mu;
        }

        //---------------------------------------RENDERING CORE V2---------------------------------------------------------------------------//

        //----- engine interaction scenarios---------------
        public void DrawFull(GraphController<int, int> g, int sel1, int sel2, int formatter, Graphics gdi,Control[] ctrls,Panel p, int edgesize, int xcenter, int ycenter, int xk, int yk, Image normal, Image iterator, Image selected)
        {
            InitializeCore(g, formatter,p,ctrls,edgesize,xcenter,ycenter,xk,yk);
            RenderVertices(g, 0, sel1, sel2,edgesize, ctrls, normal, iterator, selected);
            RenderLinks(g, gdi,ctrls);
            if (UpdateState!=null)UpdateState(g);
        }
        public void DrawWithoutColorOverriding(GraphController<int, int> g, int sel1, int sel2, int formatter, Graphics gdi, Control[] ctrls, Panel p, int edgesize, int xcenter, int ycenter, int xk, int yk, Image normal, Image iterator, Image selected)
        {
            InitializeCore(g, formatter, p, ctrls, edgesize, xcenter, ycenter, xk, yk);
            //RenderVertices(g, 0, sel1, sel2, edgesize, ctrls, normal, iterator, selected);
            RenderLinks(g, gdi, ctrls);
            if (UpdateState != null) UpdateState(g);

        }
        public void DrawIncrementalFast(GraphController<int, int> g, int sel1, int sel2, int edgesize, Graphics gdi, Control[] ctrls, Image normal, Image iterator, Image selected)
        {
            RenderVertices(g, 0, sel1, sel2, edgesize, ctrls, normal, iterator, selected);
            RenderLinks(g, gdi, ctrls);            
        }
       public void DrawIncremental(GraphController<int, int> g, int sel1, int sel2, int edgesize,Graphics gdi, Control[] ctrls,  Image normal, Image iterator, Image selected)
        {
            RenderVertices(g, 0, sel1, sel2,edgesize,ctrls,normal, iterator,selected);
            RenderLinks(g, gdi,ctrls);
            if (UpdateState != null) UpdateState(g);
        }
      public   void DrawFastMove(GraphController<int, int> g, int dx, int dy, Panel p, Graphics gdi, Control[] ctrls)
        {
            MoveEdges(dx, dy,p);
            RenderLinks(g, gdi,ctrls);
        }

        //---------------- low level core
        void InitializeCore(GraphController<int, int> g,int formatter, Panel pp, Control[] ctrls, int edgesize,int xcenter, int ycenter, int xk, int yk)// creates visual objects
        {
            //if (ctrls[0] == null) ctrls = new Control[100];
                double divconst = g.VertexCount / 2d;
                pp.Controls.Clear();
                //drawing vertexes
                for (int i = 0; i < g.VertexCount; i++)
                {
                    Label l = new Label();
                    l.Text = i.ToString();
                    l.Size = new System.Drawing.Size(edgesize, edgesize);
                    l.TextAlign = ContentAlignment.MiddleCenter;
                    if (l_MouseClick != null) l.MouseClick += new MouseEventHandler(l_MouseClick);
                    if (l_MouseDown!=null)l.MouseDown += new MouseEventHandler(l_MouseDown);
                   // if (l_MouseUp!=null)l.MouseUp+=new MouseEventHandler(l_MouseUp);
                    Panel p = new Panel();
                    p.Controls.Add(l);
                    p.Name = i.ToString();
                    p.BackgroundImageLayout = ImageLayout.Zoom;
                    p.BackColor = Color.Transparent;
                    if (p_MouseClick != null) p.MouseClick += new MouseEventHandler(p_MouseClick);                    
                    if (formatter==0) p.Location = RoundFormatter(divconst, i, xcenter, ycenter, xk, yk);
                    if (formatter == 1) p.Location = ColorFormatter(i,ctrls[i].BackColor);
                    pp.Controls.Add(p);
                    ctrls[i] = p;
                }                
           // }           
        }      

        void RenderVertices(GraphController<int, int> g, int formation, int sel1, int sel2, int edgesize,Control[] ctrls, Image normal, Image iterator, Image selected)// render vertices, formation: 0 - round
        {
            for (int i = 0; i < ctrls.Length; i++)
            {
                Control curr = ctrls[i];
                if (i == sel1 || i == sel2) curr.BackgroundImage = selected; else curr.BackgroundImage = normal;
                try { if (g.Current() == i) curr.BackgroundImage = iterator; }
                catch (Exception e) { }
                
                curr.Size = new System.Drawing.Size(edgesize, edgesize);
                curr.Controls[0].Size = new Size(edgesize, edgesize);
            }
        }

        void RenderLinks(GraphController<int, int> g, System.Drawing.Graphics gdi,Control[] ctrls)// renders links between vertices and directon markers
        {
            Bitmap buffer = new Bitmap((int)gdi.VisibleClipBounds.Width, (int)gdi.VisibleClipBounds.Height);
            Graphics localg = Graphics.FromImage(buffer);
            localg.SmoothingMode = gdi.SmoothingMode;
            localg.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                List<Rectangle> points = new List<Rectangle>();
                Pen pen = new Pen(Brushes.Black, 2);            
                Brush br = Brushes.Gray;
                SolidBrush sb = new SolidBrush(Color.FromArgb(90,90,90));
                FontFamily ff = new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif);
                Font f = new Font("Courier New",10,FontStyle.Regular);
            
               
                for (int i = 0; i < g.VertexCount; i++)
                    for (int j = 0; j < g.VertexCount; j++)
                    {
                        if (!g.EdgeExists(i, j)) continue;
                        Control c1 = GetControlbyName(i.ToString(), ctrls);
                        Control c2 = GetControlbyName(j.ToString(), ctrls);
                        Point p1 = c1.Location + new Size(c1.Size.Width / 2, c1.Size.Height / 2);
                        Point p2 = c2.Location + new Size(c2.Size.Width / 2, c2.Size.Height / 2);
                        //gdi.DrawLine(pen, p1, p2);
                        localg.DrawLine(pen, p1, p2);
                        Rectangle p = new Rectangle(p2 - new Size((int)(0.199 * (p2.X - p1.X)) + 5, (int)(0.199 * (p2.Y - p1.Y)) + 5), new Size(10, 10));
                        localg.DrawString(g.GetEdgeWeight(i,j).ToString(), f, sb, p.Location+new Size(10,0));
                        points.Add(p);
                    }
               
                    foreach (Rectangle r in points)
                    {
                        //gdi.FillEllipse(br, r);
                       if (g.IsDirected) localg.FillEllipse(br, r);
                    }                
                gdi.Clear(Color.White); 
                gdi.DrawImageUnscaled(buffer, 0, 0);              
          
        }

        void MoveEdges(int dx, int dy, Panel p)// incremently moves edges, provide fast rendering
        {
            foreach (Control c in p.Controls)
            {
                c.Location = new Point(c.Location.X + dx, c.Location.Y + dy);
            }
        }

        //----midleware level---------

        //----------- other stuff-------------

        Point RoundFormatter(double divconst, int step, int xcenter, int ycenter, int xk, int yk)// arranges objects in a shape of a circle
        {
            int currx = (int)(xcenter + Math.Cos((Math.PI / divconst) * step) * xk);
            int curry = (int)(ycenter - Math.Sin((Math.PI / divconst) * step) * yk);
            return new Point(currx, curry);
        }

        Point ColorFormatter(int step,Color c)
        {
            int currx;
            if (c == Color.White) currx = 50; else currx = 500;
            if (c == Color.Gray) throw new Exception("Algorithm error");
            return new Point(currx, step * 50+50);            

        }

        public Control GetControlbyName(string name, Control []ctrls)
        {
            foreach (Control cntrl in ctrls)
            {
                if (cntrl.Name == name) return cntrl;
            }
            throw new Exception("UIerror");
            
        }


        //------------------------END OF RENDERING CORE V2------------------------------//
    }
}
