using UnityEngine;

public class OnPhotoClickDetect : MonoBehaviour
{
    ChoiceScript handler;
    int podozr;

    public void SetHandler(ChoiceScript h, int num)
    {
        handler = h;
        podozr = num;
    }

    void OnMouseUpAsButton()
    {
        handler.OnPhotoClick(podozr);
    }
}
