﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mercraft.Core.Scene;
using Mercraft.Core.Scene.Models;

namespace Mercraft.Maps.UnitTests
{
    public static class SceneHelper
    {
        public static void DumpScene(MapScene mapScene)
        {
            using (var file = File.CreateText(@"f:\scene.txt"))
            {
                file.WriteLine("Areas:");
                var areas = mapScene.Areas.ToList();
                for (int i = 0; i < areas.Count; i++)
                {
                    var building = areas[i];
                    file.WriteLine(@"\tAreas {0}", (i + 1));
                    var lineOffset = "\t\t";
                    file.WriteLine("{0}Tags:", lineOffset);
                    foreach (var tag in building.Tags)
                    {
                        file.WriteLine("{0}\t{1}:{2}", lineOffset, tag.Key, tag.Value);
                    }
                    file.WriteLine("{0}Points:", lineOffset);
                    foreach (var point in building.Points)
                    {
                        file.WriteLine("{0}\tnew GeoCoordinate({1},{2}),", lineOffset, point.Latitude, point.Longitude);
                    }
                }

                file.WriteLine("Ways:");
                var ways = mapScene.Ways.ToList();
                for (int i = 0; i < ways.Count; i++)
                {
                    var building = ways[i];
                    file.WriteLine(@"\tWays {0}", (i + 1));
                    var lineOffset = "\t\t";
                    file.WriteLine("{0}Tags:", lineOffset);
                    foreach (var tag in building.Tags)
                    {
                        file.WriteLine("{0}\t{1}:{2}", lineOffset, tag.Key, tag.Value);
                    }
                    file.WriteLine("{0}Points:", lineOffset);
                    foreach (var point in building.Points)
                    {
                        file.WriteLine("{0}\tnew GeoCoordinate({1},{2}),", lineOffset, point.Latitude, point.Longitude);
                    }
                }
            }
        }
    }
}