using System;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] public Texture2D cursorWalking;
    [SerializeField] public Texture2D cursorExamine;
    [SerializeField] public Texture2D cursorUse;
    [SerializeField] public Texture2D cursorTake;
    [SerializeField] public Texture2D cursorTalkTo;
    
    [NonSerialized] public bool walkingMode = true;
    [NonSerialized] public bool defaultMode = true;

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