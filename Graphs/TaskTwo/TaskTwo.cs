using System.Data;
using GraphsRender.Graph;

namespace GraphsRender.TaskTwo
{
    /// <summary>
    /// Класс решения задачи #2
    /// Вариант 3: разбиение неориентированного графа на кластеры, 
    /// объединяющие вершины ребрами с длиной, большей d. 
    /// Для разбиения использовать алгоритм Крускалла.
    /// </summary>
    public class TaskTwo
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _graph;

        /// <summary>
        /// Минимальная длина рёбер, объединяющих вершины в кластеры
        /// </summary>
        private double _minLength;

        /// <summary>
        /// Объект работника, решающего задачу
        /// </summary>
        private CruskallAlgorithm _worker;

        /// <summary>
        /// Конструктор, связывает класс решения с графом
        /// </summary>
        /// <param name="graph">Объект графа</param>
        /// <param name="minLength">Минимальная длина рёбер</param>
        public TaskTwo(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph, double minLength)
        {
            _minLength = minLength;

            if (graph.VertexCount() > 0)
            {
                if (graph.Direction() == GraphType.NotOriented)
                {
                    _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>) graph.Clone(); //Объект клонируется
                    _worker = new CruskallAlgorithm(_graph, minLength);
                }
                else throw new DataException("Сгенерируйте неориентированный граф.");
            }
            else throw new DataException("Сгенерируйте граф с ненулевым числом вершин.");
        }

        /// <summary>
        /// Связывание класса решения с указанным графом
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="minLength"></param>
        public void Set(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph, double minLength)
        {
            _minLength = minLength;
            _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>)graph.Clone();
            _worker = new CruskallAlgorithm(_graph, minLength);
        }

        /// <summary>
        /// Перезапуск решения задачи
        /// </summary>
        public void Restart()
        {
            _worker = new CruskallAlgorithm(_graph, _minLength);
        }

        /// <summary>
        /// Вывод результатов решения
        /// </summary>
        public SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> Result()
        {
            return _worker.Result();
        }
    }
}
