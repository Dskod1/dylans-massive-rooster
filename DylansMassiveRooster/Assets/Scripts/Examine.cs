using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Examine : MonoBehaviour
{
    private void OnMouseOver()
    {
        GetComponent<Animator>()
            .SetBool("mouseHovering", true);
        FindObjectOfType<CursorController>().changeToCursorExamine();
    }

    private void OnMouseExit()
    {
        GetComponent<Animator>()
            .SetBool("mouseHovering", false);
        FindObjectOfType<CursorController>().changeToCursorWalking();
    }
}
