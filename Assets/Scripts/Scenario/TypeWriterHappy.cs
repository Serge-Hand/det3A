using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriterHappy : MonoBehaviour
{
    public float delay = 0.1f;
    private string currentText = "";
    public GameObject exitButton;

    private void Start()
    {
        StartCoroutine(Ending());
        //FindObjectOfType<AudioManager>().Play("ambient");
    }

    IEnumerator Ending()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypeText("\tИз лечебницы Сэинт Крит пришла прекрасная новость! Лечение Кэтрин дало ощутимый результат! Девочка смогла встать с инвалидного кресла самостоятельно на несколько мгновений без костылей и помощи врачей! Это чудо, ведь ей грозила незавидная участь быть прикованной на всю жизнь к инвалидному креслу. Благодаря стараниям Конрада Лероя у нее появился шанс на вновь полноценную жизнь. Все самое страшное позади, но еще много чего им придется преодолеть на своем пути к полному выздоровлению Кэтрин."));
        yield return new WaitForSeconds(30f);
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
