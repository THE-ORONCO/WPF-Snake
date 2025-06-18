using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;

namespace Snake.ViewModels
{
    public class FruitViewModel
    {
        private readonly Fruit _fruit;

        public FruitViewModel(Fruit fruit)
        {
            _fruit = fruit;
        }
    }
}
