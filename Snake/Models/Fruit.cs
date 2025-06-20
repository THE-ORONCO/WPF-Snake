using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// A fruit that can be eaten by a snake.
    /// </summary>
    /// <param name="position">where the fruit is</param>
    public class Fruit(Vector position) : IGridEntity
    {
        public Vector Position { get; set; } = position;
    }
}
