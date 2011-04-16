using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Graphs
{
    /// <summary>
    /// Перечисление форм графов
    /// </summary>
    public enum GraphFormat { MatrixGraph, ListGraph }

    /// <summary>
    /// Перечисление типов графов
    /// </summary>
    public enum GraphType { Oriented, NotOriented }

    /// <summary>
    /// Основной класс простого статического графа. Реализует M- и L-
    /// представления графа.
    /// 
    /// @author Александр Миронов
    /// @date 11.04.2011
    /// </summary>
    /// <typeparam name="TVertexDataType">Тип данных вершины</typeparam>
    /// <typeparam name="TEdgeDataType">Тип данных ребра</typeparam>
    [Serializable]
    public class SimpleStaticGraph<TVertexDataType, TEdgeDataType> : ICloneable
    {
        #region Nested Classes
        /// <summary>
        /// Класс вершины
        /// </summary>
        [Serializable]
        private class Vertex
        {
            public int Number { get; private set; }
            public TVertexDataType Data { get; set; }

            public Vertex(int number, TVertexDataType data = default(TVertexDataType))
            {
                Data = data;
                Number = number;
            }
            public override bool Equals(object obj)
            {
                if (obj is Vertex)
                {
                    var temp = (Vertex)obj;

                    if (temp.Number == Number && temp.Data.Equals(Data))
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
            public TEdgeDataType Data { get; set; }

            public Edge() { }
            public Edge(int startVertex, int endVertex, TEdgeDataType data = default(TEdgeDataType))
            {
                StartVertex = startVertex;
                EndVertex = endVertex;
                Data = data;
            }
            public override bool Equals(object obj)
            {
                if (obj is Edge)
                {
                    var temp = (Edge)obj;

                    if (temp.StartVertex == StartVertex && temp.EndVertex == EndVertex
                        && temp.Data.Equals(Data))
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
        /// Основной абстрактный класс графов,
        /// Служит базой для M- и L-форм
        /// </summary>
        [Serializable]
        private abstract class GeneralGraph
        {
            public int VertexCount { get; protected set; }
            public int EdgeCount { get; protected set; }
            public GraphType Direction { get; protected set; }

            //Абстрактные методы
            public abstract bool InsertEdge(int v1, int v2);
            public abstract bool DeleteEdge(int v1, int v2);
            public abstract bool IsEdge(int v1, int v2);
            public abstract TEdgeDataType GetEdge(int v1, int v2);
            public abstract bool SetEdge(int v1, int v2, TEdgeDataType data);
            public abstract TVertexDataType GetVertex(int v);
            public abstract bool SetVertex(int v, TVertexDataType data);

            /// <summary>
            /// Вставка случайных рёбер
            /// </summary>
            /// <param name="randomEdgesCount"></param>
            protected void InsertRandomEdges(int randomEdgesCount)
            {
                var rand = new Random(unchecked((int)DateTime.Now.Ticks));

                for (int i = 0; i != randomEdgesCount; i++)
                {
                    int v1 = rand.Next(0, VertexCount);
                    int v2 = rand.Next(0, VertexCount);

                    if(!InsertEdge(v1, v2))
                    {
                        i--;
                    }
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
                    V = v;
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
            private readonly Vertex[] vertexArray;

            /// <summary>
            /// Матрица смежностей
            /// </summary>
            private readonly Edge[,] matrix;

            public MatrixGraph(int vertexCount, int randomEdgesCount, GraphType direction)
            {
                VertexCount = vertexCount;
                Direction = direction;

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
                return matrix[v1, v2] != null;
            }

            public override TEdgeDataType GetEdge(int v1, int v2)
            {
                if (matrix[v1, v2] != null)
                {
                    return matrix[v1, v2].Data;
                }
                throw new NullReferenceException("Ребро, соединяющее точки " + v1 + " и " + v2 + " не существует!");
            }
            public override bool SetEdge(int v1, int v2, TEdgeDataType data)
            {
                if (matrix[v1, v2] != null)
                {
                    matrix[v1, v2].Data = data;

                    if (Direction == GraphType.NotOriented)
                    {
                        matrix[v2, v1].Data = data;
                    }

                    return true;
                }

                return false;
            }
            public override TVertexDataType GetVertex(int v)
            {
                if (v >= 0 && v < VertexCount)
                {
                    return vertexArray[v].Data;
                }
                throw new NullReferenceException("Вершина с номером " + v + " не существует!");
            }
            public override bool SetVertex(int v, TVertexDataType data)
            {
                if (v >= 0 && v < VertexCount)
                {
                    vertexArray[v].Data = data;
                    return true;
                }

                return false;
            }

            //Класс итератора
            public class Iterator : GeneralIterator 
            {
                private int I { get; set; }
                private readonly MatrixGraph Graph;

                public Iterator(GeneralGraph graph, int v) : base(v) 
                {
                    Graph = (MatrixGraph)graph;
                    I = -1;
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
                        if (Graph.matrix[V, I] != null) break;
                    }
                }
                public override bool Next()
                {
                    for (I++; I < Graph.VertexCount; I++)
                    {
                        if (Graph.matrix[V, I] != null) return true;
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
            /// Массив вершин
            /// </summary>
            private readonly Vertex[] vertexArray;
            
            /// <summary>
            /// Список смежностей
            /// </summary>
            private readonly List<KeyValuePair<int, Edge>>[] adjacencyList;

            public ListGraph(int vertexCount, int randomEdgesCount, GraphType direction)
            {
                VertexCount = vertexCount;
                Direction = direction;

                //Инициализация списков
                adjacencyList = new List<KeyValuePair<int, Edge>>[vertexCount];
                vertexArray = new Vertex[vertexCount];

                //Заполнение массива списков
                for (int i = 0; i != vertexCount; i++)
                {
                    adjacencyList[i] = new List<KeyValuePair<int, Edge>>();
                    vertexArray[i] = new Vertex(i);
                }

                //Вставка случайных рёбер
                InsertRandomEdges(randomEdgesCount);
            }
            public override bool InsertEdge(int v1, int v2)
            {
                if (!adjacencyList[v1].Exists(pair => pair.Key == v2))
                {
                    adjacencyList[v1].Add(new KeyValuePair<int, Edge>(v2, new Edge(v1, v2))); //Вставка пары

                    if (Direction == GraphType.NotOriented) //Если не орграф
                    {
                        adjacencyList[v2].Add(new KeyValuePair<int, Edge>(v1, new Edge(v2, v1)));
                    }

                    EdgeCount++;
                    return true;
                }
                
                return false;
            }
            public override bool DeleteEdge(int v1, int v2)
            {
                if (adjacencyList[v1].Exists(pair => pair.Key == v2))
                {
                    adjacencyList[v1].RemoveAt(adjacencyList[v1].FindIndex(pair => pair.Key == v2));

                    if (Direction == GraphType.NotOriented)
                    {
                        adjacencyList[v2].RemoveAt(adjacencyList[v2].FindIndex(pair => pair.Key == v1));
                    }

                    EdgeCount--;
                    return true;
                }
                
                return false;
            }
            public override bool IsEdge(int v1, int v2)
            {
                return adjacencyList[v1].Exists(pair => pair.Key == v2);
            }
            public override TEdgeDataType GetEdge(int v1, int v2)
            {
                Edge e = adjacencyList[v1].Find(pair => pair.Key == v2).Value;

                if(e != null) return e.Data;
                throw new NullReferenceException("Ребро, соединяющее точки " + v1 + " и " + v2 + " не существует!");
            }
            public override bool SetEdge(int v1, int v2, TEdgeDataType data)
            {
                if (Direction == GraphType.NotOriented)
                {
                    Edge e1 = adjacencyList[v1].Find(pair => pair.Key == v2).Value;
                    Edge e2 = adjacencyList[v2].Find(pair => pair.Key == v1).Value;

                    if (e1 != null && e2 != null)
                    {
                        e1.Data = e2.Data = data;
                        return true;
                    }
                }
                else
                {
                    Edge e = adjacencyList[v1].Find(pair => pair.Key == v2).Value;
                    if (e != null)
                    {
                        e.Data = data;
                    }
                }

                return false;
            }
            public override TVertexDataType GetVertex(int v)
            {
                if (v >= 0 && v < VertexCount)
                {
                    return vertexArray[v].Data;
                }
                throw new NullReferenceException("Вершина с номером " + v + " не существует!");
            }
            public override bool SetVertex(int v, TVertexDataType data)
            {
                if (v >= 0 && v < VertexCount)
                {
                    vertexArray[v].Data = data;
                }

                return false;
            }

            //Класс итератора
            public class Iterator : GeneralIterator
            {
                private readonly ListGraph _graph;
                private List<KeyValuePair<int, Edge>>.Enumerator _listIter;

                public Iterator(GeneralGraph graph, int v) : base(v)
                {
                    _graph = (ListGraph)graph;
                    _listIter = _graph.adjacencyList[V].GetEnumerator();
                    _listIter.MoveNext();
                }
                public override void Begin()
                {
                    _listIter = _graph.adjacencyList[V].GetEnumerator();
                    _listIter.MoveNext();
                }
                public override void End()
                {
                    _listIter = _graph.adjacencyList[V].GetEnumerator();
                    for (int i = 0; i != _graph.adjacencyList[V].Count; i++)
                    {
                        _listIter.MoveNext();
                    }
                }
                public override bool Next()
                {
                    return _listIter.MoveNext();
                }
                public override bool IsAlive()
                {
                    return _listIter.Current.Value != null;
                }
                public override int Current()
                {
                    return IsAlive() ? _listIter.Current.Key : -1;
                }
            }
        }

        /// <summary>
        /// Итератор
        /// </summary>
        public class Iterator
        {
            private readonly SimpleStaticGraph<TVertexDataType, TEdgeDataType> _simpleGraph;
            private readonly GeneralGraph.GeneralIterator _iter;

            /// <summary>
            /// Конструктор итератора
            /// </summary>
            /// <param name="simpleGraph">Объект простого статического графа</param>
            /// <param name="v">Номер вершины</param>
            public Iterator(SimpleStaticGraph<TVertexDataType, TEdgeDataType> simpleGraph, int v)
            {
                _simpleGraph = simpleGraph;

                if (_simpleGraph.Dense() == GraphFormat.MatrixGraph)
                {
                    _iter = new MatrixGraph.Iterator(_simpleGraph._graph, v);
                }
                else
                {
                    _iter = new ListGraph.Iterator(_simpleGraph._graph, v);
                }
            }

            /// <summary>
            /// Устанавливает итератор на первую смежную вершину
            /// </summary>
            /// <returns></returns>
            public void Begin()
            {
                _iter.Begin();
            }

            /// <summary>
            /// Устанавливает итератор на последнюю смежную вершину
            /// </summary>
            public void End()
            {
                _iter.End();
            }

            /// <summary>
            /// Переход к следующей смежной вершине
            /// </summary>
            /// <returns></returns>
            public bool Next()
            {
                return _iter.Next();
            }

            /// <summary>
            /// Получение номера текущей смежной вершины
            /// </summary>
            /// <returns></returns>
            public int Current()
            {
                return _iter.Current();
            }
        }
        #endregion

        /// <summary>
        /// Базовый объект графа
        /// </summary>
        private GeneralGraph _graph;

        /// <summary>
        /// Конструктор по-умолчанию
        /// </summary>
        public SimpleStaticGraph()
        {
            _graph = new ListGraph(0, 0, GraphType.NotOriented);
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
                _graph = new MatrixGraph(vertexCount, randomEdgesCount, direction);
            }
            else
            {
                _graph = new ListGraph(vertexCount, randomEdgesCount, direction);
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
            return _graph.InsertEdge(v1, v2);
        }

        /// <summary>
        /// Удаление ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool DeleteEdge(int v1, int v2)
        {
            return _graph.DeleteEdge(v1, v2);
        }
        
        /// <summary>
        /// Проверка существования ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public bool IsEdge(int v1, int v2)
        {
            return _graph.IsEdge(v1, v2);
        }

        /// <summary>
        /// Получение веса ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <returns></returns>
        public TEdgeDataType GetEdge(int v1, int v2)
        {
            return _graph.GetEdge(v1, v2);
        }

        /// <summary>
        /// Установка данных ребра
        /// </summary>
        /// <param name="v1">Начальная вершина</param>
        /// <param name="v2">Конечная вершина</param>
        /// <param name="data">Данные вершины</param>
        /// <returns></returns>
        public bool SetEdge(int v1, int v2, TEdgeDataType data)
        {
            return _graph.SetEdge(v1, v2, data);
        }

        /// <summary>
        /// Получение данных вершины
        /// </summary>
        /// <param name="v">Номер вершины</param>
        /// <returns></returns>
        public TVertexDataType GetVertex(int v)
        {
            return _graph.GetVertex(v);
        }

        /// <summary>
        /// Установка данных вершины
        /// </summary>
        /// <param name="v">Номер вершины</param>
        /// <param name="data">Данные вершины</param>
        /// <returns></returns>
        public bool SetVertex(int v, TVertexDataType data)
        {
            return _graph.SetVertex(v, data);
        }

        /// <summary>
        /// Получение числа вершин графа
        /// </summary>
        /// <returns></returns>
        public int VertexCount()
        {
            return _graph.VertexCount;
        }

        /// <summary>
        /// Получение числа рёбер графа
        /// </summary>
        /// <returns></returns>
        public int EdgeCount()
        {
            return _graph.EdgeCount;
        }

        /// <summary>
        /// Получение формата графа (M- || L-)
        /// </summary>
        /// <returns></returns>
        public GraphFormat Dense()
        {
            return _graph is MatrixGraph ? GraphFormat.MatrixGraph : GraphFormat.ListGraph;
        }

        /// <summary>
        /// Получение типа графа
        /// </summary>
        /// <returns></returns>
        public GraphType Direction()
        {
            return _graph.Direction;
        }

        /// <summary>
        /// Конвертация типов графа
        /// </summary>
        /// <returns></returns>
        public void Convert()
        {
            if (Dense() == GraphFormat.MatrixGraph)
            {
                var g = new ListGraph(_graph.VertexCount, 0, _graph.Direction);

                for (int i = 0; i != _graph.VertexCount; i++)
                {
                    var iter = new MatrixGraph.Iterator(_graph, i);
                    iter.Begin();

                    while (iter.Current() != -1)
                    {
                        g.InsertEdge(i, iter.Current());
                        g.SetEdge(i, iter.Current(), _graph.GetEdge(i, iter.Current()));
                        iter.Next();
                    }
                }

                _graph = g;
            }
            else
            {
                var g = new MatrixGraph(_graph.VertexCount, 0, _graph.Direction);

                for (int i = 0; i != _graph.VertexCount; i++)
                {
                    var iter = new ListGraph.Iterator(_graph, i);
                    iter.Begin();

                    while (iter.Current() != -1)
                    {
                        g.InsertEdge(i, iter.Current());
                        g.SetEdge(i, iter.Current(), _graph.GetEdge(i, iter.Current()));
                        iter.Next();
                    }
                }

                _graph = g;
            }
        }

        /// <summary>
        /// Клонирование объекта
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object result;

            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, this);
                ms.Position = 0;
                result = bf.Deserialize(ms);
            }

            return result;
        }
    }
}