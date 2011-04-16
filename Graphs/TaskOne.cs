using System.Windows.Forms;

namespace Graphs
{
    /// <summary>
    /// Класс решения задачи #1
    /// Вариант 6: определение вершин отделимости в неориентированном графе
    /// </summary>
    /// <typeparam name="TVertexDataType">Тип дескриптора вершин графа</typeparam>
    /// <typeparam name="TEdgeDataType">Тип дескриптора рёбер графа</typeparam>
    public class TaskOne<TVertexDataType, TEdgeDataType> : ITask<TVertexDataType, TEdgeDataType>
    {
        /// <summary>
        /// Объект графа
        /// </summary>
        private SimpleStaticGraph<TVertexDataType, TEdgeDataType> _graph;

        /// <summary>
        /// Объект работника, решающего задачу
        /// </summary>
        private SeparationVertices<TVertexDataType, TEdgeDataType> _worker;

        /// <summary>
        /// Конструктор, связывает класс решения с графом
        /// </summary>
        /// <param name="graph">Объект графа</param>
        public TaskOne(SimpleStaticGraph<TVertexDataType, TEdgeDataType> graph)
        {
            _graph = (SimpleStaticGraph<TVertexDataType, TEdgeDataType>)graph.Clone(); //Объект клонируется
            _worker = new SeparationVertices<TVertexDataType, TEdgeDataType>(_graph);
        }

        /// <summary>
        /// Связывание класса решения с указанным графом
        /// </summary>
        /// <param name="graph"></param>
        public void Set(SimpleStaticGraph<TVertexDataType, TEdgeDataType> graph)
        {
            _graph = (SimpleStaticGraph<TVertexDataType, TEdgeDataType>)graph.Clone();
            _worker = new SeparationVertices<TVertexDataType, TEdgeDataType>(_graph);
        }

        /// <summary>
        /// Перезапуск решения задачи
        /// </summary>
        public void Restart()
        {
            _worker = new SeparationVertices<TVertexDataType, TEdgeDataType>(_graph);
        }

        /// <summary>
        /// Вывод результатов решения
        /// </summary>
        public void Result()
        {
            if (_graph.VertexCount() > 0)
            {
                if (_graph.Direction() == GraphType.NotOriented)
                {
                    string result = "";
                    int i = 0;

                    foreach (var answer in _worker.Answer)
                    {
                        if (answer)
                        {
                            result += i + ": TRUE\n";
                        }
                        else
                        {
                            result += i + ": FALSE\n";
                        }

                        i++;
                    }

                    MessageBox.Show(result, "Результат задачи #1");
                }
                else
                {
                    MainForm.ShowWarning("Сгенерируйте неориентированный граф.");
                }
            }
            else
            {
                MainForm.ShowWarning("Сгенерируйте граф с ненулевым числом вершин.");
            }
        }
    }
}
