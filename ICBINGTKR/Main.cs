using System;
using System.IO;
using System.Diagnostics;

namespace ICBINGTKR
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var g = new HollowBoxGenerator(
                new IntVec3(0, 0, 0),
                new IntVec3(1024, 1024, 1024),
                4,
                new Texture("bespin/basic"));
            var b = g.Brushes;
            var worldspawn = new WorldspawnEntity(b);
            worldspawn.AddAttribute("ambient", "300");
            worldspawn.AddAttribute("_color", (new Q3Color(0.7f, 0.6f, 0.6f)).ToString());

            Map testMap = new Map("generation.map", worldspawn);

            testMap.AddEntity(new LightEntity(new IntVec3(200, 0, 0), 2000, new Q3Color(1, 0, 0)));
            testMap.AddEntity(new LightEntity(new IntVec3(-200, 0, 0), 2000, new Q3Color(0, 0, 1)));
            testMap.AddEntity(new JAInfoPlayerDeathmatchEntity(new IntVec3(0, 0, 0)));

            WriteMap(testMap);
            CompileMap(testMap);
        }

        public static void WriteMap(Map theMap)
        {
            StreamWriter mapWriter = new StreamWriter(theMap.MapName);
            mapWriter.Write(theMap);
            mapWriter.Close();
        }

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
