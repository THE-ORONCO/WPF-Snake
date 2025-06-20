using System.ComponentModel;
using Snake.Models;

namespace Snake.ViewModels
{
    /// <summary>
    /// A Representation of a snake segment.
    /// </summary>
    public class SnakeSegmentViewModel : INotifyPropertyChanged
    {
        private SnakeSegment? snakeSegment;
        public SnakeSegment? Segment
        {
            get => snakeSegment; set
            {
                snakeSegment = value;
                // subscribe to changes in the position of the underlying model
                if (snakeSegment != null)
                {
                    snakeSegment.MovedInDirection += MovedInDirection;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// The position of the segment on the x axis scaled by the grid size.
        /// </summary>
        public int X => (snakeSegment?.position.X ?? 0) * GridSize;

        /// <summary>
        /// The position of the segment on the y axis scaled by the grid size.
        /// </summary>
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

        private void MovedInDirection(object? sender, Vector position) =>
            // emit a property changed signal if the position of the underlying segment changed
            PropertyChanged?.Invoke(this,
                                    position switch
                                    {
                                        (_, 0) => new PropertyChangedEventArgs(nameof(X)),
                                        (0, _) => new PropertyChangedEventArgs(nameof(Y)),
                                        _ => throw new Exception("somehow moved diagonally!!! very evil!"),
                                    });
    }
}
