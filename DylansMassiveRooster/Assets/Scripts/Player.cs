using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [NonSerialized] private bool move;

    [NonSerialized] private GameObject movePoint;
    [SerializeField] private GameObject movePointer;
    [SerializeField] private float moveSpeed = 1f;
    [NonSerialized] private readonly float timeToWait = 5f;
    
    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to move
        {
            Destroy(movePoint); // Destroy the previous movePoint before setting a new one
            movePoint = Instantiate(movePointer, mousePos,
                Quaternion.identity); // Create an object where the mouse has been clicked
            move = true; // Make movement possible
        }
        if (move) // check if movement is possible
            transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position,
                moveSpeed * Time.deltaTime); //Move towards the point clicked
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait * Time.deltaTime);
        FindObjectOfType<Destroy>().SetToNotAlive(); // Tells the x animation to destroy itself after it fades out
        move = false; // Stop Movement
    }

    private void OnTriggerEnter2D(Collider2D collider) //Check for collision
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
        {
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
            FindObjectOfType<Destroy>().GetComponent<Animator>().SetBool("pause", true);// Tells the animation to fade out and pause
        }
        if (collider.gameObject.CompareTag("Restricted")) // Check if you have collided with the movement position
        {
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
            FindObjectOfType<Destroy>().GetComponent<Animator>().SetBool("pause", true);// Tells the animation to fade out and pause
            move = false; // Stop Movement
        }
    }
}