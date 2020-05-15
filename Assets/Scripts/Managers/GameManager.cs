using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static readonly string SAVE_FOLDER_NOTES = "/Files/Notes/";

    private List<Note> notes = new List<Note>();

    private void Start()
    {
        /*notes.Add(new Note(0, "Test note 0 (Первый, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 0) }));
        notes.Add(new Note(1, "Test note 1 (Первый, Мотив -)", new List<NoteParameters> { new NoteParameters(-1, 1, 0) }));
        notes.Add(new Note(2, "Test note 2 (Третий, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 2) }));*/

        //SaveNotes();
        LoadNotes("notes1.txt");
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
                FindObjectOfType<AudioManager>().Play("writingSound");
                Debug.Log("Note created!");
                return;
            }
            i++;
        }
        FindObjectOfType<AudioManager>().Play("hintSound");
        Debug.Log("Note already exists!");
    }

    public void LoadNotes(string filename)
    {
        string json = File.ReadAllText(Application.dataPath + SAVE_FOLDER_NOTES + filename);
        NotesListHelper helper = JsonUtility.FromJson<NotesListHelper>(json);
        notes = helper.GetAll();
    }

    public void SaveNotes()
    {
        NotesListHelper helper = new NotesListHelper();
        helper.AddAll(notes);
        string json = JsonUtility.ToJson(helper, true);
        File.WriteAllText(Application.dataPath + SAVE_FOLDER_NOTES + "notes1.txt", json);
    }

    [Serializable]
    private class NotesListHelper
    {
        [SerializeField]
        private List<Note> notes;

        public NotesListHelper()
        {
            notes = new List<Note>();
        }

        public void Add(Note note)
        {
            notes.Add(note);
        }

        public void AddAll(List<Note> notes)
        {
            this.notes = notes;
        }

        public List<Note> GetAll()
        {
            return notes;
        }
    }
}
