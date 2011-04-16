using System;

namespace Graphs
{
    /// <summary>
    /// Дескриптор вершины
    /// </summary>
    [Serializable]
    public struct VertexDescriptor
    {
        /// <summary>
        /// Имя вершины
        /// </summary>
        public string Name;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="n">Имя вершины</param>
        public VertexDescriptor(string n)
        {
            Name = n;
        }
    }
    
    /// <summary>
    /// Дескриптор ребра
    /// </summary>
    [Serializable]
    public struct EdgeDescriptor
    {
        public double Weight;

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="w">Вес ребра</param>
        public EdgeDescriptor(double w)
        {
            Weight = w;
        }
    }
}
