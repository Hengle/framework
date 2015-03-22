﻿using System;
using ActionStreetMap.Core;
using ActionStreetMap.Explorer.Geometry;
using ActionStreetMap.Unity.Wrappers;
using UnityEngine;

namespace ActionStreetMap.Explorer.Scene.Buildings.Facades
{
    internal class EmptySideBuilder : SideBuilder
    {
        private GradientWrapper _facadeGradient;

        public EmptySideBuilder(MeshData meshData, float height)
            : base(meshData, height)
        {
        }

        public EmptySideBuilder SetFacadeGradient(GradientWrapper gradient)
        {
            _facadeGradient = gradient;
            return this;
        }

        protected override void BuildGroundFloor(MapPoint start, MapPoint end, float floorHeight)
        {
            var width = 12f;
            var floor = 0;

            var distance = start.DistanceTo(end);
            var count = (float) Math.Ceiling(distance/width);
            var ceiling = floorHeight;

            var widthStep = distance/count;

            var direction = (end - start).Normalize();
            for (int k = 0; k < count; k++)
            {
                var p1 = start + direction*(widthStep*k);
                var p2 = start + direction*(widthStep*(k + 1));

                var floorNoise1 = GetPositionNoise(new Vector3(p1.X, floor, p1.Y));
                var floorNoise2 = GetPositionNoise(new Vector3(p2.X, floor, p2.Y));

                var a = new Vector3(p1.X + floorNoise1, floor + floorNoise1, p1.Y + floorNoise1);
                var b = new Vector3(p2.X + floorNoise2, floor + floorNoise2, p2.Y + floorNoise2);
                var c = new Vector3(p2.X, ceiling, p2.Y);
                var d = new Vector3(p1.X, ceiling, p1.Y);

                AddPlane(Color.grey, Color.grey, a, b, c, d);
            }
        }

        protected override void BuildWindow(int step, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            BuildSpan(step, a, b, c, d);
        }

        protected override void BuildSpan(int step, Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            var color1 = GetColor(_facadeGradient, a);
            var color2 = GetColor(_facadeGradient, b);
            AddPlane(color1, color2, a, b, c, d);
        }

        protected override void BuildGlass(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {

        }
    }
}
