using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PointProviderFromList : IPositionGenerator
    {
        private List<Vector3> positions = new List<Vector3>();

        public PointProviderFromList(Vector3 fixedPosition)
        {
            positions.Add(fixedPosition);
        }

        public PointProviderFromList(IEnumerable<Vector3> listOfPositions)
        {
            foreach(var position in positions)
            {
                positions.Add(position);
            }    
        }

        public IList<Vector3> GetNext()
        {
            return positions;
        }
    }
}
