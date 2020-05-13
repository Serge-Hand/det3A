using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    static public int count = 0;
    public int hand = -1;
    public bool isZoomed = false;
    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void ZoomIn()
    {
        if (count == 0)
        {
            transform.position = new Vector3(6.7f, 7.9f, 7.9f);
            hand = 0;
        }
        else
        {
            Zoom[] tmp = FindObjectsOfType<Zoom>();
            Zoom t = null;
            foreach(Zoom z in tmp)
            {
                if (z.isZoomed)
                {
                    t = z;
                    break;
                }
            }
            if (t.hand == 0)
            {
                transform.position = new Vector3(4.5f, 7.9f, 7.9f);
                hand = 1;
            }
            else
            {
                transform.position = new Vector3(6.7f, 7.9f, 7.9f);
                hand = 0;
            }
        }
        transform.rotation = Quaternion.Euler(-20, 180, 0);
        isZoomed = true;
        count++;
    }

    private void ZoomOut()
    {
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(0, 180, 0);
        isZoomed = false;
        hand = -1;
        count--;
    }

    public void DoubleClick()
    {
        if (!isZoomed && count < 2)
        {
            ZoomIn();
            return;
        }
        if (isZoomed)
        {
            ZoomOut();
        }
    }
}
