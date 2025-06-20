using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public class Fruit : GridEntity
    {
        public Vector Position { get ; set; }

        public Fruit(Vector position)
        {
            Position = position;
        }
    }
}
