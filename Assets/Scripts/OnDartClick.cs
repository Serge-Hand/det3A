using UnityEngine;

public class OnDartClick : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        //Debug.Log("Darts");
        GameObject.Find("GameManager").GetComponent<GameManager>().OnSuspectedChoice();
    }
}
