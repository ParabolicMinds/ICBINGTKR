using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

/// <summary>
/// This contains multi-Brush "assembly" types that can be used to build a map.
/// </summary>

namespace ICBINGTKR
{
    abstract class AssemblyGenerator
    {
        protected List<Brush> brushes = new List<Brush>();

        public List<Brush> Brushes { get { return this.brushes; } }
    }

    class RightHexahedralBrushGenerator : AssemblyGenerator
    {
        private IntVec3 spoint, epoint;

        public RightHexahedralBrushGenerator(IntVec3 veca, IntVec3 vecb)
            : this(veca, vecb, BrushFace.DEFAULT_TEXTURE) { }
        public RightHexahedralBrushGenerator(IntVec3 veca, IntVec3 vecb, Texture texture)
        {
            this.spoint = veca;
            this.epoint = vecb;
            Utils.fixDirection(ref this.spoint, ref this.epoint);

            Brush b = new Brush();

            b.AddFace(new BrushFace(new IntPlane(this.spoint.x, 0, 0, this.spoint.x, 1, 0, this.spoint.x, 0, 1), texture));
            b.AddFace(new BrushFace(new IntPlane(this.epoint.x, 0, 0, this.epoint.x, 0, 1, this.epoint.x, 1, 0), texture));
            b.AddFace(new BrushFace(new IntPlane(0, this.spoint.y, 0, 0, this.spoint.y, 1, 1, this.spoint.y, 0), texture));
            b.AddFace(new BrushFace(new IntPlane(0, this.epoint.y, 0, 1, this.epoint.y, 0, 0, this.epoint.y, 1), texture));
            b.AddFace(new BrushFace(new IntPlane(0, 0, this.spoint.z, 1, 0, this.spoint.z, 0, 1, this.spoint.z), texture));
            b.AddFace(new BrushFace(new IntPlane(0, 0, this.epoint.z, 0, 1, this.epoint.z, 1, 0, this.epoint.z), texture));
            
            brushes.Add(b);
        }
    }

    class HollowBoxGenerator : AssemblyGenerator
    {
        public HollowBoxGenerator(IntVec3 origin, IntVec3 dimensions, int wallThickness)
            : this(origin, dimensions, wallThickness, BrushFace.DEFAULT_TEXTURE) { }
        public HollowBoxGenerator(IntVec3 origin, IntVec3 dimensions, int wallThickness, Texture texture)
        {
            // Top
            {
                var veca = new IntVec3(
                    origin.x - (dimensions.x / 2),
                    origin.y - (dimensions.y / 2),
                    origin.z + (dimensions.z / 2) - wallThickness);
                var vecb = new IntVec3(
                    origin.x + (dimensions.x / 2),
                    origin.y + (dimensions.y / 2),
                    origin.z + (dimensions.z / 2));
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }

            // Bottom
            {
                var veca = new IntVec3(
                    origin.x - (dimensions.x / 2),
                    origin.y - (dimensions.y / 2),
                    origin.z - (dimensions.z / 2));
                var vecb = new IntVec3(
                    origin.x + (dimensions.x / 2),
                    origin.y + (dimensions.y / 2),
                    origin.z - (dimensions.z / 2) + wallThickness);
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }

            // Side +x
            {
                var veca = new IntVec3(
                    origin.x + (dimensions.x / 2) - wallThickness,
                    origin.y - (dimensions.y / 2),
                    origin.z - (dimensions.z / 2));
                var vecb = new IntVec3(
                    origin.x + (dimensions.x / 2),
                    origin.y + (dimensions.y / 2),
                    origin.z + (dimensions.z / 2));
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }

            // Side -x
            {
                var veca = new IntVec3(
                    origin.x - (dimensions.x / 2),
                    origin.y - (dimensions.y / 2),
                    origin.z - (dimensions.z / 2));
                var vecb = new IntVec3(
                    origin.x - (dimensions.x / 2) + wallThickness,
                    origin.y + (dimensions.y / 2),
                    origin.z + (dimensions.z / 2));
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }

            // Side +y
            {
                var veca = new IntVec3(
                    origin.x - (dimensions.x / 2),
                    origin.y + (dimensions.y / 2) - wallThickness,
                    origin.z - (dimensions.z / 2));
                var vecb = new IntVec3(
                    origin.x + (dimensions.x / 2),
                    origin.y + (dimensions.y / 2),
                    origin.z + (dimensions.z / 2));
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }

            // Side -y
            {
                var veca = new IntVec3(
                    origin.x - (dimensions.x / 2),
                    origin.y - (dimensions.y / 2),
                    origin.z - (dimensions.z / 2));
                var vecb = new IntVec3(
                    origin.x + (dimensions.x / 2),
                    origin.y - (dimensions.y / 2) + wallThickness,
                    origin.z + (dimensions.z / 2));
                brushes.AddRange(new RightHexahedralBrushGenerator(veca, vecb, texture).Brushes);
            }
        }
    }

    class RoomGenerator : AssemblyGenerator
    {
    }

    class TriSoupBrushGenerator : AssemblyGenerator
    {
        private IntVec3 spoint, epoint;

        public TriSoupBrushGenerator(IntVec3 veca, IntVec3 vecb)
            : this(veca, vecb, BrushFace.DEFAULT_TEXTURE) { }
        public TriSoupBrushGenerator(IntVec3 veca, IntVec3 vecb, Texture texture)
        {
            this.spoint = veca;
            this.epoint = vecb;
            Utils.fixDirection(ref this.spoint, ref this.epoint);

            Brush b = new Brush();

            b.AddFace(new BrushFace(new IntPlane(this.spoint.x, 0, 0, this.spoint.x, 1, 0, this.spoint.x, 0, 1), texture));
            b.AddFace(new BrushFace(new IntPlane(this.epoint.x, 0, 0, this.epoint.x, 0, 1, this.epoint.x, 1, 0), texture));
            b.AddFace(new BrushFace(new IntPlane(0, this.spoint.y, 0, 0, this.spoint.y, 1, 1, this.spoint.y, 0), texture));
            b.AddFace(new BrushFace(new IntPlane(0, this.epoint.y, 0, 1, this.epoint.y, 0, 0, this.epoint.y, 1), texture));
            b.AddFace(new BrushFace(new IntPlane(0, 0, this.spoint.z, 1, 0, this.spoint.z, 0, 1, this.spoint.z), texture));
            b.AddFace(new BrushFace(new IntPlane(0, 0, this.epoint.z, 0, 1, this.epoint.z, 1, 0, this.epoint.z), texture));

            brushes.Add(b);
        }
    }

    abstract class TriSoupGenerator : AssemblyGenerator
    {
        protected int[,] heightmap;

        public void ApplyBoxBlur(int range)
        {
            for (int i = 0; i < heightmap.GetLength(0); i++)
            {
                for (int j = 0; j < heightmap.GetLength(1); j++)
                {

                }
            }
        }
    }

    class DiamondSquareTriSoupGenerator : TriSoupGenerator
    {
        public DiamondSquareTriSoupGenerator(IntVec3 veca, IntVec3 vecb, int scale, float randomnessFactor)
        {
            var xCount = (vecb.x - veca.x) / scale;
            var yCount = (vecb.y - veca.y) / scale;

        }



    }
}
