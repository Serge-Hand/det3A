using UnityEngine;

public class OnDartClick : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        //Debug.Log("Darts");
        GameObject.Find("GameManager").GetComponent<GameManager>().OnSuspectedChoice();
    }
}
