using System.Collections;
using TMPro;
using UnityEngine;

public class ChoiceScript : MonoBehaviour
{
    const string p1 = "Мистер Генри Найтингейл. Он уморил Регину, чтобы получить наследство. Он подарил тете радио, к которому он подключил микрофон и по ночам изображал голос покойного мужа женщины.Постепенно ее запугивая с каждым днем он стремительно приближал день кончины Регины.В день ее смерти, миссис Найтингейл рассматривал новый экземпляр завещания, в котором был Генри. Юноша зашел в комнату к тете в одежде ее мужа с приклеенной бородой, что довело женщину до инфаркта.Она выронила завещание из руки и оно сгорело в камине. В итоге действительным стало старое завещание, по которому все деньги достаются Магдале Грейсон.";
    const string p2 = "Мистер Джеймс Батлер. Из-за несправедливого обвинения и участи пожизненного заключения, Джеймс кончает с собой.";
    const string p3 = "Фергус Грейсон. Из-за потери основного кормильца семьи, Магдала и ее ребенок будут голодать. Женщине не будет хватать денег на ребенка и себя. Генри не будет ей помогать материально. Магдале придется отдать ребенка в приют, чтобы тот смог жить лучше, чем с ней в нищете.";
    const string p4 = "Магдала Грейсон. Из-за несправедливого обвинения и участи пожизненного заключения, Магдала кончает с собой.";

    GameObject blscr;
    [SerializeField] GameObject dart;

    int photoNum = -1; //Номер выбранной фотографии (0-4, т.е. 1-5)

    /* API
     * 
     * Initialize() - Фотографии становятся нажимаемыми, ожидание выбора одной из них, после чего выполняется OnPhotoClick и ThrowDart
     */

    void Start()
    {
        blscr = GameObject.Find("BlackScreen");
        blscr.SetActive(false);
    }

    public void Initialize()
    {
        GameObject[] photos = GameObject.FindGameObjectsWithTag("photo");
        int i = 0;
        foreach (GameObject p in photos)
        {
            p.AddComponent<BoxCollider>();
            p.AddComponent<OnPhotoClickDetect>().SetHandler(this, i);
            i++;
        }

        dart.SetActive(true);
    }

    public void OnPhotoClick(int num) // Вызывается нажатием на фотографию
    {
        photoNum = num; // Ставим, какое фото было нажато

        TextMeshProUGUI txt = blscr.transform.GetComponentInChildren<TextMeshProUGUI>(); //Текст на чёрном экране
        string newText = null;
        switch (photoNum) // Определяем какой именно текст ставить
        {
            case 0: newText = p1; break;
            case 1: newText = p2; break;
            case 2: newText = p3; break;
            case 3: newText = p4; break;
            case 4: newText = p4; break;
            default: newText = "default"; break;
        }
        txt.text = newText;

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
        blscr.SetActive(true); // Уйти в чёрный экран
    }
}
