using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public float delay = 0.1f;
    private string currentText = "";
    public GameObject load;

    private void Start()
    {
        StartCoroutine(Intro());
        FindObjectOfType<AudioManager>().Play("ambient");
    }

    IEnumerator Intro()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TypeText("\tКонрад Лерой восемь лет назад отошел от расследований преступлений. Все из-за того, что его собственная жена – Ровенна Лерой – оказалась хладнокровной убийцей. Она убила их собственного ребенка, который от рождения был умственно неполноценным, к тому же имел физические отклонения и уродства. Ему было около двух месяцев от роду, когда он безвременно покинул мир..."));
        yield return new WaitForSeconds(26f);
        StartCoroutine(TypeText("\tВ тот день Конрад не ночевал дома, был занят делом государственной важности. Когда он вернулся домой, его жена рыдала над колыбелькой, в которой лежал их бездыханный ребенок. Ровенна сказала, что ребенок умер во сне, что бог забрал его несчастную душу, чтобы он не мучился больше. Однако Конрад не мог поверить, что прожив два месяца его сын так просто взял и умер, хотя врачи не исключали проблем с сердцем у малыша. Мужчина заметил странное искаженное лицо младенца, как будто он истошно кричал пока не застыл бездыханным. В носу заметил небольшое гусиное перо. Подушка, которая лежала на их кровати была странно примята…"));
        yield return new WaitForSeconds(41f);
        StartCoroutine(TypeText("\tРовенна сразу поняла, что муж догадался, что она решила собственноручно избавиться от своего блемени. Она умоляла его простить ее и никому не сообщать об этом. Девушка обещала, что у них будет еще много нормальных детей, что они похоронят этого ребенка по-человечески и забудут об этом, как о страшном сне. Но Конрад не мог смириться с мыслью, что его жена так спокойно говорит об убийстве собственного ребенка так, будто вещает прогноз погоды по телевизору. Что она оказалась хуже всех тех жуликов, которых он ловил…Он заявил на нее в полицию и после суда ее повесили. После этого случая он забросил расследования, потому что не мог больше вынести всех этих ужасов преступного мира, которые и не думали с каждым годом уменьшаться..."));
        yield return new WaitForSeconds(47f);
        StartCoroutine(TypeText("\tВосемь лет спустя в мир преступлений его побудило вернуться несчастье с его братом – Эдмундом Лероем. Он, его жена и ребенок попали в аварию. Родители погибли, выжил только ребенок – десятилетняя Кэтрин Лерой. После катастрофы у девочки парализовало ноги и за ней требуется особый уход. А для этого нужны большие деньги. Конрад удочерил Кэтрин, так как глядя на нее он вспоминал своего несчастного ребенка. Для того, чтобы ухаживать за девочкой ему нужно больше денег, чем его военная пенсия. Так что Конраду нужно вернуться в дело, чтобы девочка смогла нормально жить."));
        yield return new WaitForSeconds(37f);
        FindObjectOfType<AudioManager>().SetVolume("typingSound", 0f);
        FindObjectOfType<AudioManager>().StartFade("ambient", "typingSound", 1f, 0f);
        yield return new WaitForSeconds(2f);
        load.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("room");
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
}
