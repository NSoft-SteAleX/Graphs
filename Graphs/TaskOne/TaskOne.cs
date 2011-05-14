using System.Data;
using GraphsRender.Graph;

namespace GraphsRender.TaskOne
{
    /// <summary>
    /// Класс решения задачи #1
    /// Вариант 6: определение вершин отделимости в неориентированном графе
    /// </summary>
    public class TaskOne
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _graph;

        /// <summary>
        /// Объект работника, решающего задачу
        /// </summary>
        private SeparationVertices<VertexDescriptor, EdgeDescriptor> _worker;

        /// <summary>
        /// Конструктор, связывает класс решения с графом
        /// </summary>
        /// <param name="graph">Объект графа</param>
        public TaskOne(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph)
        {
            if (graph.VertexCount() > 0)
            {
                if (graph.Direction() == GraphType.NotOriented)
                {
                    _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>)graph.Clone(); //Объект клонируется
                    _worker = new SeparationVertices<VertexDescriptor, EdgeDescriptor>(_graph);
                }
                else throw new DataException("Сгенерируйте неориентированный граф.");
            }
            else throw new DataException("Сгенерируйте граф с ненулевым числом вершин.");
        }

        /// <summary>
        /// Связывание класса решения с указанным графом
        /// </summary>
        /// <param name="graph"></param>
        public void Set(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph)
        {
            _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>)graph.Clone();
            _worker = new SeparationVertices<VertexDescriptor, EdgeDescriptor>(_graph);
        }

        /// <summary>
        /// Перезапуск решения задачи
        /// </summary>
        public void Restart()
        {
            _worker = new SeparationVertices<VertexDescriptor, EdgeDescriptor>(_graph);
        }

        /// <summary>
        /// Получение результата решения
        /// </summary>
        public SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> Result()
        {
            int i = 0;

            foreach (var answer in _worker.Answer)
            {
                if (answer)
                {
                    _graph.SetVertex(i, new VertexDescriptor(i.ToString(), Colors.Red));
                }

                i++;
            }

            return _graph;
        }
    }
}
