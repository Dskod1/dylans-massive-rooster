using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] private GameObject movePointer;
    
    [System.NonSerialized] private GameObject movePoint;
    [System.NonSerialized] private bool move = false;

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pointer")) // Check if you have collided with the movement position
        {
            Destroy(movePoint); // Destroy the movePoint
            move = false;// Stop Movement
        }
    }
}
