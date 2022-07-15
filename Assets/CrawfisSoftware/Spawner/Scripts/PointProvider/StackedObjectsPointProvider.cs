using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public static class StackedObjectsPointProvider
    {
        public static IEnumerable<Vector3> DuplicateInY(IEnumerable<Vector2> point2DStream, int numberOfStacks, float initialyValue = 0f, float ySpacing = 1)
        {
            float y = initialyValue;

            for (int i = 0; i < numberOfStacks; i++)
            {
                foreach (Vector3 point in Point2DTransforms.LiftXZto3D(point2DStream, y))
                {
                    yield return point;
                }
                y += ySpacing;
            }
        }
        public static IEnumerable<Vector3> DuplicateInY(IEnumerable<Vector2> point2DStream, Func<int, int> stackHeightFunc, float initialyValue = 0f, float ySpacing = 1)
        {
            int cellIndex = 0;

            foreach (Vector3 point in point2DStream)
            {
                int numberOfStacks = stackHeightFunc(cellIndex++);
                float y = initialyValue;
                for (int i = 0; i < numberOfStacks; i++)
                {
                    yield return new Vector3(point.x, y, point.y);
                    y += ySpacing;
                }
            }
        }
    }
}