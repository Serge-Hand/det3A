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

    public void CheckLinks(string link1, string link2)
    {
        string[] nums1 = link1.Split('_');
        string[] nums2 = link2.Split('_');

        if (int.Parse(nums1[0]) == 0 || int.Parse(nums2[0]) == 0)//проверяем, не пустышка ли одна из ссылок
        {
            //наказание
            Debug.Log("Incorrect.");
            return;
        }
        if (!link1.Equals(link2))//если что-либо у ссылок не совпадает
        {
            //наказание
            Debug.Log("Incorrect.");
            return;
        }
        if (link1.Equals(link2) && int.Parse(nums1[1]) == 0)//кликнуто на одну ссылку, но она парная, а не одиночная
        {
            //наказание
            Debug.Log("Incorrect.");
            return;
        }
        //если всё верно
        //сгенерировать заметку
    }
}
