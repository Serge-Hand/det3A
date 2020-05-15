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
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
            FindObjectOfType<AudioManager>().Play("waveSound");
        if (name.Equals("Turn"))
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamBoard") ||
                GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamTable"))
            {
                FindObjectOfType<AudioManager>().Play("waveSound");
                yield return new WaitForSeconds(0.75f);
            }
        if (name.Equals("TurnPaper"))
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamPaper") ||
                GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("CamBack"))
            {
                FindObjectOfType<AudioManager>().Play("waveSound");
                yield return new WaitForSeconds(0.75f);
            }
        GetComponent<Animator>().ResetTrigger("Turn");
        GetComponent<Animator>().ResetTrigger("TurnPaper");
        isTurning = false;
    }
}
