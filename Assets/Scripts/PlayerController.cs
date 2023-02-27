using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public variables
    [Tooltip("Speed at which Player Moves")]
    public float speed = 1.0f;

    [Tooltip("Power up indicator prefab")]
    public GameObject powerUpIndicator;

    [Tooltip("Height at which PowerUp indicator appears")]
    public float yValue;

    // private variables
    private Rigidbody _playerRb;

    private GameObject _focalPoint;
    private bool _hasPowerUp;
    private readonly float _powerUpStrength = 15.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        // Find the GameObject named "CameraFocalPoint" in the scene.
        // This is used as the point of focus for the camera around which the camera is rotated.
        _focalPoint = GameObject.Find("CameraFocalPoint");
    }

    // Update is called once per frame
    private void Update()
    {
        // Get the vertical input and move the player forward with respect to the camera's focal point
        var forwardInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(forwardInput * speed * _focalPoint.transform.forward);

        // If the player has a power-up, set the power-up indicator's position to the player's position.\
        // This ensures that the power-up indicator follows the player around.
        if (_hasPowerUp)
        {
            powerUpIndicator.transform.position = transform.position + new Vector3(0, yValue, 0);
        }
    }

    // This method is called when the player collides with a trigger collider (`other`).
    //
    // If the collider has the tag `PowerUp`, the player gains a power-up and the power-up object is destroyed.
    // The `hasPowerUp` variable is set to `true` when the player collides with a power-up.
    // The `PowerUpCountdownRoutine` method is called to start a co-routine which will set `hasPowerUp` to `false` after 7 seconds.
    // The power-up indicator is also activated.
    private void OnTriggerEnter(Component other)
    {
        if (!other.CompareTag("PowerUp")) return;
        _hasPowerUp = true;
        Destroy(other.gameObject);
        StartCoroutine(PowerUpCountdownRoutine());
        powerUpIndicator.gameObject.SetActive(true);
    }

    // This method is called when the player collides with a non-trigger collider (`collision`).
    //
    // If the collider has the tag `Enemy` and the player has a power-up, the enemy is pushed away from the player.
    private void OnCollisionEnter(Collision collision)
    {
        // If the collision happens with Enemy and the player has a power up,
        // add an Impulse to the Enemy Rigid-body (enemyRB) in the direction away from the player.
        //
        // The direction (awayFromPlayer) is a unit vector which is calculated by subtracting player's
        // 3d coordinates from collision object's 3d coordinates and then normalizing it.
        //
        // We then multiply this vector by the `powerUpStrength` variable (defined in private variables at top in this script)
        // to determine the force to apply.
        //
        // Finally, we apply the force to the enemy's rigid-body (`enemyRB`) as an Impulse.

        if (!collision.gameObject.CompareTag("Enemy") || !_hasPowerUp) return;
        var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
        var awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

        var force = awayFromPlayer * _powerUpStrength;
        enemyRb.AddForce(force, ForceMode.Impulse);
    }

    // This co-routine waits for 7 seconds and then disables the power up.
    // It is called when the player picks up a power up
    // and is used to disable the power up after 7 seconds.
    private IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        _hasPowerUp = false;

        powerUpIndicator.gameObject.SetActive(false);
    }
}