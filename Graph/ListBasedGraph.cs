using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public class ListBasedGraph<VertexType,EdgeType>: GraphBase<VertexType,EdgeType>
    {
       
        List<List<Misc.Vertex<VertexType>>> data;// conatains links between
        public List<Misc.Edge<EdgeType>> edges = new List<Misc.Edge<EdgeType>>();//store all edges objects


        public ListBasedGraph(int VertexCount, bool oriented)
        {
            IsDirected = oriented;
            this.VertexCount = VertexCount;
            data = new List<List<Misc.Vertex<VertexType>>>(VertexCount);
            for (int i = 0; i < VertexCount; i++)
            {
                Misc.Vertex<VertexType> sample = new Misc.Vertex<VertexType>();
                sample.id = i;
                vertexes.Add(sample);//adding vertex to array(list)
                List<Misc.Vertex<VertexType>> temp = new List<Misc.Vertex<VertexType>>();
                data.Add(temp);// creating 2nd level of list
            }
        }      
     
        public Misc.GraphInnerForm GetInnerForm()////
        {
            return Misc.GraphInnerForm.ListBased;
        }
        public override GraphBase<VertexType,EdgeType> ConvertToMatrixBased()
        {
            throw new NotImplementedException();
        }
        public override GraphBase<VertexType,EdgeType> ConvertToListBased()
        {
            throw new NotSupportedException("Cant cast to same type");
        }

        Misc.Vertex<VertexType> GetVertexById(int vertexid)
        {
            for (int i = 0; i < vertexes.Count; i++)
                if (vertexes[i].id == vertexid) return vertexes[i]; 
            throw new Exception("Internal error: vertex error");//////////////////////////////////////////
        }

        bool VertexLinkExist(int checkvertexindex, int vertex2search)// shell extension over list.contains() method
        {
            List<Misc.Vertex<VertexType>> list = data[checkvertexindex];//extracting needed 2nd level list
            try
            {
                GetEdge(checkvertexindex,vertex2search);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }                      
           
        }

        public override bool AddEdge(int start, int  end, EdgeType val)// adds new edge with data specified
        {
            if (VertexLinkExist(start,end)) return false;
            Misc.Edge<EdgeType> edg = new Misc.Edge<EdgeType>();
            edg.StartVertexid = start;
            edg.EndVertexid = end;
            edg.Value = val;
            edges.Add(edg);
            data[start].Add(GetVertexById(end));
             if (!IsDirected)
             {
                 Misc.Edge<EdgeType> back = new Misc.Edge<EdgeType>();
                 back.StartVertexid = end;
                 back.EndVertexid = start;
                 back.Value = val;
                 edges.Add(back);
                 data[end].Add(GetVertexById(start)); 
                 EdgeCount++; }            
            EdgeCount++;
            return true;//operation succeded
        }

      
        public override bool DeleteEdge(int start, int end)// guess what it stands for?
        {
            if (!VertexLinkExist(start, end)) return false;
            data[start].Remove(GetVertexById(end));
            if (!IsDirected) { data[end].Remove(GetVertexById(start)); EdgeCount--; edges.Remove(GetEdge(end, start)); }
            edges.Remove(GetEdge(start, end));
            EdgeCount--;
            return true;// operation succeded
        }
        public override bool EdgeExists(int start, int end)// interface shell over internal function
        {
            return VertexLinkExist(start, end);
        }

        Misc.Edge<EdgeType> GetEdge(int start, int end)
        {
            if (IsDirected)
            {
                for (int i = 0; i < edges.Count; i++)
                    if ((edges[i].StartVertexid == start) && (edges[i].EndVertexid == end)) return edges[i];
            }
            else
            {
                for (int i = 0; i < edges.Count; i++)
                    if (((edges[i].StartVertexid == start) && (edges[i].EndVertexid == end)) || ((edges[i].StartVertexid == end) && (edges[i].EndVertexid == start))) return edges[i];
            }
            throw new Exception("Not found");

        }
        public override EdgeType GetEdgeWeight(int start, int end)
        {
            if (!VertexLinkExist(start, end)) throw new Exception("Not found");
            return GetEdge(start, end).Value;
        }
        public override bool SetEdgeWeight(int start, int end,EdgeType val)
        {
            if (!VertexLinkExist(start, end)) return false;
            GetEdge(start, end).Value = val;            
            return true;
        }

    }
}
