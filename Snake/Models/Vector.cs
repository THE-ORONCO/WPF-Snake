using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{
    /// <summary>
    /// A 2D whole number vector.
    /// </summary>
    /// <param name="x">the offset in the x direction</param>
    /// <param name="y">the offset in the y direction</param>
    public readonly struct Vector(int x, int y)
    {
        #region SOME DEFAULT VECTORS
        public readonly static Vector UP = new(0, -1);
        public readonly static Vector DOWN = new(0, 1);
        public readonly static Vector RIGHT = new(1, 0);
        public readonly static Vector LEFT = new(-1, 0);
        #endregion


        public void Deconstruct(out int x, out int y)
        {
            x = X; y = Y;
        }

        public int X { get; } = x;
        public int Y { get; } = y;


        #region OPERATORS
        public static bool operator ==(Vector a, Vector b) => a.Equals(b);

        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);

        public static Vector operator *(Vector a, int factor) => new(a.X * factor, a.Y * factor);

        public static Vector operator *(int factor, Vector a) => new(a.X * factor, a.Y * factor);


        public override bool Equals(object? obj) => obj is Vector vector && X == vector.X && Y == vector.Y;

        public override int GetHashCode() => HashCode.Combine(X, Y);
        #endregion

        public override string ToString() => $"({X}, {Y})";
    }
}
