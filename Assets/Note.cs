
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private string text;
    private List<NoteParameters> parameters;

    public Note(string text, List<NoteParameters> parameters)
    {
        this.text = text;
        this.parameters = parameters;
    }

    public void SetText(string text)
    {
        this.text = text;
    }

    public void SetParameters(List<NoteParameters> parameters)
    {
        this.parameters = parameters;
    }

    public List<NoteParameters> GetParameters()
    {
        return parameters;
    }
}
