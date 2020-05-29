using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    private int tutorialStage;

    public GameObject cam;
    public GameObject subText;
    public GameObject hint;
    public GameObject time;

    private void Start()
    {
        tutorialStage = 0;
        cam.GetComponent<TurnCamera>().enabled = false;
    }

    private int count = 0;
    void Update()
    {
        switch (tutorialStage)
        {
            case 0:
                subText.SetActive(true);
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\nЗа каждое действие добавляется время";
                GameObject[] tmp = GameObject.FindGameObjectsWithTag("POICarrier");
                if (tmp.Length > 0)
                    tutorialStage++;
                break;
            case 1:
                subText.GetComponent<TextMeshProUGUI>().text = "Интересно... Возьму-ка исследованные документы в руки, чтобы прочитать более внимательно.";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\n<color=yellow>Двойной клик</color>, чтобы взять документ в руки или положить обратно";
                if (FindObjectOfType<Zoom>().GetCount() > 0)
                {
                    Zoom[] zo = FindObjectsOfType<Zoom>();
                    foreach(Zoom z in zo)
                    {
                        if (z.isZoomed && z.gameObject.tag.Equals("Searchable"))
                            return;
                    }
                    tutorialStage++;
                }
                break;
            case 2:
                subText.GetComponent<TextMeshProUGUI>().text = "Сейчас посмотрим...";
                cam.GetComponent<TurnCamera>().enabled = true;
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\n<color=yellow>F</color>, чтобы сфокусироваться";
                if (Input.GetKeyDown("f"))
                {
                    tutorialStage++;
                }
                break;
            case 3:
                //cam.GetComponent<TurnCamera>().enabled = false;
                subText.GetComponent<TextMeshProUGUI>().text = "Хм... Кое-какие высказывания можно соединить друг с другом, чтобы восстановить полную картину, а некоторые полноценны сами по себе.";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\nМожно нажимать на высказывания и соединять их друг с другом чтобы получать <color=yellow>заметки</color>";
                NotePrefab[] pr = FindObjectsOfType<NotePrefab>();
                if (pr.Length > 0)
                {
                    tutorialStage++;
                }
                break;
            case 4:
                //cam.GetComponent<TurnCamera>().enabled = true;
                subText.GetComponent<TextMeshProUGUI>().text = "Это важно, мне следует это записать.";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\n<color=yellow>F</color>, чтобы вернуться";
                if (Input.GetKeyDown("f"))
                {
                    tutorialStage++;
                }
                break;
            case 5:
                subText.GetComponent<TextMeshProUGUI>().text = "Пора взглянуть на доску с подозреваемыми.";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\n<color=yellow>Пробел</color>, чтобы посмотреть на доску";
                if (Input.GetKeyDown("space"))
                {
                    tutorialStage++;
                }
                break;
            case 6:
                subText.GetComponent<TextMeshProUGUI>().text = "Алиби, мотивы, улики и связи - все они помогут мне определить, кто же преступник. Только следует переместить заметки в нужное место...";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\n<color=yellow>Зажать ЛКМ</color>, чтобы переместить заметку";
                ProgressBar[] bars = FindObjectsOfType<ProgressBar>();
                foreach (ProgressBar bar in bars)
                {
                    if (bar.transform.GetChild(0).GetComponent<Image>().fillAmount > 0.5f ||
                        bar.transform.GetChild(0).GetComponent<Image>().fillAmount < 0.5f)
                        tutorialStage++;
                }
                break;
            case 7:
                subText.GetComponent<TextMeshProUGUI>().text = "Отлично. Теперь картина более-менее вырисовывается. Осталось определить преступника до конца рабочего дня.";
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\nЧем выше <color=yellow>шкала</color> под фото, тем более <color=yellow>подозрительным</color> являеется человек";
                if (Input.GetKeyDown("space"))
                {
                    tutorialStage++;
                }
                break;
            case 8:
                subText.GetComponent<TextMeshProUGUI>().text = "";
                hint.GetComponent<TextMeshProUGUI>().text = "";
                string[] ti = time.GetComponent<TextMeshProUGUI>().text.Split(':');
                if (int.Parse(ti[0]) >= 17)
                    tutorialStage++;
                break;
            case 9:
                if (count == 0)
                {
                    StartCoroutine(Show());
                    count++;
                }
                break;
            case 10:
                hint.GetComponent<TextMeshProUGUI>().text = "<color=green>Подсказка</color>\nМожно нажать на <color=yellow>дротики</color> на столе, чтобы выбрать преступника, если он выявлен, либо продолжить расследование завтра";
                string[] tim = time.GetComponent<TextMeshProUGUI>().text.Split(':');
                if (int.Parse(tim[0]) >= 18)
                    tutorialStage++;
                break;
            case 11:
                hint.GetComponent<TextMeshProUGUI>().text = "";
                subText.GetComponent<TextMeshProUGUI>().text = "";
                gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    IEnumerator Show()
    {
        tutorialStage++;
        subText.GetComponent<TextMeshProUGUI>().text = "Через час конец дня... Я могу отложить это дело на следующий день, но не стоит забывать про ежедневные расходы на аренду и лечение. Чтобы их покрыть, мне надо скорее получить оплату за верно раскрытое дело.";
        yield return new WaitForSeconds(20f);
        subText.GetComponent<TextMeshProUGUI>().text = "";
    }
}
