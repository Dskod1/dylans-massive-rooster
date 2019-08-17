using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Camera;

public class Item : MonoBehaviour
{
    [Header("Editor")]
    [SerializeField]private float timeToWaitForTextFeedback = 100f;

    [SerializeField] private float newItemSizeForInventory = 0.5f;
    [Header("Debug Only - DO NOT SAVE CHANGES TO THIS IN THE EDITOR")]
    [SerializeField]private bool itemPickedUp = false;
    [SerializeField] private int totalInventoryCount = 0;
    [SerializeField] private GameObject inventoryPickedUpItemsObject;
    [SerializeField] private GameObject pickedUpItemText;
    [SerializeField] private bool activated = false;
    [SerializeField] private Vector3 originalInventoryPosition;


    private void Start()
    {
        inventoryPickedUpItemsObject = GameObject.Find("Picked Up Items"); //At start of scene fine the piced up items object for use when picking up things.
        pickedUpItemText = GameObject.Find("Picked Up Item Text"); ; // At start of scene find the text to be used later
    }

    private void Update()
    {
        if (activated == true)
        {
            Vector3 currentMousePosition = Input.mousePosition; // get current mouse position
            currentMousePosition.z = 10f; // Ensure the object remains on top.
            transform.position = Camera.main.ScreenToWorldPoint(currentMousePosition); //move object together with the mouse

        }
    }

    private void OnMouseOver() //Activates if mouse is over object
    {
        if (itemPickedUp != true)
        {
            FindObjectOfType<CursorController>().walkingMode = false; // Tell player he is no longer in walking mode
            GetComponent<Animator>()
                .SetBool("mouseHovering", true); //  sets the hover animation up
            FindObjectOfType<CursorController>().changeToCursorTake(); // changes cursor to relevant one
            if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to activate object
            {
                FindObjectOfType<Player>().selectedItemPosition = new Vector2(transform.position.x, transform.position.y);
                FindObjectOfType<Player>().activateItem = true;
                FindObjectOfType<Player>().MoveToSelectedItem();
            }
        }
        
        else if (itemPickedUp == true) // checks if item has already been picked up
        {
            FindObjectOfType<CursorController>().walkingMode = false; // Tell player he is no longer in walking mode
            GetComponent<Animator>()
                .SetBool("mouseHovering", true); //  sets the hover animation up
            FindObjectOfType<CursorController>().changeToCursorTake(); // changes cursor to relevant one
            if (activated == false) //make sure the inventory stays down while the item isnt in use
            {
                FindObjectOfType<Inventory>().inventoryDown = true; 
                FindObjectOfType<Inventory>().targetPosition = FindObjectOfType<Inventory>().endingDownInventoryPosition;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to activate object
            {
                if (activated == false) // check if item is not already activated
                {
                    FindObjectOfType<Inventory>().inventoryDown = false; //inventory free to go back up
                    FindObjectOfType<Inventory>().targetPosition = FindObjectOfType<Inventory>().startingInventoryPosition;
                    originalInventoryPosition = transform.position; // record orignal position of the item
                    activated = true; // activate item
                }
                else //if already activated then
                {
                    activated = false; //deactivate item
                    transform.position = originalInventoryPosition; // move item back to original position
                }
            }
        }

    }

    public void PickUpObject()
    {
        GetComponent<BoxCollider2D>().enabled = true; // Enables the box collider. Important for item interaction in the inventory.
        itemPickedUp = true;  // sets the item as picked up
        totalInventoryCount += 1; // Sets the total amount of items in inventory
        GameObject pickedUpItem = Instantiate(gameObject, 
            new Vector3(inventoryPickedUpItemsObject.transform.position.x,inventoryPickedUpItemsObject.transform.position.y, -1), 
            Quaternion.identity, inventoryPickedUpItemsObject.transform); //Create a copy of the item in the picked up items object
        pickedUpItem.transform.localScale = new Vector3(newItemSizeForInventory, newItemSizeForInventory ); // Scale the new item so its the right size for the inventory
        FindObjectOfType<Inventory>().inventoryList.Add(pickedUpItem); // Add the item the inventory list
        Destroy(gameObject.transform.GetChild(0).gameObject); // Destroys the sprite of the object to make it dissapear
        pickedUpItemText.GetComponent<Text>().color = new Color(255,255,255, 255); // Change the transparency of the text to show to the player
        pickedUpItemText.GetComponent<Text>().text = "Picked up " + gameObject.name; // Change the text to match the item name
        StartCoroutine(WaitForExtraTime()); // Wait a little before removing text and fully deleting the original item
    }

    private IEnumerator WaitForExtraTime()
    {
        yield return new WaitForSeconds(timeToWaitForTextFeedback * Time.deltaTime);
        pickedUpItemText.GetComponent<Text>().color = new Color(255, 255, 255, 0); //set the picked up item text to be transparent
        Destroy(gameObject); // Destroy the original object of the picked up item
    }

    private void OnMouseExit()
    {
        if (activated == false)
        {
            FindObjectOfType<CursorController>().walkingMode = true; // Tell player he is in walking mode
            GetComponent<Animator>()
                .SetBool("mouseHovering", false); // end hover animation
            FindObjectOfType<CursorController>().changeToCursorWalking(); // change cursor to default
        }
    }
}
