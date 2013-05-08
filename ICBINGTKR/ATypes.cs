using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This contains multi-Brush "assembly" types that can be used to build a map.
/// </summary>

namespace ICBINGTKR
{
    abstract class AssemblyGenerator
    {
        protected List<Brush> brushes = new List<Brush>();

        public IEnumerable<Brush> Brushes { get { return this.brushes; } }
    }

    class HollowBoxGenerator : AssemblyGenerator
    {
        public HollowBoxGenerator(IntVec3 origin, IntVec3 dimensions, int wallThickness, Texture tex = null)
        {
            if (tex == null)
            {
                tex = Brush.defaultTexture;
            }

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
                brushes.Add(new Brush(veca, vecb, tex));
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
                brushes.Add(new Brush(veca, vecb, tex));
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
                brushes.Add(new Brush(veca, vecb, tex));
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
                brushes.Add(new Brush(veca, vecb, tex));
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
                brushes.Add(new Brush(veca, vecb, tex));
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
                brushes.Add(new Brush(veca, vecb, tex));
            }
        }
    }

    class RoomGenerator : AssemblyGenerator
    {
    }
}
