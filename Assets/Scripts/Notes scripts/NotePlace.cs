using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePlace : MonoBehaviour
{
    private int column;
    NoteParameters.Row row;

   /* private void Start()
    {
        column = 0;
        row = 0;
    }*/

    public int GetColumn()
    {
        return column;
    }
    public NoteParameters.Row GetRow()
    {
        return row;
    }

    public void SetColumn(int column)
    {
        this.column = column;
    }
    public void SetRow(NoteParameters.Row row)
    {
        this.row = row;
    }
}
