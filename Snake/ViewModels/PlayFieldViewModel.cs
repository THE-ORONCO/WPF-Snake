using System.ComponentModel;
using System.Windows.Input;
using Snake.Models;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows;

namespace Snake.ViewModels
{

    /// <summary>
    /// The representation of the play field.
    /// </summary>
    public class PlayFieldViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly PlayField playField;

        public int GridSize { get; set; } = 20;
        public int Width => playField.Width * GridSize;
        public int Heigt => playField.Height * GridSize;

        public uint Score => playField.Score;
        private uint highScore { get; set; }
        public String HighScore => highScore.ToString();

        public Visibility ShowStartButton => playField.Running ? Visibility.Collapsed : Visibility.Visible;

        /// <summary>
        /// A lock object that is used to synchronize updates to the Snake.
        /// </summary>
        private static object snake_lock = new();
        public ObservableCollection<SnakeSegmentViewModel> Snake { get; set; } = [];

        /// <summary>
        /// A lock object that is used to synchronize updates to the Fruits.
        /// </summary>
        private static object fruit_lock = new();
        public ObservableCollection<FruitViewModel> Fruits { get; set; } = [];

        #region DIRECTIONAL COMMANDS
        public DirectionalCommand MoveRight { get; set; }
        public DirectionalCommand MoveLeft { get; set; }
        public DirectionalCommand MoveUp { get; set; }
        public DirectionalCommand MoveDown { get; set; }
        #endregion

        public StartGameCommand StartGame { get; set; }

        public PlayFieldViewModel()
        {
            // allow updates to the Observable collections from other threads than the UI thread
            BindingOperations.EnableCollectionSynchronization(Snake, snake_lock);
            BindingOperations.EnableCollectionSynchronization(Fruits, fruit_lock);

            playField = new PlayField();
            playField.SegmentAdded += SegmentAdded;
            playField.FruitAdded += FruitAdded;
            playField.FruitEaten += FruitEaten;
            playField.Started += (_, _) => PropertyChanged?.Invoke(this, new(nameof(ShowStartButton)));
            playField.Finished += ResetGame;
            playField.ScoreUpdated += (_, _) => PropertyChanged?.Invoke(this, new(nameof(Score)));
            playField.NewGame();


            MoveRight = new DirectionalCommand(Direction.RIGHT, playField.SetDirection, playField.CanGoDirection);
            MoveLeft = new DirectionalCommand(Direction.LEFT, playField.SetDirection, playField.CanGoDirection);
            MoveUp = new DirectionalCommand(Direction.UP, playField.SetDirection, playField.CanGoDirection);
            MoveDown = new DirectionalCommand(Direction.DOWN, playField.SetDirection, playField.CanGoDirection);

            StartGame = new StartGameCommand(playField);
        }

        private void SegmentAdded(object? sender, SnakeSegment snakeSegment) => Snake.Add(new SnakeSegmentViewModel { Segment = snakeSegment, GridSize = GridSize });

        private void FruitAdded(object? sender, Fruit fruit) => Fruits.Add(new FruitViewModel(fruit, GridSize));

        private void FruitEaten(object? sender, Fruit fruit) => Fruits.Where(f => f.Fruit == fruit).ToList().ForEach(f => Fruits.Remove(f));

        private void ResetGame(object? sender, uint score)
        {
            // reset the game
            Fruits.Clear();
            Snake.Clear();
            playField.NewGame();

            // remember the best score
            if (score > highScore)
            {
                highScore = score;
                PropertyChanged?.Invoke(this, new(nameof(HighScore)));
            }

            // show the start button
            PropertyChanged?.Invoke(this, new(nameof(ShowStartButton)));
        }
    }
}
