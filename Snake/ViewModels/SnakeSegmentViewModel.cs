using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;

namespace Snake.ViewModels
{
    public class SnakeSegmentViewModel
    {
        private SnakeSegment _snakeSegment;

        public SnakeSegmentViewModel(SnakeSegment snakeSegment)
        {
            _snakeSegment = snakeSegment;
        }
    }
}
