using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    private List<GameObject> notes = new List<GameObject>();
    public GameObject notePrefab;
    public GameObject notePlacePrefab;

    public GameObject progressBarPrefab;
    public GameObject canvas;

    private int noteLayers;

    private void Start()
    {
        CreateBoard(4);
        //CreateNote("Первый подозреваемый\nАлиби\n+", new List<NoteParameters> { new NoteParameters(1, 0, 0) });
        //CreateNote("Первый подозреваемый\nМотив\n-", new List<NoteParameters> { new NoteParameters(-1, (NoteParameters.Row)1, 0) });
        //CreateNote("Третий подозреваемый\nАлиби\n+", new List<NoteParameters> { new NoteParameters(1, 0, 2) });

        ProgressBar[] bars = FindObjectsOfType<ProgressBar>();
        foreach (ProgressBar bar in bars)
        {
            bar.transform.GetChild(0).GetComponent<Image>().fillAmount = 0.5f;
        }
    }

    public void CreateBoard(int suspectsNumber)
    {
        noteLayers = 1;

        float nextPositionY = transform.position.y + 1.95f;//2.3f;
        float gapY = 0.2f;
        for (int i = 0; i < 4; i++)
        {
            float nextPositionZ = transform.position.z + 7.17f;//-7.2f;
            float gapX = 0.2f;
            for (int j = 0; j < suspectsNumber; j++)
            {
                GameObject newPlace = Instantiate(notePlacePrefab, new Vector3(transform.position.x - 0.02f, nextPositionY, nextPositionZ), notePlacePrefab.transform.rotation);
                //newPlace.transform.SetParent(transform, false);
                newPlace.transform.rotation = Quaternion.Euler(0, 360, 90);
                newPlace.GetComponent<NotePlace>().SetRow(i);
                newPlace.GetComponent<NotePlace>().SetColumn(j);
                nextPositionZ -= (notePlacePrefab.GetComponent<Renderer>().bounds.size.x + gapX);
            }
            nextPositionY -= (notePlacePrefab.GetComponent<Renderer>().bounds.size.y + gapY);
        }
        float nextPosBar = 14.9f;
        for (int i = 0; i < suspectsNumber; i++)
        {
            var createImage = Instantiate(progressBarPrefab, new Vector3(nextPosBar, -5, 0), progressBarPrefab.transform.rotation) as GameObject;
            createImage.transform.SetParent(canvas.transform, false);
            createImage.GetComponent<ProgressBar>().SetColumn(i);
            nextPosBar += (progressBarPrefab.GetComponent<RectTransform>().sizeDelta.x + 2.5f);
        }
    }

    public static void Compare(GameObject note, GameObject place)
    {
       List<NoteParameters> param = note.GetComponent<NotePrefab>().GetParameters();

       if (param == null)
        {
            print("Param is null");
            return;
        }

       foreach(NoteParameters p in param)
        {
            if (place.GetComponent<NotePlace>().GetColumn() == p.GetColumn() && place.GetComponent<NotePlace>().GetRow() == p.GetRow())
            {
                ProgressBar curBar = null;
                ProgressBar[] bars = FindObjectsOfType<ProgressBar>();
                foreach (ProgressBar bar in bars)
                {
                    if (bar.GetColumn() == p.GetColumn())
                    {
                        curBar = bar;
                        break;
                    }
                }
                print("Matched! Value = " + p.GetValue());
                if (p.GetValue() == 1)
                    curBar.transform.GetChild(0).GetComponent<Image>().fillAmount += 0.125f;
                if (p.GetValue() == -1)
                    curBar.transform.GetChild(0).GetComponent<Image>().fillAmount -= 0.125f;
            }
        }
    }

    public static void Detach (GameObject note, GameObject place)
    {
        List<NoteParameters> param = note.GetComponent<NotePrefab>().GetParameters();

        if (param == null)
        {
            print("Param is null");
            return;
        }

        foreach (NoteParameters p in param)
        {
            if (place.GetComponent<NotePlace>().GetColumn() == p.GetColumn() && place.GetComponent<NotePlace>().GetRow() == p.GetRow())
            {
                ProgressBar curBar = null;
                ProgressBar[] bars = FindObjectsOfType<ProgressBar>();
                foreach (ProgressBar bar in bars)
                {
                    if (bar.GetColumn() == p.GetColumn())
                    {
                        curBar = bar;
                        break;
                    }
                }
                print("Detached! Value = " + p.GetValue());
                if (p.GetValue() == 1)
                    curBar.transform.GetChild(0).GetComponent<Image>().fillAmount -= 0.125f;
                if (p.GetValue() == -1)
                    curBar.transform.GetChild(0).GetComponent<Image>().fillAmount += 0.125f;
            }
        }
    }

    public void CreateNote(Note note)
    {
        GameObject newNote = Instantiate(notePrefab, new Vector3(15.854f, Random.Range(10.6f, 20f), Random.Range(-3.5f, 0f)), notePrefab.transform.rotation);
        newNote.transform.rotation = Quaternion.Euler(0, 360, 90);
        newNote.transform.GetChild(0).GetComponent<TextMeshPro>().text = note.GetText();
        newNote.GetComponent<NotePrefab>().SetText(note.GetText());
        newNote.GetComponent<NotePrefab>().SetParameters(note.GetParameters());
        newNote.GetComponent<NotePrefab>().SetLayer(noteLayers);
        newNote.transform.position -= new Vector3(noteLayers * 0.0028f, 0, 0);
        notes.Add(newNote);
        noteLayers++;
    }

    public void ChangeLayers(int layer)
    {
        NotePrefab[] tmp = FindObjectsOfType<NotePrefab>();
        foreach (NotePrefab n in tmp)
        {
            int l = n.GetLayer();
            if (l == layer && layer != (noteLayers - 1))
            {
                n.SetLayer(noteLayers - 1);
                n.gameObject.transform.position = new Vector3(15.854f - (0.0028f * (noteLayers - 1f)), n.gameObject.transform.position.y, n.gameObject.transform.position.z);
            }
            if (l > layer)
            {
                n.SetLayer(--l);
                n.gameObject.transform.position = new Vector3(15.854f - 0.0028f * l, n.gameObject.transform.position.y, n.gameObject.transform.position.z);
            }
        }
    }
}
