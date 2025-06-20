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
        /// <summary>
        /// Is triggered if a fruit is eaten by the snake.
        /// </summary>
        public event EventHandler<Fruit>? FruitEaten;
        /// <summary>
        /// Is triggered if a new snake Segment is added.
        /// </summary>
        public event EventHandler<SnakeSegment>? SegmentAdded;
        /// <summary>
        /// Is triggered if a fruit spawns.
        /// </summary>
        public event EventHandler<Fruit>? FruitAdded;
        /// <summary>
        /// Triggered if the score is updated.
        /// </summary>
        public event EventHandler<uint>? ScoreUpdated;

        /// <summary>
        /// The timer that defines the tick rate of the game.
        /// </summary>
        private readonly Timer timer;

        /// <summary>
        /// A direction Input buffer that stores the direction that will be used on the next tick.
        /// </summary>
        private Vector nextDirection = Vector.RIGHT;


        public Vector StartPosition { get; private set; } = new(1, 1);
        public Vector StartDirection { get; private set; } = Vector.RIGHT;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Head? SnakeHead { get; set; }
        public List<Fruit> Fruits { get; set; } = [];
        private uint score { get; set; } = 0;
        public uint Score
        {
            get => score; set
            {
                score = value;
                ScoreUpdated?.Invoke(this, score);
            }
        }

        private readonly Random random = new();
        /// <summary>
        /// Creates a play field.
        /// </summary>
        /// <param name="fruits">Fruits that are present at the start of the game</param>
        /// <param name="width">The width of the play field.</param>
        /// <param name="height">The height of the play field.</param>
        /// <param name="tickRateMS">The tick rate (in milliseconds).</param>
        public PlayField(List<Fruit> fruits, int width = 20, int height = 20, int tickRateMS = 64)
        {
            if (fruits != null)
            {
                Fruits.AddRange(fruits);
            }
            timer = new Timer(_ => Tick(), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(tickRateMS));
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Checks if a position is within the bounds of the play field
        /// </summary>
        /// <param name="pos">the position to check</param>
        /// <returns>if the position is inside the play field</returns>
        private bool WithinBounds(Vector pos) => pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;

        /// <summary>
        /// Execute 1 game tick and advance the snake 1 grid space.
        /// </summary>
        public void Tick()
        {
            Trace.WriteLine("Tick");
            if (SnakeHead == null)
            {
                return;
            }

            SpawnFruit();

            // actuall apply the direction from the direction buffers
            SnakeHead.Direction = nextDirection;

            // check if the snake can actually move
            if (!SnakeHead.CanMove(SnakeHead.Direction) || !WithinBounds(SnakeHead.NextPosition))
            {
                Trace.WriteLine("Bonk!");
                return; // TODO end game
            }

            EatFruits();

            // move the snake
            Trace.WriteLine($"Slither {SnakeHead.Direction}");
            SnakeHead.Move(SnakeHead.Direction);

        }

        /// <summary>
        /// Consumes Fruits that are in the same place as the head of the snake.
        /// </summary>
        private void EatFruits()
        {
            // only eat the fruit if the sneak has a head
            if (SnakeHead == null)
            {
                return;
            }

            var fruitsEaten = Fruits.FindAll(fruit => fruit.Position == SnakeHead.Position);

            // check if any fruits exist that could be eaten
            if (fruitsEaten.Count > 0)
            {
                // eat every single fruit and trigger the relevant events
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

        /// <summary>
        /// Spawns a fruit in an empty grid space
        /// </summary>
        private void SpawnFruit()
        {
            if (Fruits.Count == 0 && SnakeHead != null)
            {
                var position = new Vector(random.Next(Width), random.Next(Height));

                while (SnakeHead.ColidesWith(position))
                { // TODO use a table of free spaces instead as this could run forever
                    position = new Vector(random.Next(Width), random.Next(Height));
                }

                var fruit = new Fruit(position);

                Fruits.Add(fruit);
                FruitAdded?.Invoke(this, fruit);
            }
        }

        /// <summary>
        /// Adds a positive number of segments to the snake
        /// </summary>
        /// <param name="segmentsToAdd">the number of segments to add</param>
        public void AddSegments(uint segmentsToAdd)
        {
            for (uint i = 0; i < segmentsToAdd; i++)
            {
                AddSegment();
            }
        }

        /// <summary>
        /// Add 1 Segment to the Snake
        /// </summary>
        public void AddSegment()
        {
            // if we don't have a head spawn a head
            if (SnakeHead == null)
            {
                SnakeHead = new Head(StartPosition, StartDirection);
                SegmentAdded?.Invoke(this, SnakeHead);
            }
            else
            {
                // add the segment in the back direction of the head
                var addedSegment = SnakeHead.AddSegment(SnakeHead.Direction * -1);
                SegmentAdded?.Invoke(this, addedSegment);
            }
        }

        /// <summary>
        /// Check if the direction is valid (a 90 degree turn).
        /// </summary>
        /// <param name="direction">The direction the snake should move to</param>
        /// <returns>if the snake can go in that direction</returns>
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

        /// <summary>
        /// Actually set the direction the snake head should move in.
        /// </summary>
        /// <param name="direction">the direction the snake should move in</param>
        /// <exception cref="NotImplementedException">If an invalid direction is provided</exception>
        public void SetDirection(Direction direction)
        {
            Trace.WriteLine($"Move {direction}");
            nextDirection = direction switch
            {
                Direction.UP => Vector.UP,
                Direction.DOWN => Vector.DOWN,
                Direction.LEFT => Vector.LEFT,
                Direction.RIGHT => Vector.RIGHT,
                _ => throw new NotImplementedException("This cannot happen (unless a 3rd dimension is added)!"),
            };
        }
    }
}
