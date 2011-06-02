using System;


namespace Graph
{
  
    [Serializable]
    public abstract class GraphBase<VertexType,EdgeType>
    {
        public double Getk
        {
            get
            {
                double maxEdges = VertexCount * (VertexCount - 1);
                double k = EdgeCount / maxEdges;
                if (IsDirected)
                {
                    return k;
                }
                return k*2;
            }
            
        }
        public int VertexCount { get;  set; }
        public int EdgeCount { get; protected set; }
        public bool IsDirected { get;  set; }
        public abstract GraphBase<VertexType, EdgeType> ConvertToMatrixBased();
        public abstract GraphBase<VertexType, EdgeType> ConvertToListBased();
        public abstract bool AddEdge(int start, int end, EdgeType val);
        public abstract bool DeleteEdge(int start, int end);
        public abstract bool EdgeExists(int start, int end);
        public abstract EdgeType GetEdgeWeight(int start, int end);
        public abstract bool SetEdgeWeight(int start, int end,EdgeType val);
        public abstract Misc.Vertex<VertexType> GetVertex(int id);
    }
   
    public interface ITeratorBase<VertexType>
    {
        bool MoveLast();
        bool MoveNext();
        void Reset();
        int Current();
        bool Isvalid();
    }

}
