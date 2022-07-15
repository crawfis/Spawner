using UnityEngine;
using UnityEngine.InputSystem;

namespace CrawfisSoftware.Spawner
{
    public class MovementInputHandler : MonoBehaviour
    {
        private Vector2 movementVector;
        [SerializeField] float speed = 10;
        [SerializeField] Rigidbody rigidBodyToMove;

        public void OnMove(InputValue movementValue)
        {
            movementVector = movementValue.Get<Vector2>();
        }    

        private void FixedUpdate()
        {
            Vector3 movement = new Vector3(movementVector.x, 0, movementVector.y);
            rigidBodyToMove.AddForce(movement * speed);
        }
    }
}