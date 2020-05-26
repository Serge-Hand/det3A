using UnityEngine;

public class OnPhotoClickDetect : MonoBehaviour
{
    ChoiceScript handler;
    int podozr;
    bool clickable = true;

    public void SetHandler(ChoiceScript h, int num)
    {
        handler = h;
        podozr = num;
    }

    public void ClickPossibilityOff()
    {
        clickable = false;
    }

    void OnMouseUpAsButton()
    {
        if (clickable)
        {
            handler.OnPhotoClick(podozr);
        }
    }
}
