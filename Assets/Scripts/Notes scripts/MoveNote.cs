using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNote : MonoBehaviour
{
    private Vector3 mOffset;

    private float mZCoord;

    private GameObject currPlace = null;
    private bool isAttached = false;
    private bool isOut = false;

    public float speed = 1.0f;

    private void Update()
    {
        float step = speed * Time.deltaTime;

        if (isAttached)
        {
            Vector3 target = currPlace.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, target.y, target.z), step);

            if (Math.Abs(transform.position.z - currPlace.transform.position.z) < 0.001f 
                && Math.Abs(transform.position.y - currPlace.transform.position.y) < 0.001f)
            {
                // Swap the position of the cylinder.
                print("Note attached");
                FindObjectOfType<AudioManager>().Play("putNoteSound");
                //print(currPlace.transform.name[0].ToString());
                isAttached = false;
                BoardManager.Compare(gameObject, currPlace);
            }
        }
        if (isOut)
        {
            transform.position = new Vector3(15.85f, UnityEngine.Random.Range(10.6f, 20f), UnityEngine.Random.Range(-3.5f, 0f));
            isOut = false;
        }
    }

    private void OnMouseDown()
    {
        transform.position = gameObject.transform.position - new Vector3(0.1f, 0, 0);

        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        //offset = gameobj world pos = mouse world pos
        mOffset = gameObject.transform.position - GetMouseWorldPos();

        //currPlace = null;
        //isAttached = false;
        if (currPlace != null)
            BoardManager.Detach(gameObject, currPlace);

        print("Clicked");
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void OnMouseUp()
    {
        transform.position = gameObject.transform.position + new Vector3(0.1f, 0, 0f);

        if (currPlace != null)
        {
            //print("Moving");
            //isAttached = true;
            //gameObject.transform.position = new Vector3(0, 0, gameObject.transform.position.z);
            if (currPlace.tag == "place")
                isAttached = true;
            else
                isOut = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "place" || other.gameObject.tag == "border") && currPlace == null)
        {
            if (other.gameObject.tag == "place")
                print("Entered trigger (" + other.GetComponent<NotePlace>().GetColumn() + ", " + (int)other.GetComponent<NotePlace>().GetRow() + ")");
            currPlace = other.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.tag == "place" || other.gameObject.tag == "border") && currPlace == null)
        {
            currPlace = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "place")
            print("Canceled trigger (" + other.GetComponent<NotePlace>().GetColumn() + ", " + (int)other.GetComponent<NotePlace>().GetRow() + ")");
        if (currPlace == other.gameObject)
        {
            currPlace = null;
            isAttached = false;
            isOut = false;
        }
    }
}
