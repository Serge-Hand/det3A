using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static readonly string SAVE_FOLDER_NOTES = "/Files/Notes/";
    private static readonly string SAVE_FOLDER_DOCUMENTS = "/Files/Documents/";

    private List<Note> notes = new List<Note>();
    private List<string> documents = new List<string>();

    [SerializeField]
    private GameObject documentPrefab;

    private void Start()
    {
        /*notes.Add(new Note(0, "Test note 0 (Первый, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 0) }));
        notes.Add(new Note(1, "Test note 1 (Первый, Мотив -)", new List<NoteParameters> { new NoteParameters(-1, 1, 0) }));
        notes.Add(new Note(2, "Test note 2 (Третий, Алиби +)", new List<NoteParameters> { new NoteParameters(1, 0, 2) }));*/
        /*documents.Add("Проверка 0");
        documents.Add("Проверка 1");
        documents.Add("Проверка 2");*/

        //Save();
        Load(1);
        CreateDocuments();
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

    public void CreateDocuments()
    {
        Zoom[] tmp = FindObjectsOfType<Zoom>();
        foreach (Zoom z in tmp)//удаляем старые документы со сцены
        {
            Destroy(z.gameObject);
        }

        float x = 8.88f;
        float y = 6.24f;
        foreach (string s in documents)
        {
            GameObject newDoc = Instantiate(documentPrefab, new Vector3(x/*UnityEngine.Random.Range(4f, 8.5f)*/, y, 3.5f/*UnityEngine.Random.Range(1.5f, 4.5f)*/), documentPrefab.transform.rotation);
            newDoc.transform.rotation = Quaternion.Euler(0, UnityEngine.Random.Range(165, 195), 0);
            newDoc.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = s;
            x -= 0.9f;
            y += 0.01f;
        }
    }

    public void Load(int caseIndex)
    {
        string jsonNotes = File.ReadAllText(Application.dataPath + SAVE_FOLDER_NOTES + "notes" + caseIndex + ".txt");
        string jsonDocs = File.ReadAllText(Application.dataPath + SAVE_FOLDER_DOCUMENTS + "documents" + caseIndex + ".txt");

        NotesListHelper helperNotes = JsonUtility.FromJson<NotesListHelper>(jsonNotes);
        DocumentsListHelper helperDocs = JsonUtility.FromJson<DocumentsListHelper>(jsonDocs);

        notes = helperNotes.GetAll();
        documents = helperDocs.GetAll();
    }

    public void Save()
    {
        /*NotesListHelper helper = new NotesListHelper();
        helper.AddAll(notes);
        string json = JsonUtility.ToJson(helper, true);
        File.WriteAllText(Application.dataPath + SAVE_FOLDER_NOTES + "notes1.txt", json);*/
        /*DocumentsListHelper helper = new DocumentsListHelper();
        helper.AddAll(documents);
        string json = JsonUtility.ToJson(helper, true);
        File.WriteAllText(Application.dataPath + SAVE_FOLDER_DOCUMENTS + "documents1.txt", json);*/
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

    [Serializable]
    private class DocumentsListHelper
    {
        [SerializeField]
        private List<string> documents;

        public DocumentsListHelper()
        {
            documents = new List<string>();
        }

        public void Add(string text)
        {
            documents.Add(text);
        }

        public void AddAll(List<string> documents)
        {
            this.documents = documents;
        }

        public List<string> GetAll()
        {
            return documents;
        }
    }
}
