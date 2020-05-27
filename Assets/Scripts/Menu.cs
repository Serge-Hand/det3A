using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("menuMusic");
    }
    public void PlayPressed()
    {
        SceneManager.LoadScene("intro");
    }
    public void QuitPressed()
    {
        Application.Quit();
    }
}
