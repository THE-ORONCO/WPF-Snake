using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// The head of the snake.
    /// </summary>
    public class Head : SnakeSegment
    {
        /// <summary>
        /// The direction that the snakes is currently moving in.
        /// </summary>
        public Vector Direction { get; set; }

        /// <summary>
        /// The next position the snake wants to move to.
        /// </summary>
        public Vector NextPosition { get => this.Position + this.Direction; }

        public Head(Vector position, Vector direction, SnakeSegment? next = null)
        {
            Direction = direction;
            Next = next;
            Position = position;
        }
    }
}
