using System;
using System.IO;

namespace ICBINGTKR
{
	class MainClass {
		public static void Main (string[] args) {
			Map testmap = new Map("generation.map");
			testmap.NewBrush(new IntVec3(64,64,64), new IntVec3(512,512,512)).AddCuttingPlane(new IntVec3(64,64,256), new IntVec3(256,64,64), new IntVec3(64,256,64));
			testmap.NewBrush (new IntVec3(512,64,256), new IntVec3(1024,512,512));
			WriteMap(testmap);
		}

		public static void WriteMap (Map themap) {
			StreamWriter mapwriter = new StreamWriter(themap.mapname);
			mapwriter.Write(themap);
			mapwriter.Close();
		}
	}
}
