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
        NOTHING,
    }

    public class PlayField
    {
        public event EventHandler<Fruit>? FruitEaten;
        public event EventHandler<SnakeSegment>? SegmentAdded;
        public event EventHandler<Fruit>? FruitAdded;
        public event EventHandler<uint>? ScoreUpdated;

        private readonly Timer _timer;

        private Vector nextDirection = Vector.RIGHT;


        public Vector StartPosition { get; private set; } = new(1, 1);
        public Vector StartDirection { get; private set; } = Vector.RIGHT;
        public int Width {  get; private set; }
        public int Height { get; private set; }
        public Head? SnakeHead { get; set; }
        public List<Fruit> Fruits { get; set; }
        private uint score { get; set; } = 0;
        public uint Score { get => score; set
            {
                score = value;
                ScoreUpdated?.Invoke(this, score);
            }
        }

        private Random _random = new Random();

        public PlayField(Head? snakeHead, List<Fruit> fruits, int width = 20, int height= 20)
        {
            SnakeHead = snakeHead;
            Fruits = fruits;
            if (SnakeHead != null)
            {
                nextDirection = SnakeHead.Direction;
            }
            _timer = new Timer(_ => Tick(), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(64));
            Width = width;
            Height = height;
        }

        private bool WithinBounds(Vector pos) => pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;

        public void Tick()
        {
            Trace.WriteLine("Tick");
            if (SnakeHead == null)
            {
                return;
            }

            SpawnFruit();

            SnakeHead.Direction = nextDirection;


            if (!SnakeHead.CanMove(SnakeHead.Direction) || !WithinBounds(SnakeHead.NextPosition))
            {
                Trace.WriteLine("Bonk!");
                return; // TODO end game
            }

            EatFruits();

            Trace.WriteLine($"Slither {SnakeHead.Direction}");
            SnakeHead.Move(SnakeHead.Direction);

        }

        private void EatFruits()
        {
            var fruitsEaten = Fruits.FindAll(fruit => fruit.Position == SnakeHead.Position);

            if (fruitsEaten.Count > 0)
            {
                fruitsEaten.ForEach(fruit =>
                {
                    var addedSegment = SnakeHead.AddSegment(SnakeHead.Direction * -1);
                    SegmentAdded?.Invoke(this, addedSegment);
                    FruitEaten?.Invoke(this, fruit);
                    Score += 1;
                    Fruits.Remove(fruit);
                });

                Trace.WriteLine("Omnom!");
            }
        }

        private void SpawnFruit()
        {
            if (Fruits.Count == 0)
            {
                var position = new Vector(_random.Next(Width), _random.Next(Height));

                while (SnakeHead.ColidesWith(position))
                { // TODO use a table of free spaces instead as this could run forever
                    position = new Vector(_random.Next(Width), _random.Next(Height));
                }

                var fruit = new Fruit(position);

                Fruits.Add(fruit);
                FruitAdded?.Invoke(this, fruit);
            }
        }

        public void AddSegments(uint segmentsToAdd)
        {
            for (uint i = 0; i < segmentsToAdd; i++)
            {
                AddSegment();   
            }
        }

        public void AddSegment()
        {
            if (SnakeHead == null)
            {
                SnakeHead = new Head(StartPosition, StartDirection);
                SegmentAdded?.Invoke(this, SnakeHead);
            }
            else
            {
                var addedSegment = SnakeHead.AddSegment(SnakeHead.Direction * -1);
                SegmentAdded?.Invoke(this, addedSegment);
            }
        }

        public bool CanGoDirection(Direction direction)
        {
            Trace.WriteLine($"Can I move in {direction}? Snake looks {SnakeHead?.Direction}.");
            return SnakeHead?.Direction switch
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
