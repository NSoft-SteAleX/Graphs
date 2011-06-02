using System;
using System.Collections.Generic;

namespace Graph
{
    [Serializable]
    class MatrixBasedGraph<VertexType,EdgeType>:GraphBase<VertexType,EdgeType>
    {
      public  Misc.Edge<EdgeType>[,] Data;
        public List<Misc.Vertex<VertexType>> Vertexes = new List<Misc.Vertex<VertexType>>();// contains vertex objects

        public MatrixBasedGraph(int VertexCount, bool oriented)
        {
            this.VertexCount = VertexCount;
            IsDirected = oriented;
            Data = new Misc.Edge<EdgeType> [VertexCount,VertexCount];
            for (int i = 0; i < VertexCount; i++)
            {
                Misc.Vertex<VertexType> temp = new Misc.Vertex<VertexType>();
                temp.id = i;
                Vertexes.Add(temp);
            }
        }
        public override Misc.Vertex<VertexType> GetVertex(int id)
        {
            return GetVertexById(id);
        }

        public Misc.GraphInnerForm GetInnerForm()////
        {
            return Misc.GraphInnerForm.MatrixBased;
        }

        public override GraphBase<VertexType, EdgeType> ConvertToMatrixBased()
        {
            throw new NotSupportedException("Cant cast to same type");
        }
        public override GraphBase<VertexType, EdgeType> ConvertToListBased()
        {
            throw new NotImplementedException();            
        }        
 

        Misc.Vertex<VertexType> GetVertexById(int vertexid)
        {
            for (int i = 0; i < Vertexes.Count; i++)
                if (Vertexes[i].id == vertexid) return Vertexes[i];
            throw new Exception("Internal error: vertex seek error");//////////////////////////////////////////
        }
       

        public override bool EdgeExists(int start, int end)// interface shell over internal function
        {
            if (start == end) return false;
            if (!IsDirected) { if ((Data[start, end] != null) && (Data[end, start] != null)) return true; else return false; }
            else if (Data[start, end] != null) return true; else return false;
        }

        Misc.Edge<EdgeType> GetEdge(int start, int end)
        {
            if (start == end) return null;
            if (Data[start, end] != null) return Data[start, end]; else return null;    
        }

        public override EdgeType GetEdgeWeight(int start, int end)
        {
            if (!EdgeExists(start, end)) throw new Exception("Not found");
            return GetEdge(start, end).Value;           
        }

        public override bool SetEdgeWeight(int start, int end, EdgeType val)
        {
            if (!EdgeExists(start, end)) return false;
            GetEdge(start, end).Value = val;           
            return true;
        }

        public override bool AddEdge(int start, int end, EdgeType val)// adds new edge with data specified
        {
            if (start == end) return false;
            if (EdgeExists(start, end)) return false;
            Misc.Edge<EdgeType> edg = new Misc.Edge<EdgeType>();
            edg.StartVertexid = start;
            edg.EndVertexid = end;
            edg.Value = val;            
            Data[start, end] = edg;
            if (!IsDirected) Data[end, start] = edg;
            EdgeCount++;
            return true;//operation succeded
        }

        public override bool DeleteEdge(int start, int end)// guess what it stands for?
        {
            if (!EdgeExists(start, end)) return false;
            Data[start, end] = null;
            if (!IsDirected) Data[end, start] = null;
            EdgeCount--;
            return true;// operation succeded
        }

        [Serializable]
        public class Iterator<VertexType,EdgeType> : ITeratorBase<VertexType>
        {
            MatrixBasedGraph<VertexType,EdgeType> _inner;
            int lineindex, basevertex; bool blocked = false;
           public  Iterator(MatrixBasedGraph<VertexType, EdgeType> g,int bx)
            {
                _inner = g ;
                lineindex = 0;
                basevertex = bx;
                Reset();
            }
           public  void Reset()
            {
                blocked = false;
                lineindex = -1;
                MoveNext(); 
            }

            public int Current()
            {
                if (!Isvalid()) throw new Exception("Iterator-not-set_itmacu");
                if (_inner.Data[basevertex, lineindex].EndVertexid==basevertex) return _inner.Data[lineindex, basevertex].StartVertexid;
                return _inner.Data[basevertex, lineindex].EndVertexid;
            }

            public   bool Isvalid()
            {
                if (blocked) return false;
                if (_inner.Data[basevertex, lineindex] == null) { blocked = true; return false; }
                if (lineindex > _inner.VertexCount){blocked=true; return false;}
                return true;
            }
           public bool MoveLast()
            {
               int lastnnull=-1;
                for(lineindex=0;lineindex<_inner.VertexCount;lineindex++)
                    if (_inner.Data[basevertex,lineindex]!=null) lastnnull=lineindex;
               if (lastnnull==-1) return false;
               lineindex=lastnnull;
               return true;

            }

           public  bool MoveNext()
            {
                if (blocked) return false;
                for (; lineindex < _inner.VertexCount; )
                {
                    lineindex++;
                    if (lineindex == _inner.VertexCount) { blocked = true; return false; }
                    if (_inner.Data[basevertex, lineindex] != null) return true;
                    
                }                    
                if (lineindex == _inner.VertexCount) blocked = true;
                return false;
            }

        }
    }
}
