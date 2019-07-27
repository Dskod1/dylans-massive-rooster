using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized] private readonly float timeToWait = 1f;
    [NonSerialized] private bool move;
    [NonSerialized] private float dirX;
    [NonSerialized] private float dirY;

    [NonSerialized] private GameObject movePoint;
    [SerializeField] private GameObject movePointer;
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D RB2D;
    [SerializeField] private float reboundVelocity = 50f;
    
    

    private void Start()
    {
        RB2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        Debug.Log(RB2D.velocity);
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
            RB2D.position = Vector2.MoveTowards(transform.position, movePoint.transform.position,
                moveSpeed * Time.deltaTime); //Move towards the point clicked
            WalkAnimationOn();
        }
        else
        {
            WalkAnimationOff();
            if (FindObjectOfType<Destroy>() != null) //Checks if the move point still exsists
            {
                FindObjectOfType<Destroy>().GetComponent<Animator>()
                    .SetBool("pause", true); // Tells the animation to fade out and pause
                FindObjectOfType<Destroy>()
                    .SetToNotAlive(); // Tells the x animation to destroy itself after it fades out
            }

            //Stop movement of character if move = false
            RB2D.velocity = new Vector2(0, 0); // stop player movement
        }
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait * Time.deltaTime);
        move = false; // Stop Movement;
    }

    private void OnCollisionEnter2D(Collision2D collider) //Check for collision
    {
        if (collider.gameObject.CompareTag("Restricted")) // Check if you have collided with the movement position
        {
            var dir = collider.contacts[0].point -
                      new Vector2(transform.position.x, transform.position.y); //get the direction of the collision
            dir = -dir.normalized; // normalize direction and reverse it
            RB2D.AddForce(dir * reboundVelocity); // move player away from collision
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) //Check for collision trigger
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
            move = false; // Stop Movement
    }

    private void OnCollisionStay2D(Collision2D collider) //Check if colliders are stuck on each other and correct
    {
        if (collider.gameObject.CompareTag("Restricted")) // Check if player is stuck on restricted collider
        {
            var dir = collider.contacts[0].point -
                      new Vector2(transform.position.x, transform.position.y); //get the direction of the collision
            dir = -dir.normalized; // normalize direction and reverse it
            RB2D.AddForce(dir * reboundVelocity); // move player away from collision
        }
    }

    // Player Animation
    // Animator Int "direction" 1 = up left; 2 = up right; 3 = down right; 4 = down left
    // Animator bool "walk" 

    private void WalkAnimationOn()
    {
        GetComponent<Animator>().SetBool("walk", true);
        dirX = transform.position.x - movePoint.transform.position.x;
        dirY = transform.position.y - movePoint.transform.position.y;

        if (dirY < -1)
        {
            if (dirX < 0)
            {
                GetComponent<Animator>().SetInteger("direction", 1);
            }
            else
            {
                GetComponent<Animator>().SetInteger("direction", 2);
            }
        }
        else
        {
            if (dirX < 0)
            {
                GetComponent<Animator>().SetInteger("direction", 3);
            }
            else
            {
                GetComponent<Animator>().SetInteger("direction", 4);
            }
        }
    }
    private void WalkAnimationOff()
    {
        GetComponent<Animator>().SetBool("walk", false);
    }

}