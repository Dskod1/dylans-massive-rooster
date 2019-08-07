using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Debug Only - DO NOT SAVE CHANGES TO THIS IN THE EDITOR")][SerializeField] public bool alive = true;

    public void DestroyObject()
    {
        if (alive == false) Destroy(gameObject);
    }

    public void SetToNotAlive()
    {
        alive = false;
    }
}