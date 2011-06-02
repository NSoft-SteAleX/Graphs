
using System.Drawing;
using System;

namespace Graph
{
   public  class Misc
    {
       public enum GraphInnerForm
       {
           MatrixBased,
           ListBased           
       }
       [Serializable]
       public class Edge<T>
       {
           public int StartVertexid;
           public int EndVertexid;
           public T Value;           
       }
       [Serializable]
       public class Vertex<T>
       {
           public T value;          
           public int id;           
           public Color _inner;
       }
    }
}
