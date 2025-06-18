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
        private readonly Fruit _fruit;

        public int X { get => _fruit.Position.X; }

        public int Y { get => _fruit.Position.Y; }

        public FruitViewModel(Fruit fruit)
        {
            _fruit = fruit;
        }

        public FruitViewModel()
        {
        }
    }
}
