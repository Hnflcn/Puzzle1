using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Move : MonoBehaviour
{
    protected Vector3 objPos;
        
    protected void Moving()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = 9f;
        objPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = objPos;
    }
}
