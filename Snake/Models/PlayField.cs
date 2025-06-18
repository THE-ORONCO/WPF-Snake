using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;
using Snake.ViewModels;

namespace Snake.Models
{

    public enum Result
    {
        ATE_FRUIT,
        COLLIDED,
        MOVED,
    }

    public class PlayField
    {
        public event EventHandler<List<Fruit>>? AteFruits;
        public event EventHandler<SnakeSegment>? SegmentAdded;
        public event EventHandler<Fruit>? FruitAdded;

        private readonly DispatcherTimer _timer = new();

        private Vector nextDirection = Vector.RIGHT;


        public Head SnakeHead { get; set; }
        public List<Fruit> Fruits { get; set; }

        public PlayField(Head snakeHead, List<Fruit> fruits)
        {
            SnakeHead = snakeHead;
            Fruits = fruits;
            nextDirection = SnakeHead.Direction;

            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            Tick();
            Trace.WriteLine("Tick");
        }

        public Result Tick()
        {
            SnakeHead.Direction = nextDirection;

            if (!SnakeHead.CanMove(SnakeHead.Direction))
            {
                Trace.WriteLine("Bonk!");
                return Result.COLLIDED;
            }

            var fruitsEaten = Fruits.FindAll(fruit => fruit.Position == SnakeHead.Position);

            if (fruitsEaten.Count > 0)
            {
                fruitsEaten.ForEach(_ =>
                {
                    var addedSegment = SnakeHead.AddSegment(SnakeHead.Direction * -1);
                    SegmentAdded?.Invoke(this, addedSegment);
                    
                });

                SnakeHead.Move(SnakeHead.Direction);
                Trace.WriteLine("Omnom!");
                return Result.ATE_FRUIT;
            }
            else
            {
                SnakeHead.Move(SnakeHead.Direction);
                Trace.WriteLine($"Slither {SnakeHead.Direction}");
                return Result.MOVED;
            }
        }

        public bool CanGoDirection(Direction direction)
        {
            Trace.WriteLine($"Can I move in {direction}? Snake looks {SnakeHead.Direction}.");
            return SnakeHead.Direction switch
            {
                (0, 1) or (0, -1) when direction == Direction.LEFT || direction == Direction.RIGHT => true,
                (1, 0) or (-1, 0) when direction == Direction.UP || direction == Direction.DOWN => true,
                _ => false,
            };
        }

        public void SetDirection(Direction direction)
        {
            Trace.WriteLine($"Move {direction}");
            nextDirection = direction switch
            {
                Direction.UP => Vector.UP,
                Direction.DOWN => Vector.DOWN,
                Direction.LEFT => Vector.LEFT,
                Direction.RIGHT => Vector.RIGHT,
                _ => throw new NotImplementedException("AAAAAAAAAAAAAAAAAAAAAAAAA"),
            };
        }
    }
}
