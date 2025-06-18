using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public interface GridEntity
    {
        public Vector Position { get; set; }

        public bool IntersectsWith(GridEntity other)
        {
            return this.Position == other.Position;
        }
    }
}
