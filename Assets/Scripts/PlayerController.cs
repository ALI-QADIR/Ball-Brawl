using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // public variables
    [Tooltip("Speed at which Player Moves")]
    public float speed = 1.0f;

    // private variables
    private Rigidbody playerRB;
    private GameObject focalPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();                                               // get the rigidbody component
        focalPoint = GameObject.Find("FocalPoint");                                         // find the game object with the name "FocalPoint"
    }

    // Update is called once per frame
    void Update()
    {
        // Get the vertical input and move the player forward with respect to the camera's focal point
        float forwardInput = Input.GetAxis("Vertical");
        playerRB.AddForce(forwardInput * speed * focalPoint.transform.forward);
    }
}
