using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    GameObject pausePanel;
    GameManager c_gameMan;
    bool onPause;
    GameObject mainIsolatorCube;
    BoxCollider isolCube;

    void Start()
    {
        c_gameMan = GameObject.Find("GameManager").GetComponent<GameManager>();

        pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);
        onPause = false;

        mainIsolatorCube = GameObject.Find("MainIsolatorCube");
        isolCube = mainIsolatorCube.GetComponent<BoxCollider>();
        isolCube.enabled = false;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(onPause)
            {
                OnContinueButtonClick();
            }
            else
            {
                onPause = true;
                pausePanel.SetActive(true);
                c_gameMan.StopTimerAndSaveTime();
                isolCube.enabled = true;
            }
        }
    }

    public void OnContinueButtonClick()
    {
        onPause = false;
        pausePanel.SetActive(false);
        c_gameMan.StartTimerWithCurrenTime();
        isolCube.enabled = false;
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("menu");
    }
}
