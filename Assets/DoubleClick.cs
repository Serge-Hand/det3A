using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleClick : MonoBehaviour
{
    float doubleClickStart = 0; 
    void OnMouseUp() 
    { 
        if ((Time.time - doubleClickStart) < 0.3f) 
        {
            Debug.Log("Double Clicked!");
            GetComponent<Zoom>().DoubleClick();
            doubleClickStart = -1; 
        } 
        else 
        { 
            doubleClickStart = Time.time; 
        } 
    }
}
