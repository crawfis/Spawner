using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    /// <summary>
    /// Creates a stream IEnumerator of positions (Vector3's).
    /// </summary>
    public class PointProviderGrid : IEnumerable<Vector3>
    {
        private int nx = 10;
        private int ny = 10;
        private Vector2 gridSize;
        private Vector2 offset;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nx">The size of the grid in x.</param>
        /// <param name="ny">The size of the grid in y.</param>
        /// <param name="prefabSize">The prefab size or grid cell size.</param>
        /// <param name="offset">An offset used for each position.</param>
        public PointProviderGrid(int nx, int ny, Vector2 prefabSize, Vector2 offset)
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
            Vector3 position = new Vector3(-offset.x, 0, -offset.y); // Recenter to (0,y,0)

            position.x += gridSize.x / 2;
            // Create Parent
            for (int i = 0; i < nx; i++)
            {
                position.z = -offset.y + gridSize.y / 2;
                for (int j = 0; j < ny; j++)
                {
                    yield return position;
                    position.z += gridSize.y;
                }
                position.x += gridSize.x;
            }
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
