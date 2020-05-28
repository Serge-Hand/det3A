using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    private Texture2D cursorArrow;
    private Texture2D cursorClick;

    void Start()
    {
        Sprite cursor = Resources.Load<Sprite>("Sprites/cursorSmall");
        Sprite click = Resources.Load<Sprite>("Sprites/clickSmall");

        cursorArrow = cursor.texture;
        cursorClick = click.texture;
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorClick, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

}
