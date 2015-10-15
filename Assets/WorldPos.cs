using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public struct WorldPos
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;

        public WorldPos(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            return ((WorldPos)obj).X == X && ((WorldPos)obj).Y == Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) + X;
                result = (result * 397) + Y;
                result = (result * 397) + Z;
                return result;
            }
        }
    }
}
