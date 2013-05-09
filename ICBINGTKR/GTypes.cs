using System;

/// <summary>
/// This contains geometrical types such as points and planes.
/// </summary>

namespace ICBINGTKR
{
    class IntVec3
    {
        public int x;
        public int y;
        public int z;
        public IntVec3(int[] arry) : this(arry[0], arry[1], arry[2]) { }
        public IntVec3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public override string ToString()
        {
            return this.x + " " + this.y + " " + this.z;
        }
    }

    class Q3Color
    {
        public float r;
        public float g;
        public float b;
        public Q3Color(int[] arry) : this(arry[0], arry[1], arry[2]) { }
        public Q3Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
        public override string ToString()
        {
            return this.r + " " + this.g + " " + this.b;
        }
    }

    class IntPlane
    {
        public IntVec3 VectorA;
        public IntVec3 VectorB;
        public IntVec3 VectorC;
        public IntPlane(int ax, int ay, int az, int bx, int by, int bz, int cx, int cy, int cz)
            : this(new IntVec3(ax, ay, az), new IntVec3(bx, by, bz), new IntVec3(cx, cy, cz)) { }

        public IntPlane(IntVec3 veca, IntVec3 vecb, IntVec3 vecc)
        {
            this.VectorA = veca;
            this.VectorB = vecb;
            this.VectorC = vecc;
        }

        private String addParens(String s)
        {
            return "( " + s + " )";
        }

        public override string ToString()
        {
            return addParens(VectorA.ToString()) + " " + addParens(VectorB.ToString()) + " " + addParens(VectorC.ToString());
        }
    }
}

