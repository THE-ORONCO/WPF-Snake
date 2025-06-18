using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;

namespace Snake.ViewModels
{
    public class SnakeSegmentViewModel : INotifyPropertyChanged
    {
        private SnakeSegment? snakeSegment;
        public SnakeSegment? Segment
        {
            get => snakeSegment; set
            {
                snakeSegment = value;
                if (snakeSegment != null)
                {
                    snakeSegment.MovedInDirection += MovedInDirection;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public SnakeSegmentViewModel Self { get => this; }
        public int X => (snakeSegment?.position.X ?? 0) * GridSize;
        public int Y => (snakeSegment?.position.Y ?? 0) * GridSize;

        private int gridSize { get; set; }
        public int GridSize
        {
            get => gridSize; set
            {
                gridSize = value;
                PropertyChanged?.Invoke(this, new(nameof(GridSize)));
            }
        }

        public SnakeSegmentViewModel()
        {

        }

        private void MovedInDirection(object? sender, Vector position)
        {
            PropertyChangedEventArgs r = position switch
            {
                (_, 0) => new PropertyChangedEventArgs(nameof(X)),
                (0, _) => new PropertyChangedEventArgs(nameof(Y)),
                _ => throw new Exception("somehow moved diagonally!!! very evil!"),
            };
            PropertyChanged?.Invoke(this, r);
        }
    }
}
