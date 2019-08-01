using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private float timeToWaitForTextFeedback = 100f;
    private void OnMouseOver() //Activates if mouse is over object
    {
        FindObjectOfType<Player>().walkingMode = false; // Tell player he is no longer in walking mode
        GetComponent<Animator>()
            .SetBool("mouseHovering", true); //  sets the hover animation up
        FindObjectOfType<CursorController>().changeToCursorTake(); // changes cursor to relevant one
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Check if mouse has been clicked to activate object
        {
            FindObjectOfType<Player>().selectedItemX = transform.position.x;
            FindObjectOfType<Player>().MoveToSelectedItem();
        }

    }

    public void PickUpObject()
    {
        GameObject pickedUpItem = Instantiate(gameObject, new Vector3(0, 0, -50), Quaternion.identity);
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
        FindObjectOfType<Player>().walkingMode = true; // Tell player he is in walking mode
        GetComponent<Animator>() 
            .SetBool("mouseHovering", false); // end hover animation
        FindObjectOfType<CursorController>().changeToCursorWalking(); // change cursor to default
    }
}
