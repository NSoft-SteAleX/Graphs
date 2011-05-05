namespace GraphsRender.Graph
{
    /// <summary>
    /// Обобщённый интерфейс задачи
    /// </summary>
    /// <typeparam name="TVertexDataType">Тип дескриптора вершины</typeparam>
    /// <typeparam name="TEdgeDataType">Тип дескриптора ребра</typeparam>
    public interface ITask<TVertexDataType, TEdgeDataType>
    {
        void Set(SimpleStaticGraph<TVertexDataType, TEdgeDataType> graph);
        void Restart();
        void Result();
    }

    /// <summary>
    /// Конкретизированный интерфейс задачи
    /// </summary>
    public interface IConcreteTask
    {
        void Set(SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> graph);
        void Restart();
        SimpleStaticGraph<VertexDescriptor, EdgeDescriptor> Result();
    }
}
