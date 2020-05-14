using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Note> notes;

    private void Start()
    {
        notes.Add(new Note(0, "Test note 0 (Первый, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 0) }));
        notes.Add(new Note(1, "Test note 1 (Первый, Мотив -)", new List<NoteParameters> { new NoteParameters(-1, (NoteParameters.Row)1, 0) }));
        notes.Add(new Note(2, "Test note 2 (Третий, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 2) }));
    }

    public void CreateNote(int ID)
    {
        int i = 0;
        foreach (Note note in notes)
        {
            if (note.GetID() == ID)
            {
                FindObjectOfType<BoardManager>().CreateNote(note);
                notes.Remove(note);
                Debug.Log("Note created!");
                return;
            }
            i++;
        }
        Debug.Log("Note already exists!");
    }
}
