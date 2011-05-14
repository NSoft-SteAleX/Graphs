using System;

namespace GraphsRender.Graph
{
    /// <summary>
    /// Перечисление цветов
    /// </summary>
    public enum Colors { White, Blue, Yellow, Red };

    /// <summary>
    /// Дескриптор вершины
    /// </summary>
    [Serializable]
    public struct VertexDescriptor
    {
        public string Name;
        public Colors Color;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="n">Имя вершины</param>
        /// <param name="c">Цвет вершины (жёлтый по-умолчанию)</param>
        public VertexDescriptor(string n, Colors c = Colors.Yellow)
        {
            Name = n;
            Color = c;
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
