using System;
using System.IO;

namespace ICBINGTKR
{
	class MainClass {
		public static void Main (string[] args) {
            var b = (new HollowBoxGenerator(new IntVec3(0, 0, 0), new IntVec3(1024, 1024, 1024), 4, new Texture("bespin/basic"))).Brushes;
            var e = new WorldspawnEntity(b);
            Map testMap = new Map("generation.map", e);
            testMap.AddEntity(new LightEntity(new IntVec3(40, 0, 0), 2000));
            testMap.AddEntity(new JAInfoPlayerDeathmatchEntity(new IntVec3(0, 0, 0)));
            //testmap.NewBrush(new IntVec3(64,64,64), new IntVec3(512,512,512)).AddCuttingPlane(new IntVec3(64,64,256), new IntVec3(256,64,64), new IntVec3(64,256,64));
            //testmap.NewBrush (new IntVec3(512,64,256), new IntVec3(1024,512,512));
            //testmap.NewBrush(new IntVec3(-8, -8, -8), new IntVec3(8, 8, 8));
            //testmap.NewBrush(new IntVec3(-64, -64, 68), new IntVec3(64, 64, 64));
            //testmap.AddBrush((new HollowBoxGenerator(new IntVec3(0, 0, 0), new IntVec3(128, 128, 128), 4, new Texture("bespin/basic"))).Brushes);
			WriteMap(testMap);
		}

		public static void WriteMap (Map theMap) {
			StreamWriter mapWriter = new StreamWriter(theMap.MapName);
			mapWriter.Write(theMap);
			mapWriter.Close();
		}
	}
}
