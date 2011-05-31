using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace Graph
{

    interface ITask // defines minimum fumction set for task solvation class
    {
        void Set(GraphController<int, int> g);
        void Restart();
        object Result();
        void Solve();
    }

    class TasksOne:ITask// split graph into 2 parts, which dont have intersections insides it (2 colorued graph) 
    {
        GraphController<int, int> g;

        public void Solve()
        {
            solve();
        }
        void solve()
        {
            Color lastc = Color.Black;
            Misc.Vertex<int> curr;

            for (int i = 0; i < g.VertexCount; i++)// initialize vertices colors
            {
                curr = g.GetVertex(i);
                curr._inner = Color.Gray;
            }
            for (int i = 0; i < g.VertexCount; i++)
            {
                lastc = Color.Black;
                curr = g.GetVertex(i);              
                ColorRoute(curr, Color.White); 
            }
        }

            Color ColorRoute(Misc.Vertex<int > curr, Color inpc)  // inpc, income predicted color
            {
                Color newpc = Color.OrangeRed;// some mess to make compiler happy
                if (inpc == Color.White) newpc = Color.Black; else newpc = Color.White;
                List<Color> currlist = new List<Color>();
                Color back;
                int i = 0;            
                if (curr._inner != Color.Gray) return curr._inner;
                curr._inner = Color.Red;
                g.CreateIterator(curr.id);
                while (g.IsValid())
                {
                    Misc.Vertex<int> newcurr;
                    newcurr = g.GetVertex(g.Current());// get linked vertex        
                    back =  ColorRoute(newcurr,newpc);
                    if (back == Color.Red) { newcurr._inner = newpc; back = newpc; }
                    currlist.Add(back);             
                    g.CreateIterator(curr.id);
                    for (int j = 0; j < i; j++) g.MoveNext();                      
                    g.MoveNext();
                    i++;
                }

                if (currlist.Count > 0)
                {
                    bool iswhite = (currlist[0] == Color.White);
                    for (int j = 0; j < currlist.Count; j++)
                    {
                        if (currlist[j] == Color.Red) continue;
                        bool localcc = (currlist[j] == Color.White);
                        if (localcc != iswhite) throw new Exception("Cant find solution");
                    }
                    if (iswhite) curr._inner = Color.Black; else curr._inner = Color.White;
                }
                else curr._inner = inpc;
                //  check bypassed, things are looking good, now able to color current vertex               
                    return curr._inner;
            }


        
       public  void Set(GraphController<int, int> gin)
        {
            g = gin;
        }
        public object Result()
        {
            bool [] vrts = new bool[g.VertexCount];
            for (int i = 0; i < g.VertexCount; i++)
            {
                Misc.Vertex<int> cv = g.GetVertex(i);
                if (cv._inner == Color.Black) vrts[i] = true;
            }
            return vrts;
        }
        public void Restart()
        {
            solve();
        }
    }

    class TaskTwo 
    {
        GraphController<int, int> g;
        bool bg = false; // bg is bad graph, not bad game
        int basevertex;

        public void Set(GraphController<int, int> gin,int basevertex)
        {
            g = gin;
            this.basevertex = basevertex;
        }
        public object Result()
        {
            return null;
        }
        public void Restart()
        {
            MapGraph();
        }
        public void Solve()
        {
            MapGraph();
        }
        //-- internal part of class code

        // vertex data filelds mapping:    distance-->value;


        void MapGraph()// perfoms distance mapping
        {
            Misc.Vertex<int> cv,vcv; // current vertex, very current vertex xD
            for (int i = 0; i < g.VertexCount; i++)
            {
                cv = g.GetVertex(i);
                if (cv.id == basevertex) cv.value = 0; else cv.value = 100500;// asuming this is infinity
            }
            // second stage: distance calculating

            for (int inu = 0; inu < g.VertexCount; inu++)// inu - Index is Never Used
            {
                for (int i = 0; i < g.VertexCount; i++)
                {
                    g.CreateIterator(i);
                    cv = g.GetVertex(i);

                    while (g.IsValid())
                    {
                        vcv = g.GetVertex(g.Current());

                        if (cv.value + g.GetEdgeWeight(cv.id, vcv.id) < vcv.value)
                        {
                            vcv.value = cv.value + g.GetEdgeWeight(cv.id, vcv.id);
                        }
                        g.MoveNext();

                    }
                }
            }
                // now its time for 3rd stage: "searching for route cheats"

            for (int inu = 0; inu < g.VertexCount; inu++)// inu - Index is Never Used
            {
                for (int i = 0; i < g.VertexCount; i++)
                {
                    g.CreateIterator(i);
                    cv = g.GetVertex(i);

                    while (g.IsValid())
                    {
                        vcv = g.GetVertex(g.Current());

                        if (cv.value + g.GetEdgeWeight(cv.id, vcv.id) < vcv.value)
                        {
                            bg = true;
                        }
                        g.MoveNext();

                    }
                }
            }
            if (bg) throw new Exception("Result may be incorrect due to negative weight function");  
        }
    }
}
