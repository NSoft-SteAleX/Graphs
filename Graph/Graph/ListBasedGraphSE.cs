/*
 Second version of listbased graph:
 * -Completely redesigned data structure
 * -Using VertexL class as vertex descriptor, dreived from classic Vertex class
 * */


using System;
using System.Collections.Generic;

using System.Text;

namespace Graph
{
    [Serializable]
    public class VertexL<VertexType,EdgeType> : Misc.Vertex<VertexType>
    {
       public  List<Misc.Edge<EdgeType>> list = new List<Misc.Edge<EdgeType>>();

    }

    // Second Edition of list based graph
    [Serializable]
    public class ListBasedGraphSE<VertexType,EdgeType>:GraphBase<VertexType,EdgeType>
    {
        VertexL<VertexType, EdgeType>[] data;

       public  ListBasedGraphSE(int VertexCount, bool oriented)
        {
            IsDirected = oriented;
            this.VertexCount = VertexCount;
            data = new VertexL<VertexType, EdgeType>[VertexCount];
            for (int i = 0; i < VertexCount; i++)
            {
                data[i] = new VertexL<VertexType, EdgeType>();
                data[i].list = new List<Misc.Edge<EdgeType>>();
                data[i].id = i;
            }
        }

       public Misc.GraphInnerForm GetInnerForm()////
       {
           return Misc.GraphInnerForm.ListBased;
       }
       public override GraphBase<VertexType, EdgeType> ConvertToMatrixBased()
       {
           throw new NotImplementedException();
       }
       public override GraphBase<VertexType, EdgeType> ConvertToListBased()
       {
           throw new NotSupportedException("Cant cast to same type");
       }
       public override Misc.Vertex<VertexType> GetVertex(int id)
       {
           return GetVertexById(id);
       }

       Misc.Vertex<VertexType> GetVertexById(int vertexid)
       {
           for (int i = 0; i < data.Length; i++)
               if (data[i].id == vertexid) return data[i];
           throw new Exception("Internal error: vertex seek error");//////////////////////////////////////////
       }

       Misc.Edge<EdgeType> GetEdge(int start, int end)
       {
           bool f1 = false, f2 = false; int i = 0;
           if (start == end) throw new Exception("not_found_pre_cond");

           for (i=0; i < data[start].list.Count; i++)
               if ((data[start].list[i].EndVertexid == end)&&(data[start].list[i].StartVertexid == start)) { f1 = true; break; }

           if (f1) return data[start].list[i];// in case of directed graph if 1st link exists, return it 
           if (IsDirected) throw new Exception("Not found_b1");

           for (i = 0; i < data[start].list.Count; i++)
               if ((data[start].list[i].EndVertexid == start) && (data[start].list[i].StartVertexid == end)) { f2 = true; break; }
           if (f2) return data[start].list[i];
           
           throw new Exception("Not found_g");// no luck this time
       }

       bool VertexLinkExist(int checkvertexindex, int vertex2search)// check link existance using getedge method
       {
           bool f = true;
           try
           {
               GetEdge(checkvertexindex, vertex2search);
               f = true;              
           }
           catch (Exception e)
           {
               f = false;
           }
           return f;       
           
       }

       public override bool EdgeExists(int start, int end)// interface shell over internal function
       {
           bool f = VertexLinkExist(start, end);
           return f;
       }

       public override bool AddEdge(int start, int end, EdgeType val)// adds new edge with data specified
       {
           if (start == end) return false;
           if (VertexLinkExist(start, end)) return false;
           Misc.Edge<EdgeType> edg = new Misc.Edge<EdgeType>();
           edg.StartVertexid = start;
           edg.EndVertexid = end;
           edg.Value = val;
           data[start].list.Add(edg);
           if (!IsDirected) data[end].list.Add(edg);       
           EdgeCount++;
           return true;//operation succeded
       }
       public override bool DeleteEdge(int start, int end)// guess what it stands for?
       {
           if (!VertexLinkExist(start, end)) return false;
           data[start].list.Remove(GetEdge(start,end));
           if (!IsDirected) { data[end].list.Remove(GetEdge(end,start));  }
           EdgeCount--;
           return true;// operation succeded
       }

       public override EdgeType GetEdgeWeight(int start, int end)
       {
           if (!VertexLinkExist(start, end)) throw new Exception("Not found");
           return GetEdge(start, end).Value;
       }
       public override bool SetEdgeWeight(int start, int end, EdgeType val)
       {
           if (!VertexLinkExist(start, end)) return false;
           GetEdge(start, end).Value = val;
           return true;
       }
        [Serializable]
        public class Iterator<VertexType,EdgeType> : ITeratorBase<VertexType>
        {
            ListBasedGraphSE<VertexType,EdgeType> _inner;
            int  basevertex; bool blocked = false;
            System.Collections.IEnumerator it;
           public  Iterator(ListBasedGraphSE<VertexType, EdgeType> g,int bx)
            {
                _inner = g;
                basevertex = bx;
                it = g.data[bx].list.GetEnumerator();
                Reset();
            }
           public  void Reset()
            {
                blocked = false;
                it = _inner.data[basevertex].list.GetEnumerator();
                it.MoveNext();
                               
            }

            public int Current()
           {
               int dat;
               if (blocked) throw new Exception("LSE_Curr_beg");
               blocked = true;               
               if ((it.Current as Misc.Edge<EdgeType>).StartVertexid == basevertex) dat= (it.Current as Misc.Edge<EdgeType>).EndVertexid;
               else dat= (it.Current as Misc.Edge<EdgeType>).StartVertexid;
               blocked = false;
                    return dat;
                
            }

            public   bool Isvalid()
            {
                try
                {
                    Current();
                    return true;
                }
                catch(Exception ee){blocked=true; return false;}
            }
           public bool MoveLast()
            {
                int attempts = 0;
                it = _inner.data[basevertex].list.GetEnumerator();
                try
                {
                    while (true)
                    {
                        it.MoveNext();
                        object useless= it.Current;
                        attempts++;
                    }
                }
                catch (Exception e) { }
                if (attempts == 0) return false;
                it = _inner.data[basevertex].list.GetEnumerator();
                for (int i = 0; i < attempts; i++)
                    it.MoveNext();

                    return true;
            }

           public  bool MoveNext()
            {
                try { it.MoveNext(); }
                catch (Exception e) { blocked = true;  return false; }
                return true;
            }

        }


    }
}
