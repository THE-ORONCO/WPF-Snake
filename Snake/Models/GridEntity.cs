using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// Something that can be placed on a grid
    /// </summary>
    public interface IGridEntity
    {
        /// <summary>
        /// The position on the grid.
        /// </summary>
        public Vector Position { get; set; }

        /// <summary>
        /// Check if this entity intersects with another entity
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IntersectsWith(IGridEntity other) => Position == other.Position;
    }
}
