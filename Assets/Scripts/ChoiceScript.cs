using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour
{
    [SerializeField] GameObject photo1 = null;
    [SerializeField] GameObject photo2 = null;
    [SerializeField] GameObject photo3 = null;
    [SerializeField] GameObject photo4 = null;

    const string p1 = "Мистер Генри Найтингейл. Он уморил Регину, чтобы получить наследство. Он подарил тете радио, к которому он подключил микрофон и по ночам изображал голос покойного мужа женщины.Постепенно ее запугивая с каждым днем он стремительно приближал день кончины Регины.В день ее смерти, миссис Найтингейл рассматривал новый экземпляр завещания, в котором был Генри. Юноша зашел в комнату к тете в одежде ее мужа с приклеенной бородой, что довело женщину до инфаркта.Она выронила завещание из руки и оно сгорело в камине. В итоге действительным стало старое завещание, по которому все деньги достаются Магдале Грейсон.";
    const string p2 = "Мистер Джеймс Батлер. Из-за несправедливого обвинения и участи пожизненного заключения, Джеймс кончает с собой.";
    const string p3 = "Фергус Грейсон. Из-за потери основного кормильца семьи, Магдала и ее ребенок будут голодать. Женщине не будет хватать денег на ребенка и себя. Генри не будет ей помогать материально. Магдале придется отдать ребенка в приют, чтобы тот смог жить лучше, чем с ней в нищете.";
    const string p4 = "Магдала Грейсон. Из-за несправедливого обвинения и участи пожизненного заключения, Магдала кончает с собой.";

  GameObject blscr;

    void Start()
    {
        blscr = GameObject.Find("BlackScreen");
        blscr.SetActive(false);
    }

    public void Initialize()
    {
        BoxCollider col;

        col = photo1.AddComponent<BoxCollider>();
        //col.size = new Vector3(col.size.x, 1, col.size.z);
        photo1.AddComponent<OnPhotoClickDetect>().SetHandler(this, 1);

        col = photo2.AddComponent<BoxCollider>();
        //col.size = new Vector3(col.size.x, 1, col.size.z);
        photo2.AddComponent<OnPhotoClickDetect>().SetHandler(this, 2);

        col = photo3.AddComponent<BoxCollider>();
        //col.size = new Vector3(col.size.x, 1, col.size.z);
        photo3.AddComponent<OnPhotoClickDetect>().SetHandler(this, 3);

        col = photo4.AddComponent<BoxCollider>();
        //col.size = new Vector3(col.size.x, 1, col.size.z);
        photo4.AddComponent<OnPhotoClickDetect>().SetHandler(this, 4);
    }

    public void OnPhotoClick(int num)
    {
        //Debug.Log(num);

        blscr.SetActive(true);
        TextMeshProUGUI txt = blscr.transform.GetComponentInChildren<TextMeshProUGUI>();

        string newText = null;

        switch(num)
        {
            case 1: newText = p1; break;
            case 2: newText = p2; break;
            case 3: newText = p3; break;
            case 4: newText = p4; break;
            default: newText = "default"; break;
        }

        txt.text = newText;
    }
}
