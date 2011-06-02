/*
 Manage with internal graphs storage forms
 Route function calls to internal objects
 Automatically manage with the iterator of current form 
  
 * */
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic; 

namespace Graph
{
    [Serializable]
   public  class GraphController<VertexType,EdgeType>
    {
        private GraphBase<VertexType, EdgeType> g;
        private ITeratorBase<VertexType> it; 
       public  GraphController(int VertexCount,bool listbased,bool directed)
        {
            if (listbased) g = new ListBasedGraphSE<VertexType, EdgeType>(VertexCount, directed);
            else g = new MatrixBasedGraph<VertexType, EdgeType>(VertexCount, directed);
            PrepareIterator(0);
        }
       public int LastVertex { get; set; }
         void PrepareIterator(int bv)
        {
            LastVertex = bv;
            if (g is ListBasedGraphSE<VertexType, EdgeType>) it = new ListBasedGraphSE<VertexType,EdgeType>.Iterator<VertexType,EdgeType>(g as ListBasedGraphSE<VertexType,EdgeType>,bv); 
            else  it = new MatrixBasedGraph<VertexType, EdgeType>.Iterator<VertexType, EdgeType>(g as MatrixBasedGraph<VertexType,EdgeType>,bv);
        }
        public  void GenerateRandomLinks(int amount, EdgeType fill)
         {
             int maxEdges = VertexCount * (VertexCount - 1);
             if (IsDirected) maxEdges*=2;
             
             if (amount > maxEdges) amount = maxEdges;
             for (int i = 0; i < amount; i++)
             {
                 Random rnd = new Random();                 
                 if (!AddEdge(rnd.Next(0, VertexCount), rnd.Next(0, VertexCount),fill )) i--;
             }

         }
        public Misc.Vertex<VertexType> GetVertex(int id)
        {
            return g.GetVertex(id);
        }
         public bool IslistBased()
         {
             return g is ListBasedGraphSE<VertexType, EdgeType>;
         }
        public double Getk
        {
            get { return g.Getk; }
        }
        public int VertexCount
        {
            get { return g.VertexCount; }
            set { g.VertexCount = value; }
        }
        public int EdgeCount
        {
            get { return g.EdgeCount; }
        }
        public bool IsDirected
        {
            get { return g.IsDirected; }
            set { g.IsDirected = value; }
        }
        public bool AddEdge(int start, int end, EdgeType val)
        {
            return g.AddEdge(start, end, val);
        }
        public bool DeleteEdge(int start, int end)
        {
            return g.DeleteEdge(start, end);
        }
        public bool EdgeExists(int start, int end)
        {
            return g.EdgeExists(start, end);
        }
       public EdgeType GetEdgeWeight(int start, int end)
        {
            return g.GetEdgeWeight(start, end);
        }
        public bool SetEdgeWeight(int start, int end,EdgeType val)
        {
            return g.SetEdgeWeight(start, end, val);
        }

        //iterator calls
        public void CreateIterator(int vertex)
        {
            PrepareIterator(vertex);
        }
        public void Reset()
        {
            it.Reset();
        }
        public void Begin()
        {
            it.Reset();
        }
        public bool MoveNext()
        {
            return it.MoveNext();
        }
        public bool MoveLast()
        {
            return it.MoveLast();
        }
        public bool IsValid()
        {
            return it.Isvalid();
        }
        public int Current()
        {
            return it.Current();
        }

        //------converters

      public  void Convert2anothertype()
        {
            GraphBase<VertexType,EdgeType> ng;// will conatain new graph referance;
            if (g is MatrixBasedGraph<VertexType,EdgeType>) ng =  new ListBasedGraphSE<VertexType, EdgeType>(VertexCount, g.IsDirected);
            else ng = new MatrixBasedGraph<VertexType, EdgeType>(VertexCount, g.IsDirected);
            //objectless functional calls are routed to g object
            for (int i=0;i<VertexCount;i++)
                for (int j=0;j<VertexCount;j++)
                    if (EdgeExists(i, j))
                    {
                        ng.AddEdge(i, j,GetEdgeWeight(i, j));                       
                    }
            g = ng;
            PrepareIterator(0);
        }

    }
}
