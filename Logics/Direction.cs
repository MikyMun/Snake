using System;
using System.Collections.Generic;

namespace Snake
{
    public class Direction
    {
        public readonly static Direction left = new Direction(0, -1);
        public readonly static Direction right = new Direction(0, 1);
        public readonly static Direction up = new Direction(-1, 0);
        public readonly static Direction down = new Direction(1, 0);


        public int rowOffSet { get; }
        public int columnOffSet { get; }

        private Direction(int rowOffSet, int columnOffSet)
        {
            this.rowOffSet = rowOffSet;
            this.columnOffSet = columnOffSet;
        }

        public Direction Opposite()
        {
            return new Direction(-rowOffSet, -columnOffSet);
        }

        public override bool Equals(object? obj)
        {
            return obj is Direction direction &&
                   rowOffSet == direction.rowOffSet &&
                   columnOffSet == direction.columnOffSet;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(rowOffSet, columnOffSet);
        }

        public static bool operator ==(Direction? left, Direction? right)
        {
            return EqualityComparer<Direction>.Default.Equals(left, right);
        }

        public static bool operator !=(Direction? left, Direction? right)
        {
            return !(left == right);
        }
    }
}
