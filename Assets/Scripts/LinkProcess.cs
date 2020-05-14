using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkProcess : MonoBehaviour
{
    //формат ID - X_X_X
    // _ - разделитель
    //Первая цифра(0, 1) - является ли ссылка НЕ пустышкой(0 - пустышка, 1 - не пустышка), если пустышка, остальные цифры значения не имеют
    //Вторая цифра(0, 1) - есть ли у ссылки пара(0 - нет, 1 - да), если нет, то ссылку можно совместить только саму с собой, но парные сами с собой - нет
    //Третья цифра(произвольное целое число) - уникальный идентификатор ссылки; Могут быть соединены только ссылки с одинаковыми значениями.Если они разные: они не соединяются

    public static void CheckLinks(string link1_ID, string link2_ID)
    {
        string[] nums1 = link1_ID.Split('_');
        string[] nums2 = link2_ID.Split('_');

        if (int.Parse(nums1[0]) == 0 || int.Parse(nums2[0]) == 0)//проверяем, не пустышка ли одна из ссылок
        {
            //наказание
            Debug.Log("Incorrect: Empty");
            return;
        }
        if (!link1_ID.Equals(link2_ID))//если что-либо у ссылок не совпадает
        {
            //наказание
            Debug.Log("Incorrect: Cant match");
            return;
        }
        if (link1_ID.Equals(link2_ID) && int.Parse(nums1[1]) != 0)//кликнуто на одну ссылку, но она парная, а не одиночная
        {
            //наказание
            Debug.Log("Incorrect: Expected a pair");
            return;
        }
        //если всё верно
        //сгенерировать заметку
        FindObjectOfType<GameManager>().CreateNote(int.Parse(nums1[2]));
    }
}
