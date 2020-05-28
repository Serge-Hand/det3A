using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] Texture2D cursor;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("menuMusic");
        UnityEngine.Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void PlayPressed()
    {
        Cursor.visible = false;
        SceneManager.LoadScene("intro");
    }
    public void QuitPressed()
    {
        Application.Quit();
    }
}
