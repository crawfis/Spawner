using UnityEngine;

namespace CrawfisSoftware.PointProvider
{
    public class PositionProviderBuilder : MonoBehaviour
    {
        private IPositionGenerator positionGenerator;

        public IPositionGenerator PositionGenerator
        {
            get
            {
                if (positionGenerator == null)
                    CreatePositionGenerator();
                return positionGenerator;
            }
        }

        protected virtual void CreatePositionGenerator()
        {
            positionGenerator = null;
        }
    }
}