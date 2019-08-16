using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [Header("Editor")]
    [SerializeField] public Texture2D cursorWalking;
    [SerializeField] public Texture2D cursorExamine;
    [SerializeField] public Texture2D cursorUse;
    [SerializeField] public Texture2D cursorTake;
    [SerializeField] public Texture2D cursorTalkTo;
    [Header("Debug Only - DO NOT SAVE CHANGES TO THIS IN THE EDITOR")]
    [SerializeField] public bool walkingMode = true;
    [SerializeField] public bool defaultMode = true;

    
    // Different states the cursor can be put in by hovering over objects or manually ordering it.
    public void changeToCursorWalking()
    {
        Cursor.SetCursor(cursorWalking, Vector2.zero, CursorMode.Auto);
    }

    public void changeToCursorExamine()
    {
        Cursor.SetCursor(cursorExamine, Vector2.zero, CursorMode.Auto);
    }

    public void changeToCursorUse()
    {
        Cursor.SetCursor(cursorUse, Vector2.zero, CursorMode.Auto);
    }

    public void changeToCursorTake()
    {
        Cursor.SetCursor(cursorTake, Vector2.zero, CursorMode.Auto);
    }

    public void changeToCursorTalkTo()
    {
        Cursor.SetCursor(cursorTalkTo, Vector2.zero, CursorMode.Auto);
    }
}