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
                    StartCoroutine(Turn("Turn"));
                }
                if (Input.GetKeyDown("f"))
                {
                    StartCoroutine(Turn("TurnPaper"));
                }
            }
        }
    }

    IEnumerator Turn(string name)
    {
        isTurning = true;
        GetComponent<Animator>().SetTrigger(name);
        yield return new WaitForSeconds(0.01f);
        if (name.Equals("Turn"))
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamBoard") ||
                GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamTable"))
                yield return new WaitForSeconds(0.9f);
        if (name.Equals("TurnPaper"))
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamPaper") ||
                GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamBack"))
                yield return new WaitForSeconds(0.75f);
        GetComponent<Animator>().ResetTrigger("Turn");
        GetComponent<Animator>().ResetTrigger("TurnPaper");
        isTurning = false;
    }
}
