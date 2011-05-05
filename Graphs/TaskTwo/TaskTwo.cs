using GraphsRender.Graph;

namespace GraphsRender.TaskTwo
{
    /// <summary>
    /// Класс решения задачи #2
    /// Вариант 3: 
    /// </summary>
    public class TaskTwo : IConcreteTask
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> _graph;

        /// <summary>
        /// Объект работника, решающего задачу
        /// </summary>
        private CruskallAlgorithm _worker;

        /// <summary>
        /// Конструктор, связывает класс решения с графом
        /// </summary>
        /// <param name="graph">Объект графа</param>
        public TaskTwo(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph)
        {
            _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>)graph.Clone(); //Объект клонируется
            _worker = new CruskallAlgorithm(_graph);
        }

        /// <summary>
        /// Связывание класса решения с указанным графом
        /// </summary>
        /// <param name="graph"></param>
        public void Set(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph)
        {
            _graph = (SimpleStaticGraph<VertexDescriptor, EdgeDescriptor>)graph.Clone();
            _worker = new CruskallAlgorithm(_graph);
        }

        /// <summary>
        /// Перезапуск решения задачи
        /// </summary>
        public void Restart()
        {
            _worker = new CruskallAlgorithm(_graph);
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
