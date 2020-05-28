using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriterEnding : MonoBehaviour
{
    public float delay = 0.1f;
    private string currentText = "";
    public GameObject exitButton;

    private void Start()
    {
        StartCoroutine(Ending());
        FindObjectOfType<AudioManager>().Play("ambient");
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypeText("\tИз-за отсутствия денег Конрада Лероя вышвыривают из съемной квартиры. Он больше не может оплачивать лечение Кэтрин и через несколько месяцев она умирает. Не в силах вынести гибель близкого ему человека и позора, Конрад Лерой покончил с собой выстрелом в голову из револьвера..."));
        yield return new WaitForSeconds(17f);
        FindObjectOfType<AudioManager>().SetVolume("typingSound", 0f);
        FindObjectOfType<AudioManager>().StartFade("ambient", "typingSound", 1f, 0f);
        yield return new WaitForSeconds(2f);
        exitButton.SetActive(true);
        yield return new WaitForSeconds(2f);
        Cursor.visible = true;
    }

    IEnumerator TypeText(string fullText)
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            GetComponent<TextMeshProUGUI>().text = currentText;
            FindObjectOfType<AudioManager>().Play("typingSound");
            yield return new WaitForSeconds(delay);
            if (currentText.EndsWith("."))
                yield return new WaitForSeconds(delay + 0.2f);
        }
    }

    public void OnExitButtonClick()
    {
        SceneManager.LoadScene("menu");
    }
}
