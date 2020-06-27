using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyScript : MonoBehaviour
{
    static GameObject instance;
    static int a = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(this);
            Debug.Log(name + "Awake" + a);
            a++;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        Debug.Log(name + "Start");
    }

    void Update()
    {
        
    }
}
