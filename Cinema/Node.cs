using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema
{
    public class Node
    {
        /// <summary>
        /// Значение вершины.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Пусть от корня до текущей ноды
        /// </summary>
        public List<Film> pathToNode;

        /// <summary>
        /// Список дочерних вершин.
        /// </summary>
        public List<Node> Children { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="value">Значние вершины (node) - остаток</param>
        /// <param name="path">путь от корня до это вершины</param>
        public Node(int value, List<Film> path)
        {
            Value = value;
            pathToNode = path;
            Children = new List<Node>();
        }

        /// Добавляет новую соседнюю вершину.
        public void AddChildren(Node node)
        {
            Children.Add(node);
        }
    }
}
