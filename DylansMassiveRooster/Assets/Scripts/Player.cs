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
        if (move) // check if movement is possible
            transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position,
                moveSpeed * Time.deltaTime); //Move towards the point clicked
    }

    public void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to move
        {
            Destroy(movePoint); // Destroy the previous movePoint before setting a new one
            movePoint = Instantiate(movePointer, Camera.main.ScreenToWorldPoint(Input.mousePosition),
                Quaternion.identity); // Create an object where the mouse has been clicked
            move = true; // Make movement possible
        }
    }

    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait * Time.deltaTime);
        Destroy(movePoint); // Destroy the movePoint
        move = false; // Stop Movement
    }

    private void OnTriggerEnter2D(Collider2D collider) //Check for collision
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
    }
}