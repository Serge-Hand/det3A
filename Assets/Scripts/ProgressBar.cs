using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private int column;

    public int GetColumn()
    {
        return column;
    }
    public void SetColumn(int column)
    {
        this.column = column;
    }
}
