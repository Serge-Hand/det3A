using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastToMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Pressed");
            //Ray a = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
            
            //Debug.DrawRay(a.origin, a.direction);
            //Debug.Break();

            RaycastHit hit;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)), out hit))
            {
                Debug.Log(hit.transform.gameObject.name + "ERRRR");
            }
            /*
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)), out hit))
            {
                Debug.Log(hit.transform.gameObject.name);
            }
            */
        }
    }
}
