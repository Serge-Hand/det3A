
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private int ID;
    private string text;
    private List<NoteParameters> parameters;

    public Note(int ID, string text, List<NoteParameters> parameters)
    {
        this.ID = ID;
        this.text = text;
        this.parameters = parameters;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void SetID(int ID)
    {
        this.ID = ID;
    }

    public void SetParameters(List<NoteParameters> parameters)
    {
        this.parameters = parameters;
    }

    public int GetID()
    {
        return ID;
    }

    public string GetText()
    {
        return text;
    }

    public List<NoteParameters> GetParameters()
    {
        return parameters;
    }
}
