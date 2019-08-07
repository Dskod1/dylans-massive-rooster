using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [Header("Editor")]
    [SerializeField]private float timeToWaitForTextFeedback = 100f;

    [SerializeField] private float newItemSizeForInventory = 0.5f;
    [Header("Debug Only - DO NOT SAVE CHANGES TO THIS IN THE EDITOR")]
    [SerializeField]private bool itemPickedUp = false;
    [SerializeField] private int totalInventoryCount = 0;
    [SerializeField] private GameObject inventoryPickedUpItemsObject;
    [SerializeField] private GameObject inventoryObject;

    private void Start()
    {
        inventoryPickedUpItemsObject = GameObject.Find("Picked Up Items");
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

    }

    public void PickUpObject()
    {
        itemPickedUp = true;
        totalInventoryCount += 1;
        GameObject pickedUpItem = Instantiate(gameObject, new Vector3(inventoryPickedUpItemsObject.transform.position.x, inventoryPickedUpItemsObject.transform.position.y, -1), Quaternion.identity, inventoryPickedUpItemsObject.transform);
        pickedUpItem.transform.localScale = new Vector3(newItemSizeForInventory, newItemSizeForInventory );
        FindObjectOfType<Inventory>().inventoryList.Add(pickedUpItem);
        Destroy(gameObject.transform.GetChild(0).gameObject);
        GameObject pickedUpItemText = GameObject.Find("Picked Up Item Text");
        pickedUpItemText.GetComponent<Text>().color = new Color(255,255,255, 255);
        pickedUpItemText.GetComponent<Text>().text = "Picked up " + gameObject.name;
        StartCoroutine(WaitForExtraTime()); // Wait a little before removing text
    }

    private IEnumerator WaitForExtraTime()
    {
        yield return new WaitForSeconds(timeToWaitForTextFeedback * Time.deltaTime);
        GameObject pickedUpItemText = GameObject.Find("Picked Up Item Text");
        pickedUpItemText.GetComponent<Text>().color = new Color(255, 255, 255, 0);
        Destroy(gameObject);
    }

    private void OnMouseExit()
    {
        if (itemPickedUp != true)
        {
            FindObjectOfType<CursorController>().walkingMode = true; // Tell player he is in walking mode
            GetComponent<Animator>()
                .SetBool("mouseHovering", false); // end hover animation
            FindObjectOfType<CursorController>().changeToCursorWalking(); // change cursor to default
        }
    }
}
