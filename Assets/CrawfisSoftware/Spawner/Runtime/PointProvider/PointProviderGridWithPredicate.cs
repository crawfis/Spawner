using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    /// <summary>
    /// Creates a stream (IEnumerator) of positions (Vector3's) on a grid with
    /// each location checked by a predicate function (a "mask").
    /// </summary>
    public class PointProviderGridWithPredicate : PointProviderGrid
    {
        private readonly Func<Vector3, bool> predicate;

        public PointProviderGridWithPredicate(int nx, int ny, Func<Vector3, bool> predicate, Vector2 prefabSize, Vector2 offset)
            : base(nx, ny, prefabSize, offset)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Get the Enumerator of Vector3's.
        /// </summary>
        /// <returns>A stream of Vector3's.</returns>
        public new IEnumerator<Vector3> GetEnumerator()
        {
            var enumerator = base.GetEnumerator();
            while(enumerator.MoveNext())
            {
                var position = enumerator.Current;
                if (predicate(position))
                    yield return position;
            }
        }
    }
}