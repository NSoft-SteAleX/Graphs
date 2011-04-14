using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    class MatrixBasedGraph<VertexType,EdgeType>:GraphBase<VertexType,EdgeType>
    {
        Misc.Edge<EdgeType>[,] data;

        public MatrixBasedGraph(int VertexCount, bool oriented)
        {
            this.VertexCount = VertexCount;
            IsDirected = oriented;
            data = new Misc.Edge<EdgeType> [VertexCount,VertexCount];
            for (int i = 0; i < VertexCount; i++)
            {
                Misc.Vertex<VertexType> temp = new Misc.Vertex<VertexType>();
                temp.id = i;
                vertexes.Add(temp);
            }
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
            for (int i = 0; i < vertexes.Count; i++)
                if (vertexes[i].id == vertexid) return vertexes[i];
            throw new Exception("Internal error: vertex seek error");//////////////////////////////////////////
        }
       

        public override bool EdgeExists(int start, int end)// interface shell over internal function
        {
            if (!IsDirected) { if ((data[start, end] != null) || (data[end, start] != null)) return true; else return false; }
            else if (data[start, end] != null) return true; else return false;
        }

        Misc.Edge<EdgeType> GetEdge(int start, int end)
        {
            if (data[start, end] != null) return data[start, end]; else throw new Exception("Not found");    
        }

        public override EdgeType GetEdgeWeight(int start, int end)
        {
            EdgeType val;
            if (EdgeExists(start, end))   return GetEdge(start, end).Value; 
            if (IsDirected) throw  new Exception("Not found");// in this case no chance that this edge is exists
            if (EdgeExists(end, start)) return GetEdge(start, end).Value;
            throw new Exception("Not found");// now thats for sure           
        }

        public override bool SetEdgeWeight(int start, int end, EdgeType val)
        {
            if (!EdgeExists(start, end)) return false;
            GetEdge(start, end).Value = val;
            return true;
        }

        public override bool AddEdge(int start, int end, EdgeType val)// adds new edge with data specified
        {
            if (EdgeExists(start, end)) return false;
            Misc.Edge<EdgeType> edg = new Misc.Edge<EdgeType>();
            edg.StartVertexid = start;
            edg.EndVertexid = end;
            edg.Value = val;            
            data[start, end] = edg;        
            EdgeCount++;
            return true;//operation succeded
        }

        public override bool DeleteEdge(int start, int end)// guess what it stands for?
        {
            if (!EdgeExists(start, end)) return false;
            data[start, end] = null;
            EdgeCount--;
            return true;// operation succeded
        }
    }
}
