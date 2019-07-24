using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized] private readonly float timeToWait = 10f;
    [NonSerialized] private bool move;

    [NonSerialized] private GameObject movePoint;
    [SerializeField] private GameObject movePointer;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float reboundVelocity = 50f;

    private void Update()
    {
        Movement();
    }

    // Player Movement

    private void Movement()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to move
        {
            Destroy(movePoint); // Destroy the previous movePoint before setting a new one
            movePoint = Instantiate(movePointer, mousePos,
                Quaternion.identity); // Create an object where the mouse has been clicked
            move = true; // Make movement possible
        }

        if (move) // check if movement is possible
        {
            GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(transform.position, movePoint.transform.position,
                moveSpeed * Time.deltaTime); //Move towards the point clicked
        }
        else //remove the move point
        {
            FindObjectOfType<Destroy>().GetComponent<Animator>()
                .SetBool("pause", true); // Tells the animation to fade out and pause
            FindObjectOfType<Destroy>().SetToNotAlive(); // Tells the x animation to destroy itself after it fades out
        }
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait * Time.deltaTime);
        move = false; // Stop Movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collider) //Check for collision
    {
        if (collider.gameObject.CompareTag("Restricted")) // Check if you have collided with the movement position
        {
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
            move = false; // Stop Movement
            var dir = collider.contacts[0].point -
                      new Vector2(transform.position.x, transform.position.y); //get the direction of the collision
            dir = -dir.normalized; // normalize direction and reverse it
            GetComponent<Rigidbody2D>().AddForce(dir * reboundVelocity); // move player away from collision
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // stop player movement
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) //Check for collision trigger
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
        {
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
        }
    }

    private void OnCollisionStay2D(Collision2D collider) //Check if colliders are stuck on each other
    {
        if (collider.gameObject.CompareTag("Restricted")) // Check if player is stuck on restricted collider
        {
            var dir = collider.contacts[0].point -
                      new Vector2(transform.position.x, transform.position.y); //get the direction of the collision
            dir = -dir.normalized; // normalize direction and reverse it
            GetComponent<Rigidbody2D>().AddForce(dir * reboundVelocity); // move player away from collision
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // stop player movement
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
        }
    }
}