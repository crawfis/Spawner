using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public static class Point2DTransforms
    {
        public static IEnumerable<Vector3> LiftXZto3D(IEnumerable<Vector2> point2DStream, float yValue = 0f)
        {
            foreach(Vector2 point2D in point2DStream)
            {
                yield return new Vector3(point2D.x, yValue, point2D.y);
            }
        }

        public static IEnumerable<Vector3> LiftXZto3D(IEnumerable<Vector2> point2DStream, Func<float,float,float> determineHeight)
        {
            foreach (Vector2 point2D in point2DStream)
            {
                float yValue = determineHeight(point2D.x, point2D.y);
                yield return new Vector3(point2D.x, yValue, point2D.y);
            }
        }

        public static IEnumerable<Vector2> Translate(IEnumerable<Vector2> point2DStream, Vector2 translation)
        {
            foreach (Vector2 point2D in point2DStream)
            {
                yield return point2D + translation;
            }
        }

        public static IEnumerable<Vector2> Scale(IEnumerable<Vector2> point2DStream, Vector2 scale)
        {
            foreach (Vector2 point2D in point2DStream)
            {
                yield return point2D * scale;
            }
        }

        public static IEnumerable<Vector2> Jitter(IEnumerable<Vector2> point2DStream, Vector2 maxJitterAmount, System.Random random = null)
        {
            if (random == null) random = new System.Random();
            foreach (Vector2 point2D in point2DStream)
            {
                Vector2 offset = 2f * maxJitterAmount * new Vector2((float)random.NextDouble()-0.5f, (float)random.NextDouble()-0.5f);
                yield return point2D + offset;
            }
        }

        public static IEnumerable<Vector2> MirrorHorizontal(IEnumerable<Vector2> point2DStream)
        {
            foreach (Vector2 point2D in point2DStream)
            {
                yield return point2D;
                yield return new Vector2(-point2D.x, point2D.y);
            }
        }

        public static IEnumerable<Vector2> MirrorVertical(IEnumerable<Vector2> point2DStream)
        {
            foreach (Vector2 point2D in point2DStream)
            {
                yield return point2D;
                yield return new Vector2(point2D.x, -point2D.y);
            }
        }
    }
}
