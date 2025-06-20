using Snake.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.ViewModels
{
    public class FruitViewModel
    {
        public Fruit Fruit {  get; set; }

        public int X  => (Fruit?.Position.X ?? 0) * GridSize;

        public int Y => (Fruit?.Position.Y ?? 0) * GridSize;
          
        public int GridSize { get; set; }

        public FruitViewModel(Fruit fruit, int gridSize)
        {
            Fruit = fruit;
            GridSize = gridSize;
        }
    }
}
