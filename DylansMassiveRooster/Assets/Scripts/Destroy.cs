using System;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    [NonSerialized] public bool alive = true;

    public void DestroyObject()
    {
        if (alive == false)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetToNotAlive()
    {
        alive = false;
    }
}
