using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphs
{
    [Serializable]
    public class SimpleStaticGraph<VertexDataType, EdgeDataType> : ICloneable
    {
        #region Базовые классы
        /// <summary>
        /// Класс вершины
        /// </summary>
        [Serializable]
        private class Vertex
        {
            public enum Colors { Black, White, None }

            public int Number { get; private set; }
            public Colors Color { get; set; }
            public VertexDataType Data { get; set; }

            public Vertex(int number) : this(number, Colors.None) { }
            public Vertex(int number, VertexDataType data) : this(number, Colors.None, data) { }
            public Vertex(int number, Colors color)
            {
                this.Color = color;
                this.Number = number;
            }
            public Vertex(int number, Colors color, VertexDataType data)
            {
                this.Color = color;
                this.Data = data;
                this.Number = number;
            }
            public override bool Equals(object obj)
            {
                if (obj is Vertex && obj != null)
                {
                    Vertex temp = (Vertex)obj;

                    if (temp.Number == this.Number && temp.Color == this.Color && temp.Data.Equals(this.Data))
                    {
                        return true;
                    }
                }

                return false;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// Класс ребра
        /// </summary>
        [Serializable]
        private class Edge
        {
            public int StartVertex { get; private set; }
            public int EndVertex { get; private set; }
            public EdgeDataType Weight { get; set; }

            public Edge() { }
            public Edge(int startVertex, int endVertex)
            {
                this.StartVertex = startVertex;
                this.EndVertex = endVertex;
            }
            public Edge(int startVertex, int endVertex, EdgeDataType weight)
            {
                this.StartVertex = startVertex;
                this.EndVertex = endVertex;
                this.Weight = weight;
            }
            public override bool Equals(object obj)
            {
                if (obj is Edge && obj != null)
                {
                    Edge temp = (Edge)obj;

                    if (temp.StartVertex == this.StartVertex && temp.EndVertex == this.EndVertex 
                        && temp.Weight.Equals(this.Weight))
                    {
                        return true;
                    }
                }

                return false;
            }
            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        /// <summary>
        /// Перечисление форм графов
        /// </summary>
        public enum GraphFormat { MatrixGraph, ListGraph }
        
        /// <summary>
        /// Перечисление типов графов
        /// </summary>
        public enum GraphType { Oriented, NotOriented }

        /// <summary>
        /// Основной абстрактный класс графов,
        /// Служит базой для M- и L-форм
        /// </summary>
        [Serializable]
        private abstract class GeneralGraph
        {
            public int VertexCount { get; set; }
            public int EdgeCount { get; set; }
            public GraphType Direction { get; set; }

            public abstract bool InsertEdge(int v1, int v2);
            public abstract bool DeleteEdge(int v1, int v2);
            public abstract bool IsEdge(int v1, int v2);
            public abstract EdgeDataType GetEdge(int v1, int v2);
            public abstract bool SetEdge(int v1, int v2, EdgeDataType w);

            /// <summary>
            /// Вставка случайных рёбер
            /// </summary>
            /// <param name="randomEdgesCount"></param>
            protected void InsertRandomEdges(int randomEdgesCount)
            {
                Random rand = new Random(unchecked((int)DateTime.Now.Ticks));

                for (int i = 0; i != randomEdgesCount; i++)
                {
                    int v1 = rand.Next(0, VertexCount);
                    int v2 = rand.Next(0, VertexCount);

                    InsertEdge(v1, v2);
                }
            }

            /// <summary>
            /// Класс итератора
            /// </summary>
            public abstract class GeneralIterator
            {
                protected int V { get; set; }

                public GeneralIterator(int v)
                {
                    this.V = v;
                }
                public abstract void Begin();
                public abstract bool Next();
                public abstract void End();
                public abstract bool IsAlive();
                public abstract int Current();
            }
        }

        /// <summary>
        /// Класс M-графа
        /// </summary>
        [Serializable]
        private class MatrixGraph : GeneralGraph
        {
            /// <summary>
            /// Массив вершин
            /// </summary>
            private Vertex[] vertexArray;

            /// <summary>
            /// Матрица смежностей
            /// </summary>
            private Edge[,] matrix;

            public MatrixGraph(int vertexCount, int randomEdgesCount, GraphType direction)
            {
                this.VertexCount = vertexCount;
                this.Direction = direction;

                //Инициализация массивов
                matrix = new Edge[vertexCount, vertexCount];
                vertexArray = new Vertex[vertexCount];

                //Заполнение массива вершин
                for (int i = 0; i != vertexCount; i++) vertexArray[i] = new Vertex(i);

                InsertRandomEdges(randomEdgesCount);
            }
            public override bool InsertEdge(int v1, int v2)
            {
                if (matrix[v1, v2] == null)
                {
                    matrix[v1, v2] = new Edge(v1, v2);

                    if (Direction == GraphType.NotOriented)
                    {
                        matrix[v2, v1] = new Edge(v2, v1);
                    }

                    EdgeCount++;
                    return true;
                }

                return false;
            }
            public override bool DeleteEdge(int v1, int v2)
            {
                if (matrix[v1, v2] != null)
                {
                    matrix[v1, v2] = null;

                    if (Direction == GraphType.NotOriented)
                    {
                        matrix[v2, v1] = null;
                    }

                    EdgeCount--;
                    return true;
                }
                
                return false;
            }
            public override bool IsEdge(int v1, int v2)
            {
                if (matrix[v1, v2] != null)
                {
                    return true;
                }

                return false;
            }
            public override EdgeDataType GetEdge(int v1, int v2)
            {
                if (matrix[v1, v2] != null)
                {
                    return matrix[v1, v2].Weight;
                }
                else throw new NullReferenceException("Ребро, соединяющее точки " + v1 + " и " + v2 + " не существует!");
            }
            public override bool SetEdge(int v1, int v2, EdgeDataType w)
            {
                if (matrix[v1, v2] != null)
                {
                    matrix[v1, v2].Weight = w;

                    if (Direction == GraphType.NotOriented)
                    {
                        matrix[v2, v1].Weight = w;
                    }

                    return true;
                }

                return false;
            }

            //Класс итератора
            public class Iterator : GeneralIterator 
            {
                private int I { get; set; }
                private readonly MatrixGraph Graph;

                public Iterator(ref GeneralGraph graph, int v) : base(v) 
                {
                    this.Graph = (MatrixGraph)graph;
                    this.I = -1;
                }
                public override void Begin()
                {
                    I = -1;
                    Next();
                }
                public override void End()
                {
                    for (I = Graph.VertexCount - 1; I != -1; I--)
                    {
                        if (Graph.matrix[V, I] is Edge) break;
                    }
                }
                public override bool Next()
                {
                    for (I++; I < Graph.VertexCount; I++)
                    {
                        if (Graph.matrix[V, I] is Edge) return true;
                    }

                    return false;
                }
                public override bool IsAlive()
                {
                    return I < Graph.VertexCount;
                }
                public override int Current()
                {
                    if (IsAlive()) return I;
                    else return -1;
                }
            }
        }

        /// <summary>
        /// Класс L-графа
        /// </summary>
        [Serializable]
        private class ListGraph : GeneralGraph
        {
            /// <summary>
            /// Список смежностей
            /// </summary>
            private List<KeyValuePair<Vertex, Edge>>[] adjacencyList;

            /// <summary>
            /// Массив вершин
            /// </summary>
            private Vertex[] vertexArray;

            public ListGraph(int vertexCount, int randomEdgesCount, GraphType direction)
            {
                this.VertexCount = vertexCount;
                this.Direction = direction;

                //Инициализация списков
                adjacencyList = new List<KeyValuePair<Vertex, Edge>>[vertexCount];
                vertexArray = new Vertex[vertexCount];

                //Заполнение массива списков
                for (int i = 0; i != vertexCount; i++)
                {
                    adjacencyList[i] = new List<KeyValuePair<Vertex,Edge>>();
                    vertexArray[i] = new Vertex(i);
                }

                //Вставка случайных рёбер
                InsertRandomEdges(randomEdgesCount);
            }
            public override bool InsertEdge(int v1, int v2)
            {
                if (!adjacencyList[v1].Exists(pair => pair.Key.Number == v2))
                {
                    adjacencyList[v1].Add(new KeyValuePair<Vertex, Edge>(new Vertex(v2), new Edge(v1, v2))); //Вставка пары

                    if (Direction == GraphType.NotOriented) //Если не орграф
                    {
                        adjacencyList[v2].Add(new KeyValuePair<Vertex, Edge>(new Vertex(v1), new Edge(v2, v1)));
                    }

                    EdgeCount++;
                    return true;
                }
                
                return false;
            }
            public override bool DeleteEdge(int v1, int v2)
            {
                if (adjacencyList[v1].Exists(pair => pair.Key.Number == v2))
                {
                    adjacencyList[v1].RemoveAt(adjacencyList[v1].FindIndex(pair => pair.Key.Number == v2));

                    if (Direction == GraphType.NotOriented)
                    {
                        adjacencyList[v2].RemoveAt(adjacencyList[v2].FindIndex(pair => pair.Key.Number == v1));
                    }

                    EdgeCount--;
                    return true;
                }
                
                return false;
            }
            public override bool IsEdge(int v1, int v2)
            {
                if (adjacencyList[v1].Exists(pair => pair.Key.Number == v2))
                {
                    return true;
                }

                return false;
            }
            public override EdgeDataType GetEdge(int v1, int v2)
            {
                Edge e = adjacencyList[v1].Find(pair => pair.Value.StartVertex == v1 && pair.Value.EndVertex == v2).Value;

                if(e != null) return e.Weight;
                else throw new NullReferenceException("Ребро, соединяющее точки " + v1 + " и " + v2 + " не существует!");
            }
            public override bool SetEdge(int v1, int v2, EdgeDataType w)
            {
                if (Direction == GraphType.NotOriented)
                {
                    Edge e1 = adjacencyList[v1].Find(pair => pair.Value.StartVertex == v1 && pair.Value.EndVertex == v2).Value;
                    Edge e2 = adjacencyList[v2].Find(pair => pair.Value.StartVertex == v2 && pair.Value.EndVertex == v1).Value;

                    if (e1 != null && e2 != null)
                    {
                        e1.Weight = e2.Weight = w;
                        return true;
                    }
                }
                else
                {
                    Edge e = adjacencyList[v1].Find(pair => pair.Value.StartVertex == v1 && pair.Value.EndVertex == v2).Value;
                    if (e != null) e.Weight = w;
                }

                return false;
            }

            //Класс итератора
            public class Iterator : GeneralIterator
            {
                private readonly ListGraph Graph;
                private List<KeyValuePair<Vertex, Edge>>.Enumerator listIter;

                public Iterator(ref GeneralGraph graph, int v) : base(v)
                {
                    this.Graph = (ListGraph)graph;
                    this.listIter = Graph.adjacencyList[V].GetEnumerator();
                    this.listIter.MoveNext();
                }
                public override void Begin()
                {
                    listIter = Graph.adjacencyList[V].GetEnumerator();
                    listIter.MoveNext();
                }
                public override void End()
                {
                    throw new NotImplementedException();
                }
                public override bool Next()
                {
                    return listIter.MoveNext();
                }
                public override bool IsAlive()
                {
                    if (listIter.Current.Key != null && listIter.Current.Value != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                public override int Current()
                {
                    if (IsAlive())
                    {
                        return listIter.Current.Key.Number;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }

        /// <summary>
        /// Итератор
        /// </summary>
        public class Iterator
        {
            private readonly SimpleStaticGraph<VertexDataType, EdgeDataType> SimpleGraph;
            private readonly GeneralGraph.GeneralIterator Iter;

            /// <summary>
            /// Конструктор итератора
            /// </summary>
            /// <param name="_simpleGraph">Объект простого статического графа</param>
            /// <param name="v">Номер вершины</param>
            public Iterator(ref SimpleStaticGraph<VertexDataType, EdgeDataType> _simpleGraph, int v)
            {
                this.SimpleGraph = _simpleGraph;

                if (SimpleGraph.Dense() == GraphFormat.MatrixGraph)
                {
                    Iter = new MatrixGraph.Iterator(ref SimpleGraph.graph, v);
                }
                else
                {
                    Iter = new ListGraph.Iterator(ref SimpleGraph.graph, v);
                }
            }

            /// <summary>
            /// Устанавливает итератор на первую смежную вершину
            /// </summary>
            /// <returns></returns>
            public void Begin()
            {
                Iter.Begin();
            }

            /// <summary>
            /// Устанавливает итератор на последнюю смежную вершину
            /// </summary>
            public void End()
            {
                Iter.End();
            }

            /// <summary>
            /// Переход к следующей смежной вершине
            /// </summary>
            /// <returns></returns>
            public bool Next()
            {
                return Iter.Next();
            }

            /// <summary>
            /// Получение номера текущей смежной вершины
            /// </summary>
            /// <returns></returns>
            public int Current()
            {
                return Iter.Current();
            }
        }
        #endregion

        /// <summary>
        /// Базовый объект графа
        /// </summary>
        private GeneralGraph graph;

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public SimpleStaticGraph()
        {
            graph = new ListGraph(0, 0, GraphType.NotOriented);
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="vertexCount">Число вершин</param>
        /// <param name="direction">Тип графа</param>
        /// <param name="format">Форма представления графа</param>
        public SimpleStaticGraph(int vertexCount, GraphType direction, GraphFormat format) : this(vertexCount, 0, direction, format) { }

        /// <summary>
        /// Ведущий конструктор
        /// </summary>
        /// <param name="vertexCount">Число вершин</param>
        /// <param name="randomEdgesCount">Число случайных рёбер</param>
        /// <param name="direction">Тип графа</param>
        /// <param name="format">Форма представления графа</param>
        public SimpleStaticGraph(int vertexCount, int randomEdgesCount, GraphType direction, GraphFormat format)
        {
            if (format == GraphFormat.MatrixGraph)
            {
                graph = new MatrixGraph(vertexCount, randomEdgesCount, direction);
            }
            else
            {
                graph = new ListGraph(vertexCount, randomEdgesCount, direction);
            }
        }

        /// <summary>
        /// Вставка ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool InsertEdge(int v1, int v2)
        {
            return graph.InsertEdge(v1, v2);
        }

        /// <summary>
        /// Удаление ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool DeleteEdge(int v1, int v2)
        {
            return graph.DeleteEdge(v1, v2);
        }
        
        /// <summary>
        /// Проверка существования ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool IsEdge(int v1, int v2)
        {
            return graph.IsEdge(v1, v2);
        }

        /// <summary>
        /// Получение веса ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public EdgeDataType GetEdge(int v1, int v2)
        {
            return graph.GetEdge(v1, v2);
        }

        /// <summary>
        /// Установка веса ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool SetEdge(int v1, int v2, EdgeDataType w)
        {
            return graph.SetEdge(v1, v2, w);
        }

        /// <summary>
        /// Получение числа вершин графа
        /// </summary>
        /// <returns></returns>
        public int VertexCount()
        {
            return graph.VertexCount;
        }

        /// <summary>
        /// Получение числа рёбер графа
        /// </summary>
        /// <returns></returns>
        public int EdgeCount()
        {
            return graph.EdgeCount;
        }

        /// <summary>
        /// Получение формата графа (M- || L-)
        /// </summary>
        /// <returns></returns>
        public GraphFormat Dense()
        {
            if (graph is MatrixGraph)
                return GraphFormat.MatrixGraph;
            else
                return GraphFormat.ListGraph;
        }

        /// <summary>
        /// Получение типа графа
        /// </summary>
        /// <returns></returns>
        public GraphType Direction()
        {
            return graph.Direction;
        }

        /// <summary>
        /// Клонирование объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object result = null;

            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                result = bf.Deserialize(ms);
            }

            return result;
        }
    }
}
