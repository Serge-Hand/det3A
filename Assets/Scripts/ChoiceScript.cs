﻿using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;

public class ChoiceScript : MonoBehaviour
{
    /*const string p1 = "Мистер Генри Найтингейл. Он уморил Регину, чтобы получить наследство. Он подарил тете радио, к которому он подключил микрофон и по ночам изображал голос покойного мужа женщины.Постепенно ее запугивая с каждым днем он стремительно приближал день кончины Регины.В день ее смерти, миссис Найтингейл рассматривал новый экземпляр завещания, в котором был Генри. Юноша зашел в комнату к тете в одежде ее мужа с приклеенной бородой, что довело женщину до инфаркта.Она выронила завещание из руки и оно сгорело в камине. В итоге действительным стало старое завещание, по которому все деньги достаются Магдале Грейсон.";
    const string p2 = "Мистер Джеймс Батлер. Из-за несправедливого обвинения и участи пожизненного заключения, Джеймс кончает с собой.";
    const string p3 = "Фергус Грейсон. Из-за потери основного кормильца семьи, Магдала и ее ребенок будут голодать. Женщине не будет хватать денег на ребенка и себя. Генри не будет ей помогать материально. Магдале придется отдать ребенка в приют, чтобы тот смог жить лучше, чем с ней в нищете.";
    const string p4 = "Магдала Грейсон. Из-за несправедливого обвинения и участи пожизненного заключения, Магдала кончает с собой.";*/

    GameObject g_blscr;
    GameObject g_dart;
    GameObject g_isolator_cube;

    GameObject[] photos;
    int photoNum = -1; //Номер выбранной фотографии (0-4, т.е. 1-5)

    /* API
     * 
     * Initialize() - Фотографии становятся нажимаемыми, ожидание выбора одной из них, после чего выполняется OnPhotoClick и ThrowDart
     */

    public void Start()
    {
        g_blscr = GameObject.Find("BlackScreen");
        g_blscr.SetActive(false);

        g_isolator_cube = GameObject.Find("IsolatorCube");
        g_isolator_cube.SetActive(false);

        g_dart = GameObject.Find("Dart");
        g_dart.SetActive(false);
    }

    public void Initialize()
    {
        photos = GameObject.FindGameObjectsWithTag("photo");
        int i = 0;
        foreach (GameObject p in photos)
        {
            p.AddComponent<BoxCollider>();
            p.AddComponent<OnPhotoClickDetect>().SetHandler(this, i);
            p.AddComponent<CursorChange>();
            i++;
        }

        g_dart.SetActive(true);
        g_isolator_cube.SetActive(true);
    }

    public void OnPhotoClick(int num) // Вызывается нажатием на фотографию
    {
        photoNum = num; // Ставим, какое фото было нажато

        foreach (GameObject p in photos) // Больше нельзя нажимать на фотографии
        {
            p.GetComponent<OnPhotoClickDetect>().ClickPossibilityOff();
        }

        string allResults = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "/Files/Other/" + "results" + GameManager.currentCaseNum + ".txt");
        string[] tmp = allResults.Split('/');

        TextMeshProUGUI txt = g_blscr.transform.Find("EndCaseText").GetComponentInChildren<TextMeshProUGUI>(); //Текст на чёрном экране
        string newText;
        switch (photoNum) // Определяем какой именно текст ставить
        {
            case 0: newText = tmp[1]; break;
            case 1: newText = tmp[2]; break;
            case 2: newText = tmp[3]; break;
            case 3: newText = tmp[4]; break;
            case 4: newText = tmp[5]; break;
            default: newText = "default"; break;
        }
        txt.text = newText.Substring(0, newText.Length - 3);

        int payment = int.Parse(tmp[0]);//сколько денег получаем за дело
        bool isCorrect;//верно ли выбрано
        if (newText.Substring(newText.Length - 3, 1).Equals("1"))
            isCorrect = true;
        else
            isCorrect = false;

        StartCoroutine(ThrowDart(num)); // Запускаем процесс запуска дротика (анимация и звук)
    }

    IEnumerator ThrowDart(int num)
    {
        GameObject cam = GameObject.Find("Choice Camera");
        GameObject dart = GameObject.Find("Dart");

        cam.GetComponent<Animator>().SetTrigger("Cam" + num);
        dart.GetComponent<Animator>().SetTrigger("Dart" + num);

        yield return new WaitForSeconds(1.4f); // Подождать пока закончится анимация полёта

        FindObjectOfType<AudioManager>().Play("hitSound"); // Воспроизвести звук

        yield return new WaitForSeconds(2.0f); // Подождать пока закончится звук
        g_blscr.SetActive(true); // Уйти в чёрный экран
    }
}
