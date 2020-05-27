using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static readonly string SAVE_FOLDER_NOTES = "/Files/Notes/";
    private static readonly string SAVE_FOLDER_DOCUMENTS = "/Files/Documents/";

    private List<Note> notes = new List<Note>();
    private List<string> documents = new List<string>();

    [SerializeField] private GameObject documentPrefab = null;

    GameObject hintPanel;
    Animator hintAnim;
    [SerializeField] private GameObject tutorialManager = null;

    GameObject mainCam;
    GameObject choiceCam;

    TimeManager timeMan;

    static public int currentCaseNum = 1;

    GameObject g_newDayScreen;
    static int day = 1;
    static int curHour = 8;
    static int curMin = 0;

    //private static GameObject instance; // Ссылка на первый gameobject, чтобы он оставался единственным

    private void Start()
    {
        //При загрузке уровня:
        
        if (currentCaseNum == 0) currentCaseNum = 1; //определить номер дела (хранится в currentCaseNum)
        Load(currentCaseNum); //загрузить файлы этого дела
        CreateDocuments(); //сгенерировать документы
        //создать доску (обязательно после определения номера дела, потому что оно берёт номер дела внутри своих функций)
        FindObjectOfType<BoardManager>().CreateBoard();
        //включить туториал на первом уровне (в самом низу старта)

        timeMan = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        if (timeMan == null)
        {
            Debug.LogWarning("TimeManager don't found");
        }
        else
        {
            timeMan.StartTimer(curHour, curMin, 18);
        }

        hintPanel = GameObject.Find("HintPanel");
        hintAnim = hintPanel.GetComponent<Animator>();

        mainCam = GameObject.Find("Main Camera");
        choiceCam = GameObject.Find("Choice Camera");
        mainCam.SetActive(true);
        choiceCam.SetActive(false);

        g_newDayScreen = GameObject.Find("NewDayScreen");
        g_newDayScreen.SetActive(false);

        if (currentCaseNum == 1)
        {
            TurnOnTutorial(); //включить туториал на первом уровне
        }
    }

    public void CreateNote(int ID)
    {
        bool isCreated = false;
        for (int i = notes.Count - 1; i >= 0; i--)
        {
            if (notes[i].GetID() == ID)
            {
                FindObjectOfType<BoardManager>().CreateNote(notes[i]);
                notes.RemoveAt(i);
                Debug.Log("Note created!");
                showNewNoteHint();
                isCreated = true;
            }
        }
        if (!isCreated)
        {
            FindObjectOfType<AudioManager>().Play("existSound");
            showExistingNoteHint();
            Debug.Log("Note already exists!");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("writingSound");
            timeMan.AddMinutesToTime(TimeManager.c_linksMinutesPlus);
        }
    }

    public void CreateDocuments()
    {
        Zoom[] tmp = FindObjectsOfType<Zoom>();
        foreach (Zoom z in tmp) //удаляем старые документы со сцены
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
        string jsonNotes = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + SAVE_FOLDER_NOTES + "notes" + caseIndex + ".txt");
        string jsonDocs = File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + SAVE_FOLDER_DOCUMENTS + "documents" + caseIndex + ".txt");

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

    void showNewNoteHint()
    {
        hintPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Новая заметка!";
        hintAnim.Play("HintPanel");
    }

    void showExistingNoteHint()
    {
        hintPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Такая заметка уже существует";
        hintAnim.Play("HintPanel");
    }

    public void CameraChoiceSwitch()
    {
        if(mainCam.activeSelf)
        {
            mainCam.SetActive(false);
            choiceCam.SetActive(true);
        }
        else
        {
            choiceCam.SetActive(false);
            mainCam.SetActive(true);
        }
    }

    public void TurnOnTutorial()
    {
        tutorialManager.SetActive(true);
    }

    public IEnumerator OnEndOfDay()
    {
        TextMeshProUGUI c_text = g_newDayScreen.transform.Find("NewDayText").GetComponentInChildren<TextMeshProUGUI>();

        g_newDayScreen.SetActive(true);
        day++;
        c_text.SetText("День " + day);

        yield return new WaitForSeconds(3f);

        timeMan.StartTimer(8, 0, 18);
        g_newDayScreen.SetActive(false);
    }

    public void OnSuspectedChoice() // При выборе подозреваемого сделать...:
    {
        GetComponent<ChoiceScript>().Initialize();
        CameraChoiceSwitch();

        //GameObject.Find("GameManager").GetComponent<ChoiceScript>().Initialize();
        //GameObject.Find("GameManager").GetComponent<GameManager>().CameraChoiceSwitch();

        timeMan.StopTimer();
    }

    public void OnEndOfCase()
    {
        currentCaseNum++;

        curHour = timeMan.GetCurrentDayTime().Hour;
        curMin = timeMan.GetCurrentDayTime().Minute;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
