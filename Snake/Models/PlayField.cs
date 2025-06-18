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
        public event EventHandler<List<Fruit>>? AteFruit;
        public event EventHandler<List<SnakeSegment>>? SnakeMoved;

        private DispatcherTimer _timer = new DispatcherTimer();


        public Head SnakeHead { get; set; }
        public List<Fruit> Fruits { get; set; }

        public PlayField(Head snakeHead, List<Fruit> fruits)
        {
            SnakeHead = snakeHead;
            Fruits = fruits;

            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += TimerTick;
            _timer.Start();
        }

        private void TimerTick(object? sender, EventArgs e)
        {
            Tick();
            List<SnakeSegment> segments = [];
            SnakeSegment? segment = SnakeHead;
            while (segment != null)
            {
                segments.Add(segment);
                segment = segment.Next;
            }
            SnakeMoved?.Invoke(this, segments);
            Trace.WriteLine("Tick");
        }

        public Result Tick()
        {

            if (!SnakeHead.CanMove(SnakeHead.Direction))
            {
                Trace.WriteLine("Bonk!");
                return Result.COLLIDED;
            }

            var fruitsEaten = Fruits.FindAll(fruit => fruit.Position == SnakeHead.Position);

            if (fruitsEaten.Count > 0)
            {
                AteFruit?.Invoke(this, fruitsEaten);
                fruitsEaten.ForEach(_ => SnakeHead.AddSegment(SnakeHead.Direction * -1));

                SnakeHead.Move(SnakeHead.Direction);
                Trace.WriteLine("Omnom!");
                return Result.ATE_FRUIT;
            }
            else
            {
                SnakeHead.Move(SnakeHead.Direction);
                Trace.WriteLine("Slither");
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
            SnakeHead.Direction = direction switch
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
