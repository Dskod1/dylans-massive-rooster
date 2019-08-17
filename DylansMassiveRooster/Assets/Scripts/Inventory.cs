using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [Header("Editor")] [Range(0, 10)][SerializeField] private float inventoryMoveSpeed = 1.0f;
    [Range(0, 10)][SerializeField] private float inventoryDropDownOffset = 2f;
    [Header("Debug Only - DO NOT SAVE CHANGES TO THIS IN THE EDITOR")]
    [SerializeField]public List<GameObject> inventoryList = new List<GameObject>();
    [SerializeField]private Vector2 startingInventoryPosition;
    [SerializeField] public Vector2 endingDownInventoryPosition;
    [SerializeField] public Vector2 targetPosition;
    [SerializeField] public bool inventoryDown = false;
    [SerializeField] private GameObject inventoryPickedUpItemsObject;

    private void Start()
    {
        startingInventoryPosition = transform.position; // Figure out the where the inventory is placed at start of scene
        //Calculate the position the inventory has to be in to be fully down
        endingDownInventoryPosition = new Vector2(startingInventoryPosition.x, 
            startingInventoryPosition.y - 
            gameObject.GetComponent<BoxCollider2D>().size.y + 
            inventoryDropDownOffset); 
       // Set the position the inventory has to be in to the starting position
        targetPosition = startingInventoryPosition;
        inventoryPickedUpItemsObject = GameObject.Find("Picked Up Items");
    }

    private void Update()
    {
        //Always move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, inventoryMoveSpeed);
        
    }

    private void OnMouseOver() //Activates if mouse is over object
    {
        //If the inventory is not down, it will set the inventory down to true and set the target position position to the calculated ending down position
        if (inventoryDown != true)
        {
            inventoryDown = true;
            targetPosition = endingDownInventoryPosition;
        }
        
    }

    private void OnMouseExit()
    {
        //Set inventory down to false and the target position back to the starting postion
        inventoryDown = false;
        targetPosition = startingInventoryPosition;
    }
}
