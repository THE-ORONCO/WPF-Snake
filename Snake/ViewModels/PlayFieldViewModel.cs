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
using System.Windows.Data;

namespace Snake.ViewModels
{

    public class PlayFieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly PlayField _playField;

        public int GridSize { get; set; } = 20;
        public int Width => _playField.Width * GridSize;
        public int Heigt => _playField.Height * GridSize;

        public uint Score => _playField.Score;

        private static object _snake_lock = new();
        public ObservableCollection<SnakeSegmentViewModel> Snake { get; set; } = [];

        private static object _fruit_lock = new ();
        public ObservableCollection<FruitViewModel> Fruits { get; set; } = [];
        public ICommand MoveRight { get; set; }

        public ICommand MoveLeft { get; set; }

        public ICommand MoveUp { get; set; }

        public ICommand MoveDown { get; set; }

        public PlayFieldViewModel()
        {
            Tail tail = new(position: new(1, 1), next: null);
            Head head = new(position: new(1, 1), direction: Vector.RIGHT, next: tail);

            BindingOperations.EnableCollectionSynchronization(Snake, _snake_lock);
            BindingOperations.EnableCollectionSynchronization(Fruits, _fruit_lock);

            _playField = new PlayField(null, []);
            _playField.SegmentAdded += SegmentAdded;
            _playField.FruitAdded += FruitAdded;
            _playField.FruitEaten += FruitEaten;
            _playField.ScoreUpdated += (_, _) => PropertyChanged?.Invoke(this, new(nameof(Score)));
            _playField.AddSegments(1);

            //playField.SnakeMoved += OnPropertyChanged;
            MoveRight = new DirectionalCommand(Direction.RIGHT, _playField.SetDirection, _playField.CanGoDirection);
            MoveLeft = new DirectionalCommand(Direction.LEFT, _playField.SetDirection, _playField.CanGoDirection);
            MoveUp = new DirectionalCommand(Direction.UP, _playField.SetDirection, _playField.CanGoDirection);
            MoveDown = new DirectionalCommand(Direction.DOWN, _playField.SetDirection, _playField.CanGoDirection);
        }

        private void SegmentAdded(object? sender, SnakeSegment snakeSegment) => Snake.Add(new SnakeSegmentViewModel { Segment = snakeSegment, GridSize = GridSize });

        private void FruitAdded(object? sender, Fruit fruit) => Fruits.Add(new FruitViewModel(fruit, GridSize));

        private void FruitEaten(object? sender, Fruit fruit) => Fruits.Where(f => f.Fruit == fruit).ToList().ForEach(f => Fruits.Remove(f));
    }
}
