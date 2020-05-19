
using System.Collections.Generic;
using UnityEngine;

public class NotePrefab : MonoBehaviour
{
    private int ID;
    private string text;
    private List<NoteParameters> parameters;
    private int layer;

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

    public void SetLayer(int layer)
    {
        this.layer = layer;
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

    public int GetLayer()
    {
        return layer;
    }
}
