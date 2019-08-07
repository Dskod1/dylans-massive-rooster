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
    [SerializeField]private Vector2 endingDownInventoryPosition;
    [SerializeField]private Vector2 targetPosition;
    [SerializeField] private bool inventoryDown = false;
    [SerializeField] private GameObject inventoryPickedUpItemsObject;

    private void Start()
    {
        startingInventoryPosition = transform.position;
        endingDownInventoryPosition = new Vector2(startingInventoryPosition.x, startingInventoryPosition.y - gameObject.GetComponent<BoxCollider2D>().size.y + inventoryDropDownOffset);
        targetPosition = startingInventoryPosition;
        inventoryPickedUpItemsObject = GameObject.Find("Picked Up Items");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, inventoryMoveSpeed);
        
    }

    private void OnMouseOver() //Activates if mouse is over object
    {
        if (inventoryDown != true)
        {
            inventoryDown = true;
            targetPosition = endingDownInventoryPosition;
        }
        
    }

    private void OnMouseExit()
    {
        inventoryDown = false;
        targetPosition = startingInventoryPosition;
    }
}
