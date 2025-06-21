using Snake.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.ViewModels
{
    /// <summary>
    /// A view on a fruit.
    /// </summary>
    /// <param name="fruit">the fruit that this view model represents</param>
    /// <param name="gridSize">the size of the grid</param>
    public class FruitViewModel(Fruit fruit, int gridSize)
    {
        public Fruit Fruit { get; set; } = fruit;

        public int GridSize { get; set; } = gridSize;
        /// <summary>
        /// The position on the x axis scaled by the grid size.
        /// </summary>
        public int X  => (Fruit?.Position.X ?? 0) * GridSize - GridSize/4;

        /// <summary>
        /// The position on the y axis scaled by the grid size.
        /// </summary>
        public int Y => (Fruit?.Position.Y ?? 0) * GridSize - GridSize/4;


    }
}
