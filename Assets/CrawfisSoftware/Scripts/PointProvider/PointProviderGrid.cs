using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    /// <summary>
    /// Creates a stream (IEnumerator) of positions (Vector3's) on a grid.
    /// </summary>
    public class PointProviderGrid : IPositionGenerator, IEnumerable<Vector3>
    {
        private int nx = 10;
        private int ny = 10;
        private Vector3 gridSize;
        private Vector3 offset;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nx">The size of the grid in x.</param>
        /// <param name="ny">The size of the grid in y.</param>
        /// <param name="prefabSize">The prefab size or grid cell size.</param>
        /// <param name="offset">An offset used for each position.</param>
        public PointProviderGrid(int nx, int ny, Vector3 prefabSize, Vector3 offset)
        {
            this.nx = nx;
            this.ny = ny;
            gridSize = prefabSize;
            this.offset = offset;
        }

        /// <summary>
        /// Get the Enumerator of Vector3's.
        /// </summary>
        /// <returns>A stream of Vector3's.</returns>
        public IEnumerator<Vector3> GetEnumerator()
        {
            var enumerable = CreatePoints2D.Grid(nx, ny, new Vector2(gridSize.x, gridSize.z), new Vector2(offset.x, offset.z));
            var enumerable2 = Point2DTransforms.Jitter(enumerable, new Vector2(0.1f,0.1f));
            var enumerable3 = Point2DTransforms.MirrorHorizontal(enumerable2);
            var enumerable4 = Point2DTransforms.MirrorVertical(enumerable3);
            var enumerable5 = EnumerableHelpers.Shuffle(enumerable4.ToList());
            foreach (Vector2 gridPoint in enumerable5)
            {
                yield return new Vector3(gridPoint.x, 0, gridPoint.y);
            }
        }

        public IList<Vector3> GetNextSet()
        {
            return this.ToList();
        }

        /// <summary>
        /// Non-generic enumerator.
        /// </summary>
        /// <returns>The generic Enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
