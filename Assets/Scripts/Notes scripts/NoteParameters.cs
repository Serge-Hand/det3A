using System;
using UnityEngine;

[Serializable]
public class NoteParameters
{
    [SerializeField]
    private int value;
    /*public enum Row
    {
        Alibi,
        Motive,
        Clue,
        Relationship
    }*/
    [SerializeField]
    //private Row row;
    private int row;
    [SerializeField]
    private int column;

    public NoteParameters(int value, int row, int column)
    {
        this.value = value;
        this.row = row;
        this.column = column;
    }

    public int GetColumn()
    {
        return column;
    }
    public int GetRow()
    {
        return row;
    }
    public int GetValue()
    {
        return value;
    }
}
