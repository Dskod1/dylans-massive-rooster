using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveArea : MonoBehaviour
{
    private void OnMouseDown()
    {
        FindObjectOfType<Player>().Movement();
    }
}
