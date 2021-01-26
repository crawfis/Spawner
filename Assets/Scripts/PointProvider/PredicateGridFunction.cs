using System;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PredicateGridFunction
    {
        private readonly Func<Vector3, Vector2Int> positionToGridMap;
        private readonly Func<int, int, bool> gridPredicate;

        public PredicateGridFunction(Func<Vector3, Vector2Int> positionToGridMap, Func<int,int, bool> gridPredicate=null)
        {
            this.positionToGridMap = positionToGridMap;
            this.gridPredicate = gridPredicate;
        }
        public bool ValidPoint(Vector3 position)
        {
            if (gridPredicate == null) return true;
            Vector2Int gridIndex = positionToGridMap(position);
            if (gridPredicate(gridIndex.x, gridIndex.y))
                return true;
            return false;
        }
    }
}