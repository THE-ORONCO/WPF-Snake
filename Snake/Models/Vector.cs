using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    public struct Vector
    {
        public Vector(int x, int y)
        {
            X = x; Y = y;
        }
        public void Deconstruct(out int x, out int y)
        {
            x = X; y = Y;
        }

        public int X { get; }
        public int Y { get; }


        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }


        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);

        public static Vector operator *(Vector a, int factor) => new(a.X * factor, a.Y * factor);

        public static Vector operator *(int factor, Vector a) => new(a.X * factor, a.Y * factor);
 

        public override bool Equals(object? obj)
        {
            return obj is Vector vector &&
                   X == vector.X &&
                   Y == vector.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString() => $"({X}, {Y})";


        public readonly static Vector UP = new(0, 1);
        public readonly static Vector DOWN = new(0, -1);
        public readonly static Vector RIGHT = new(1, 0);
        public readonly static Vector LEFT = new(-1, 0);

    }
}
