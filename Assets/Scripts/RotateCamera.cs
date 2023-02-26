using UnityEngine;

namespace Assets.Scripts
{
    public class RotateCamera : MonoBehaviour
    {
        [Tooltip("Speed at which Camera Rotates")]
        public float Speed = 1.0f;

        // Update is called once per frame
        private void Update()
        {
            // Get the horizontal input and rotate the camera. The minus sign before speed rotates the camera in the direction of the key pressed.
            var horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up, horizontalInput * -Speed * Time.deltaTime);
        }
    }
}
