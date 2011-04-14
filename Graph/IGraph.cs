
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Graph
{
  

    public abstract class GraphBase<VertexType,EdgeType>
    {
        public List<Misc.Vertex<VertexType>> vertexes = new List<Misc.Vertex<VertexType>>();// contains vertex objects
        

        public double Getk { get; protected set;}
        public int VertexCount { get; protected set; }
        public int EdgeCount { get; protected set; }
        public bool IsDirected { get;  set; }
        public abstract GraphBase<VertexType, EdgeType> ConvertToMatrixBased();
        public abstract GraphBase<VertexType, EdgeType> ConvertToListBased();
        public abstract bool AddEdge(int start, int end, EdgeType val);
        public abstract bool DeleteEdge(int start, int end);
        public abstract bool EdgeExists(int start, int end);
        public abstract EdgeType GetEdgeWeight(int start, int end);
        public abstract bool SetEdgeWeight(int start, int end,EdgeType val);
    }

}
