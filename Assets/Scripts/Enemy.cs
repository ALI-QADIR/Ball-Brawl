using UnityEngine;

namespace Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        // public variables
        [Tooltip("Speed at which Enemy Moves")]
        public float Speed = 1.0f;

        // private variables
        private Rigidbody _enemyRb;
        private GameObject _player;

        // Start is called before the first frame update
        private void Start()
        {
            _enemyRb = GetComponent<Rigidbody>();
            _player = GameObject.Find("Player");
        }

        // Update is called once per frame
        private void Update()
        {
            // Move the enemy towards the player by adding a force to its rigid body.
            // We calculate the direction from the enemy's position to the player's position using a normalized vector.
            // This gives us a unit vector pointing towards the player.
            // We then multiply this vector by the `speed` variable (defined in the editor) to determine the force to apply.
            // Finally, we apply the force to the enemy's rigid body (`enemyRB`).
            var lookDirection = (_player.transform.position - transform.position).normalized;
            var forceMagnitude = Speed * Time.deltaTime;
            var force = lookDirection * forceMagnitude;
            _enemyRb.AddForce(force, ForceMode.Impulse);

        }
    }
}
