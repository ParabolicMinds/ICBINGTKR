using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ICBINGTKR
{
    static class Utils
    {
        // Ensure that the brush coordinates are
        // turned the right way.
        public static void fixDirection(ref IntVec3 veca, ref IntVec3 vecb)
        {
            var a = new IntVec3(
                    Math.Min(veca.x, vecb.x),
                    Math.Min(veca.y, vecb.y),
                    Math.Min(veca.z, vecb.z));
            var b = new IntVec3(
                    Math.Max(veca.x, vecb.x),
                    Math.Max(veca.y, vecb.y),
                    Math.Max(veca.z, vecb.z));
            veca = a;
            vecb = b;
        }
    }
}
