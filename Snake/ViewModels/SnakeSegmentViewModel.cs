using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snake.Models;

namespace Snake.ViewModels
{
    public class SnakeSegmentViewModel: INotifyPropertyChanged
    {
        private SnakeSegment _snakeSegment;

        public event PropertyChangedEventHandler? PropertyChanged;

        private int x {  get; set; }
        public int X
        {
            get => x; set
            {
                x = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }
        private int y { get; set; }
        public int Y
        {
            get => y; set
            {
                y = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }



        public SnakeSegmentViewModel(SnakeSegment snakeSegment)
        {
            _snakeSegment = snakeSegment;
            _snakeSegment.PositionChanged += PositionChanged;
        }

        public SnakeSegmentViewModel()
        {
        }

        private void PositionChanged(object? sender, Vector position) => (this.X, this.Y) = position;
    }
}
