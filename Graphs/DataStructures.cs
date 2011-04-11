using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graphs
{
    /// <summary>
    /// Дескриптор вершины
    /// </summary>
    struct VertexDescriptor
    {
        /*
        public enum Colors { Black, White, None }
        public Colors color;
        */
        public string name;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="n">Имя вершины</param>
        public VertexDescriptor(string n)
        {
            name = n;
        }
    }
    
    /// <summary>
    /// Дескриптор ребра
    /// </summary>
    struct EdgeDescriptor
    {
        public double weight;

        /// <summary>
        /// Конструктор структуры
        /// </summary>
        /// <param name="w">Вес ребра</param>
        public EdgeDescriptor(double w)
        {
            weight = w;
        }
    }
}
