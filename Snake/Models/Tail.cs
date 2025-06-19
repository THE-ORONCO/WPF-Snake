using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Tail : SnakeSegment
    {
        public Tail(Vector position, SnakeSegment? next = null)
        {
            Position = position;
            Next = next;
        }


        public void AddSegment(SnakeSegment segment)
        {
            throw new NotImplementedException();
        }
    }
}
