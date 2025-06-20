﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake.Models
{

    public abstract class SnakeSegment : IGridEntity
    {
        public event EventHandler<Vector>? MovedInDirection;
        public SnakeSegment? Next { get; set; }
        public Vector position { get; set; }
        public Vector Position
        {
            get => position; set
            {
                var oldPosition = position;
                position = value;
                MovedInDirection?.Invoke(this, oldPosition - position);
            }
        }

        /// <summary>
        /// Adds a Segment as the child of this segment (or grandchild (or grandgrandchild ...)) in the specified direction.
        /// If it the segment already has a child we recurse deeper and add it to the child instead.
        /// </summary>
        /// <param name="backDirection"></param>
        public SnakeSegment AddSegment(Vector backDirection)
        {
            if (this.Next == null)
            {
                // add a tail segment if none is present
                this.Next = new Tail(this.Position + backDirection);
                return this.Next;
            }
            else
            {
                //TODO remove recursion
                return this.Next.AddSegment(this.Position - Next.Position);
            }
        }

        /// <summary>
        /// Checks if the Snake could move into the specified direction. This could be recursive but that might cause a stack overflow for very long snakes.
        /// </summary>
        /// <param name="direction">the direction to check</param>
        /// <returns>true if the Snake can move into the specified direction, otherwise false.</returns>
        public bool CanMove(Vector direction)
        {
            SnakeSegment? next = this.Next;
            var newPosition = this.Position + direction;

            while (next != null)
            {
                if (next.Position == newPosition)
                {
                    return false;
                }

                next = next.Next;
            }
            return true;
        }

        /// <summary>
        /// Moves the segment and all its trailing segments. This could be recursive but that might cause a stack overflow for very long snakes.
        /// This method will move the snake into positions that might cause overlapping!
        /// </summary>
        /// <param name="direction">the direction the segment should move in </param>
        public void Move(Vector direction)
        {

            // the next position should be 
            var nextPosition = this.Position + direction;
            SnakeSegment? thingToMove = this;

            while (thingToMove != null)
            {
                // remember the current position of the segment
                var oldPosition = thingToMove.Position;

                // move the segment
                thingToMove.Position = nextPosition;

                // get the next segment to move
                thingToMove = thingToMove.Next;

                // remember the old position of the segment as the next segment should be moved to its position
                nextPosition = oldPosition;
            }
        }

        /// <summary>
        /// Check if the segment (or any of its children) intersects a position on the grid.
        /// </summary>
        /// <param name="position">the position to check</param>
        /// <returns>if a collision was detected</returns>
        public bool ColidesWith(Vector position) => this.Position == position || (this.Next?.ColidesWith(position) ?? false);
    }
}
