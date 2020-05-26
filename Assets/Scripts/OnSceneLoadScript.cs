using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoadScript : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene s, LoadSceneMode sm)
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.NewCaseStart();
        Destroy(gameObject);
    }
}
