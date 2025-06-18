using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Snake.Models;
using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace Snake.ViewModels
{

    public class PlayFieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly PlayField _playField;

        public ObservableCollection<SnakeSegmentViewModel> Snake { get; set; }
        public ObservableCollection<FruitViewModel> Fruits { get; set; }

        public ICommand MoveRight { get; set; }

        public ICommand MoveLeft { get; set; }

        public ICommand MoveUp { get; set; }

        public ICommand MoveDown { get; set; }

        public PlayFieldViewModel()
        {
            _playField = new PlayField(new Head(position: new(64, 64), direction: Vector.RIGHT, next: null), []);
            _playField.SegmentAdded += SegmentAdded;
            Snake = [new SnakeSegmentViewModel(_playField.SnakeHead)];
            Fruits = [];

            //playField.SnakeMoved += OnPropertyChanged;
            MoveRight = new DirectionalCommand(Direction.RIGHT, _playField.SetDirection, _playField.CanGoDirection);
            MoveLeft = new DirectionalCommand(Direction.LEFT, _playField.SetDirection, _playField.CanGoDirection);
            MoveUp = new DirectionalCommand(Direction.UP, _playField.SetDirection, _playField.CanGoDirection);
            MoveDown = new DirectionalCommand(Direction.DOWN, _playField.SetDirection, _playField.CanGoDirection);
        }

        private void SegmentAdded(object? sender, SnakeSegment snakeSegment)
        {
            Snake.Add(new SnakeSegmentViewModel(snakeSegment));
        }
    }
}
