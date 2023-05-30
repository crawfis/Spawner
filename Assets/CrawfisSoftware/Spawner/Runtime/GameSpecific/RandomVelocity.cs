using UnityEngine;

namespace CrawfisSoftware.Spawner
{
    public class RandomVelocity : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;

        void Start()
        {
            Rigidbody rigidBody = gameObject.GetComponent<Rigidbody>();
            Vector2 direction = Random.insideUnitCircle.normalized;
            rigidBody.velocity = speed * new Vector3(direction.x, 0, direction.y);
        }
    }
}
