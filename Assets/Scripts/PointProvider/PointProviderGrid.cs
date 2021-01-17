using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    /// <summary>
    /// Creates a stream (IEnumerator) of positions (Vector3's) on a grid.
    /// </summary>
    public class PointProviderGrid : IEnumerable<Vector3>
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
            //Vector3 position = new Vector3(-offset.x, 0, -offset.y); // Recenter to (0,y,0)
            Vector3 position = -offset; // Recenter to (0,y,0)

            position.x += gridSize.x / 2;
            // Create Parent
            for (int i = 0; i < nx; i++)
            {
                position.z = -offset.z + gridSize.z / 2;
                for (int j = 0; j < ny; j++)
                {
                    yield return position;
                    position.z += gridSize.z;
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
