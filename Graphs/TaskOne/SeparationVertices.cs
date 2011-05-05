using System;
using GraphsRender.Graph;

namespace GraphsRender.TaskOne
{
    /// <summary>
    /// Обобщённый класс поиска вершин отделимости (сочленения)
    /// </summary>
    /// <typeparam name="TVertexDataType">Тип дескриптора вершин графа</typeparam>
    /// <typeparam name="TEdgeDataType">Тип дескриптора рёбер графа</typeparam>
    class SeparationVertices<TVertexDataType, TEdgeDataType>
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private readonly SimpleStaticGraph<TVertexDataType, TEdgeDataType> _graph;

        /// <summary>
        /// Массив использованных вершин
        /// </summary>
        private readonly bool[] _used;

        /// <summary>
        /// Массив сохранения "времён входа" в вершины
        /// </summary>
        private readonly int[] _tin;

        /// <summary>
        /// Массив минимумов из времени заходов в вершину
        /// </summary>
        private readonly int[] _up;

        /// <summary>
        /// Счётчик "времени"
        /// </summary>
        private int _time;

        /// <summary>
        /// Массив булевых значений isCutVertex\isn'tCutVertex
        /// </summary>
        public bool[] Answer { get; set; }

        /// <summary>
        /// Счётчик вершин отделимости
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="graph">Объект графа</param>
        public SeparationVertices(SimpleStaticGraph<TVertexDataType, TEdgeDataType> graph)
        {
            var vertexCount = graph.VertexCount();
            _graph = graph;
            _used = new bool[vertexCount];
            _tin = new int[vertexCount];
            _up = new int[vertexCount];
            Answer = new bool[vertexCount];
            _time = Count = 0;

            //Хак для несвязного графа
            for (int i = 0; i < vertexCount; i++)
            {
                if (_used[i]) continue;
                _time = Count = 0;
                FindSeparaionVertices(i, -1);
            }
        }

        /// <summary>
        /// Рекурсивный метод поиска вершин
        /// отделимости. Использует алгоритм обхода графа в
        /// глубину
        /// </summary>
        /// <param name="currentVertex">Номер вершины</param>
        /// <param name="previousVertex">Предыдущая обойдённая вершина</param>
        private void FindSeparaionVertices(int currentVertex, int previousVertex)
        {
            //Создаём итератор для обхода смежных вершин
            var iter = new SimpleStaticGraph<TVertexDataType, TEdgeDataType>.Iterator(_graph, currentVertex);
            _used[currentVertex] = true;

            //счетчик количества детей вершины u в дереве обхода
            var count = 0;

            //задание времени входа tin и начального значения up для вершины u
            _tin[currentVertex] = _up[currentVertex] = _time++;

            //Установка итератора на начало
            iter.Begin();

            //Обход графа в глубину
            for (int v = iter.Current(); v != -1; iter.Next(), v = iter.Current())
            {
                if(v == previousVertex)
                {
                    continue;
                }

                if (_used[v]) //v - предок вершины currentVertex
                {
                    _up[currentVertex] = Math.Min(_up[currentVertex], _tin[v]);
                }
                else //v - ребенок вершины currentVertex
                {
                    ++count;
                    FindSeparaionVertices(v, currentVertex);

                    _up[currentVertex] = Math.Min(_up[currentVertex], _up[v]);

                    //не существует обратного ребра из вершины v или ее потомка 
                    //в предка вершины currentVertex: currentVertex - точка сочленения
                    if(_up[v] >= _tin[currentVertex])
                    {
                        Answer[currentVertex] = true;
                        Count++;
                    }
                }
            }

            //является ли currentVertex корнем дерева обхода
            if(previousVertex == -1)
            {
                if (count > 1) //проверка количества детей у корня дерева
                {
                    Answer[currentVertex] = true;
                    Count++;
                }
                else
                {
                    Answer[currentVertex] = false;
                    Count--;
                }
            }
        }
    }
}
