using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// Any segment of the snake that is not the head.
    /// </summary>
    public class Tail : SnakeSegment
    {
        public Tail(Vector position, SnakeSegment? next = null)
        {
            Position = position;
            Next = next;
        }
    }
}
