using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCamera : MonoBehaviour
{
    private bool isTurning = false;
    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            if (!isTurning)
            {
                if (Input.GetKeyDown("space"))
                {
                    StartCoroutine(Turn());
                }
            }
        }
    }

    IEnumerator Turn()
    {
        isTurning = true;
        GetComponent<Animator>().SetTrigger("Turn");
        yield return new WaitForSeconds(1.0f);
        isTurning = false;
    }
}
