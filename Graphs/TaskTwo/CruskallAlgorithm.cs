using System.Collections.Generic;
using GraphsRender.Graph;

namespace GraphsRender.TaskTwo
{
    /// <summary>
    /// Класс, реализующий алгоритм Крускалла
    /// для разбиения на кластеры
    /// </summary>
    public class CruskallAlgorithm
    {
        private readonly double _minLength;
        private readonly SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _graph;
        private readonly List<SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Edge> _edgeList;
        private readonly List<SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Edge> _newEdgeList;
        private readonly UnionFind _unions;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="minLength"></param>
        public CruskallAlgorithm(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph, double minLength)
        {
            _minLength = minLength;
            _graph = graph;
            _unions = new UnionFind(_graph.VertexCount());
            _newEdgeList = new List<SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>.Edge>(_graph.VertexCount());
            int v = _graph.VertexCount();
            int e = _graph.EdgeCount();

            //Получение и сортировка списка рёбер
            _edgeList = _graph.GetEdges();
            _edgeList.Sort((a, b) => a.Data.Weight.CompareTo(b.Data.Weight));

            for (int i = 0, k = 1; i < e * 2 && k < v; i++)
            {
                if(!_unions.Find(_edgeList[i].StartVertex, _edgeList[i].EndVertex) && _edgeList[i].Data.Weight >= _minLength)
                {
                    _unions.Unite(_edgeList[i].StartVertex, _edgeList[i].EndVertex);
                    _newEdgeList.Add(_edgeList[i]);
                    k++;
                }
            }

            //Перекраска рёбер
            foreach (var edge in _newEdgeList)
            {
                _graph.SetEdge(edge.StartVertex, edge.EndVertex, new EdgeDescriptor(edge.Data.Weight, Colors.Blue));
                _graph.SetEdge(edge.EndVertex, edge.StartVertex, new EdgeDescriptor(edge.Data.Weight, Colors.Blue));
            }
        }

        /// <summary>
        /// Получение нового графа
        /// </summary>
        /// <returns></returns>
        public SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> Result()
        {
            return _graph;
        }
    }
}
