using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class AutoDestructionIfFalling : MonoBehaviour
    {
        private const float yThreshold = -5;

        // Todo: Add Awake and use a manager (separate script / component?)
        void Update()
        {
            // Todo: Wrap in a coroutine and check every 0.5 second or so.
            if (transform.position.y < yThreshold)
                // Todo: Fire an event or use a manager
                Destroy(this.gameObject);
        }
    }
}
