using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public static class CreatePoints2D
    {
        /// <summary>
        /// Return a stream of Vector2's on a circle of radius R.
        /// </summary>
        /// <param name="nSegments">The number of samples on the circle.</param>
        /// <param name="includeCenter">If true, the first Vector2 is the center location.</param>
        /// <returns>A stream of Vector2's.</returns>
        public static IEnumerable<Vector2> RegularPolygon(int nSegments, bool includeCenter = false)
        {
            return RegularPolygon(nSegments, Vector2.zero, 1, includeCenter);
        }

        /// <summary>
        /// Return a stream of Vector2's on a circle of radius R.
        /// </summary>
        /// <param name="nSegments">The number of samples on the circle.</param>
        /// <param name="centerPosition">An optional offset.</param>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="includeCenter">If true, the first Vector2 is the center location.</param>
        /// <returns>A stream of Vector2's.</returns>
        public static IEnumerable<Vector2> RegularPolygon(int nSegments, Vector2 centerPosition, float radius = 1f, bool includeCenter = false)
        {
            if(includeCenter)
                yield return centerPosition;
            float angleDelta = 2f * Mathf.PI / nSegments;
            float angle = 0;
            for(int i=0; i < nSegments; i++)
            {
                float x = radius * Mathf.Cos(angle) + centerPosition.x;
                float y = radius * Mathf.Sin(angle) + centerPosition.y;
                yield return new Vector2(x, y);
                angle += angleDelta;
            }
        }

        public static IEnumerable<Vector2> Grid(int nx, int ny, Vector2 voxelSize, Vector2 voxelOffset)
        {
            Vector2 position = -voxelOffset;

            position.x += voxelSize.x / 2;
            for (int i = 0; i < nx; i++)
            {
                position.y = -voxelOffset.y + voxelSize.y / 2;
                for (int j = 0; j < ny; j++)
                {
                    yield return position;
                    position.y += voxelSize.y;
                }
                position.x += voxelSize.x;
            }
        }

        /// <summary>
        /// An infinite stream of points on the unit square (0,0) ->(1,1)
        /// </summary>
        /// <param name="random">An optional System.Random number generator.</param>
        /// <returns>An infinite stream of Vector2's.</returns>
        public static IEnumerable<Vector2> RandomSamplingOnUnitSquare(System.Random random = null)
        {
            if (random == null)
                random = new System.Random();
            while (true)
            {
                yield return new Vector2((float)random.NextDouble(), (float)random.NextDouble());
            }
        }

        /// <summary>
        /// Given a stream of points (2D), determine which ones to ouput.
        /// </summary>
        /// <param name="stream">An IEnumerable of Vector2's</param>
        /// <param name="oracle">A System.Predicate function. If true, keep (output) the Vector2.</param>
        /// <returns>A stream of points (2D) which is a subset of the initial input.</returns>
        public static IEnumerable<Vector2> MaskedEnumeration(IEnumerable<Vector2> stream, Predicate<Vector2> oracle)
        {
            foreach(Vector2 point in stream)
            {
                if (oracle(point))
                {
                    yield return point;
                }
            }
        }
    }
}
