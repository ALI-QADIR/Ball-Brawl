using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // public variables
    [Tooltip("Speed at which Enemy Moves")]
    public float speed = 1.0f;

    // private variables
    private Rigidbody enemyRB;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();                                                        // get the rigidbody component
        player = GameObject.Find("Player");                                                         // find the game object with the name "Player"
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy towards the player
        // player.transform.position - transform.position calculates the vector from the enemy's
        // positions cooridnates and player's position coordinates
        // .normalized converts the vector to a unit vector
        // enemy.AddForce adds a force to the enemy in the direction of the unit vector
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;         // get the direction to the player
        enemyRB.AddForce(lookDirection * speed);                                                     // apply the force to the enemy
    }
}
