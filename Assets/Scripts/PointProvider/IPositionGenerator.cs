using System.Collections.Generic;

namespace CrawfisSoftware.PointProvider
{
    public interface IPositionGenerator
    {
        IList<UnityEngine.Vector3> GetNext();
    }
}
