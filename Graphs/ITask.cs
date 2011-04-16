namespace Graphs
{
    /// <summary>
    /// Интерфейс задачи
    /// </summary>
    /// <typeparam name="TVertexDataType">Тип дескриптора вершины</typeparam>
    /// <typeparam name="TEdgeDataType">Тип дескриптора ребра</typeparam>
    public interface ITask<TVertexDataType, TEdgeDataType>
    {
        void Set(SimpleStaticGraph<TVertexDataType, TEdgeDataType> graph);
        void Restart();
        void Result();
    }
}
