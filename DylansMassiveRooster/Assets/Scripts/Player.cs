using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private GameObject movePointer;
    
    [System.NonSerialized] private GameObject movePoint;
    [System.NonSerialized] private bool move = false;
    [System.NonSerialized] private float timeToWait = 1.5f * Time.deltaTime;

    private void Start()
    {
    }

    private void Update()
    {
        Movement();

        


    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to move
        {
            Destroy(movePoint); // Destroy the previous movePoint before setting a new one
            movePoint = Instantiate(movePointer, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);// Create an object where the mouse has been clicked
            move = true;// Make movement possible
            
        }

        if (move == true)// check if movement is possible
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoint.transform.position, moveSpeed * Time.deltaTime);//Move towards the point clicked
        }
        
    }
    
    private IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(movePoint); // Destroy the movePoint
        move = false;// Stop Movement
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
        {
            StartCoroutine(
                WaitForTime()); // Wait a little before stopping movement for player to get to center of movement
        }
    }
}
