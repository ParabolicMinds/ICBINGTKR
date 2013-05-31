using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace ICBINGTKR
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var t = new Texture("bespin/basic");
            var g = new HollowBoxGenerator( new IntVec3(0, 0, 0), new IntVec3(1024, 1024, 1024), 4,  t);
            var worldspawn = new WorldspawnEntity(g.Brushes);

            worldspawn.AddAttribute("ambient", "300");
            worldspawn.AddAttribute("_color", (new Q3Color(0.7f, 0.6f, 0.6f)).ToString());

            Map testMap = new Map("generation", worldspawn);

            // Test backward brush direction.
            worldspawn.AddBrushes(new RightHexahedralBrushGenerator(new IntVec3(100, 100, 100), new IntVec3(90, 200, 200)).Brushes);

            // Test custom-shaped brushes for trisoup generator.
            List<Brush> brushes = new List<Brush> {
                new Brush(new List<BrushFace> {
                    new BrushFace(new IntVec3(64, 128, -368), new IntVec3(0, 128, -368), new IntVec3(0, 128, -432), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(64, 528, -508), new IntVec3(0, 528, -508), new IntVec3(0, -496, -508), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(0, -512, -384), new IntVec3(0, 512, -384), new IntVec3(64, 512, -384), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(0, 64, -368), new IntVec3(64, 64, -368), new IntVec3(64, 64, -432), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(64, -512, -384), new IntVec3(64, 512, -384), new IntVec3(64, 512, -448), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(0, 512, -384), new IntVec3(0, -512, -384), new IntVec3(0, 512, -448), new Texture("bespin/control02")),
                    new BrushFace(new IntVec3(64, 128, -384), new IntVec3(64, 64, -448), new IntVec3(0, 64, -384), new Texture("bespin/control02")),
                })
            };

            worldspawn.AddBrushes(brushes);

            testMap.AddEntity(new LightEntity(new IntVec3(200, 0, 0), 2000, new Q3Color(1, 0, 0)));
            testMap.AddEntity(new LightEntity(new IntVec3(-200, 0, 0), 2000, new Q3Color(0, 0, 1)));
            testMap.AddEntity(new JAInfoPlayerDeathmatchEntity(new IntVec3(0, 0, 0)));

            WriteMap(testMap);
            CompileMap(testMap);
        }

        public static void WriteMap(Map theMap)
        {
            StreamWriter mapWriter = new StreamWriter(theMap.MapName + ".map");
            mapWriter.Write(theMap);
            mapWriter.Close();
        }

        // Paths stored in q3map2.settings
        public static void CompileMap(Map theMap)
        {
            var appPath = ICBINGTKR.Properties.q3map2.Default.q3map2_path;
            var basePath = ICBINGTKR.Properties.q3map2.Default.fs_basepath;
            var commands = ICBINGTKR.Properties.q3map2.Default.bsp_compile_commands;

            foreach (String command in commands)
            {
                String comm = command.Replace("\\", "/")
                    .Replace("<BASE>", "\"" + basePath + "\"")
                    .Replace("<MAP>", "\"" + basePath + "base/maps/" + theMap.MapName + "\"");

                String args = "/C \"\"" + appPath + "q3map2.exe\" " + comm + "\"";

                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = args
                    }
                };
                process.Start();
                process.WaitForExit();
            }
        }
    }
}
