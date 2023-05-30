using System.Collections.Generic;
using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PointGeneratorCompleteList : IPositionGenerator
    {
        protected List<Vector3> positions = new List<Vector3>();

        public PointGeneratorCompleteList(Vector3 fixedPosition)
        {
            positions.Add(fixedPosition);
        }

        public PointGeneratorCompleteList(IEnumerable<Vector3> listOfPositions)
        {
            foreach(var position in listOfPositions)
            {
                positions.Add(position);
            }    
        }

        public virtual IList<Vector3> GetNextSet()
        {
            return positions;
        }
    }
}
