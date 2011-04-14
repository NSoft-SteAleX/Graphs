using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Graph
{
   public  class Misc
    {
       public enum GraphInnerForm
       {
           MatrixBased,
           ListBased           
       }

       public class Edge<T>
       {
           public int StartVertexid;
           public int EndVertexid;
           public T Value;
           public Color rendercolor;
       }

       public class Vertex<T>
       {
           public T value;
           public int id;
           public Color rendercolor;
           public Color _inner;
       }
    }
}
