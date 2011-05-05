using System;

namespace GraphsRender.Graph
{
    /// <summary>
    /// Перечисление цветов для окраски рёбер
    /// </summary>
    public enum Colors { White, Blue };

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
        public Colors Color;

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="w">Вес ребра</param>
        /// <param name="c">Цвет ребра (белый по-умолчанию)</param>
        public EdgeDescriptor(double w, Colors c = default(Colors))
        {
            Weight = w;
            Color = c;
        }
    }
}
