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
        }

        /// <summary>
        /// Класс L-графа
        /// </summary>
        [Serializable]
        private class ListGraph : GeneralGraph
        {
            /// <summary>
            /// Массив списков смежных вершин
            /// </summary>
            private List<Vertex>[] vertexList;

            private List<KeyValuePair<Vertex, Edge>>[] adjacencyList;

            /// <summary>
            /// Список рёбер
            /// </summary>
            private List<Edge> edgeList;

            /// <summary>
            /// Массив вершин
            /// </summary>
            private Vertex[] vertexArray;

            public ListGraph(int vertexCount, int randomEdgesCount, GraphType direction)
            {
                this.VertexCount = vertexCount;
                this.Direction = direction;

                //Инициализация списков
                vertexList = new List<Vertex>[vertexCount];
                adjacencyList = new List<KeyValuePair<Vertex, Edge>>[vertexCount];
                vertexArray = new Vertex[vertexCount];
                edgeList = new List<Edge>();

                //Заполнение массива списков
                for (int i = 0; i != vertexCount; i++)
                {
                    vertexList[i] = new List<Vertex>();
                    vertexArray[i] = new Vertex(i);
                }

                //Вставка случайных рёбер
                InsertRandomEdges(randomEdgesCount);
            }
            public override bool InsertEdge(int v1, int v2)
            {
                if (!vertexList[v1].Exists(vertex => vertex.Number == v2))
                {
                    vertexList[v1].Add(new Vertex(v2));
                    edgeList.Add(new Edge(v1, v2));

                    if (Direction == GraphType.NotOriented)
                    {
                        vertexList[v2].Add(new Vertex(v1));
                        edgeList.Add(new Edge(v2, v1));
                    }

                    EdgeCount++;
                    return true;
                }
                
                return false;
            }
            public override bool DeleteEdge(int v1, int v2)
            {
                if (vertexList[v1].Exists(vertex => vertex.Number == v2))
                {
                    vertexList[v1].Remove(new Vertex(v2));
                    edgeList.Remove(new Edge(v1, v2));
                    //vertexList[v1].RemoveAt(vertexList[v1].FindIndex(vertex => vertex.Number == v2));

                    if (Direction == GraphType.NotOriented)
                    {
                        vertexList[v2].Remove(new Vertex(v1));
                        edgeList.Remove(new Edge(v2, v1));
                    }

                    EdgeCount--;
                    return true;
                }
                
                return false;
            }
            public override bool IsEdge(int v1, int v2)
            {
                if (vertexList[v1].Exists(vertex => vertex.Number == v2))
                {
                    return true;
                }

                return false;
            }
            public override EdgeDataType GetEdge(int v1, int v2)
            {
                Edge e = edgeList.Find(edge => edge.StartVertex == v1 && edge.EndVertex == v2);

                if(e != null) return e.Weight;
                else throw new NullReferenceException("Ребро, соединяющее точки " + v1 + " и " + v2 + " не существует!");
            }
            public override bool SetEdge(int v1, int v2, EdgeDataType w)
            {
                if (Direction == GraphType.NotOriented)
                {
                    List<Edge> edges = edgeList.FindAll(edge => (edge.StartVertex == v1 && edge.EndVertex == v2) || 
                        (edge.StartVertex == v2 && edge.EndVertex == v1));

                    if (edges.Count != 0)
                    {
                        edges.ForEach(edge => edge.Weight = w);
                        return true;
                    }
                }
                else
                {
                    Edge e = edgeList.Find(edge => edge.StartVertex == v1 && edge.EndVertex == v2);
                    if (e != null) e.Weight = w;
                }

                return false;
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
