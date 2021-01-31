using System;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PredicateGridFunction
    {
        private readonly Func<Vector3, Vector2Int> positionToGridMap;
        private readonly Func<Vector2Int, bool> gridPredicate = AlwaysTrue;

        public PredicateGridFunction(Func<Vector3, Vector2Int> positionToGridMap, Func<Vector2Int, bool> gridPredicate=null)
        {
            this.positionToGridMap = positionToGridMap;
            if (gridPredicate != null)
                this.gridPredicate = gridPredicate;
        }
        public bool ValidPoint(Vector3 position)
        {
            Vector2Int gridIndex = positionToGridMap(position);
            if (gridPredicate(gridIndex))
                return true;
            return false;
        }
        private static bool AlwaysTrue(Vector2Int index)
        {
            return true;
        }
    }
}