using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Head : SnakeSegment
    {
        public Vector Direction { get; set; }

        public Vector NextPosition { get => this.Position + this.Direction; }

        public Head(Vector position, Vector direction, SnakeSegment? next = null)
        {
            Direction = direction;
            Next = next;
            Position = position;
        }
    }
}
