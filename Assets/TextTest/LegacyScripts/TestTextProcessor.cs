using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestTextProcessor : MonoBehaviour
{
    //public TMP_TextEventHandler TextEventHandler;
    TextMeshPro tmp_text = null;
    TextMeshProUGUI tmp_text_ugui = null;

    private void Start()
    {
        tmp_text = GetComponent<TextMeshPro>();
        if (tmp_text == null)
        {
            tmp_text_ugui = GetComponent<TextMeshProUGUI>();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (tmp_text != null)
            {
                int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text, Input.mousePosition, Camera.main);
                if (link_index != -1)
                {
                    Debug.Log(tmp_text.textInfo.linkInfo[link_index].GetLinkText());
                    Debug.Log(link_index);
                    Debug.Log(tmp_text.textInfo.linkInfo[link_index].GetLinkID());

                    int char_ind = tmp_text.textInfo.linkInfo[link_index].linkTextfirstCharacterIndex;
                    Debug.Log( tmp_text.transform.TransformPoint( (tmp_text.textInfo.characterInfo[char_ind].topLeft) ));
                }
            }
            else
            {
                Camera cam = null;
                if(!tmp_text_ugui.canvas.renderMode.Equals(RenderMode.ScreenSpaceOverlay))
                {
                    if (tmp_text_ugui.canvas.renderMode.Equals(RenderMode.ScreenSpaceCamera))
                    {
                        cam = tmp_text_ugui.canvas.worldCamera;
                    }
                    else
                    {
                        cam = Camera.main;
                    }
                }

                int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text_ugui, Input.mousePosition, cam); // <--
                //Если canvas is using screenspace overlay, the camera reference is "null". Если же canvas стоит в другом режиме - ставить соотв. камеру

                if (link_index != -1)
                {
                    Debug.Log(tmp_text_ugui.textInfo.linkInfo[link_index].GetLinkText());
                    Debug.Log(link_index);
                    Debug.Log(tmp_text_ugui.textInfo.linkInfo[link_index].GetLinkID());

                    //int char_ind = tmp_text_ugui.textInfo.linkInfo[link_index].linkTextfirstCharacterIndex;
                    //Debug.Log( tmp_text_ugui.transform.TransformPoint( tmp_text_ugui.textInfo.characterInfo[char_ind].topLeft ));
                }
            }
            
        }
    }

    /*
    void onLinkPressed()
    {
       
    }
    */

/*
    void OnEnable()
    {
        if (TextEventHandler != null)
        {
            TextEventHandler.onLinkSelection.AddListener(linkPressedProcessor);
        }
    }

    void OnDisable()
    {
        if (TextEventHandler != null)
        {
            TextEventHandler.onLinkSelection.RemoveListener(linkPressedProcessor);
        }
    }

    void linkPressedProcessor(string linkID, string linkText, int linkIndex)
    {
        Debug.Log("Link " + linkID + " with text: \"" + linkText + "\" and with Index " + linkIndex + " was pressed!");
        //TMP_TextUtilities.FindIntersectingLink()
    
    }
    */

/*
if (tmp_text.Equals(null))
        {
            int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text, Input.mousePosition, Camera.main);
            if (link_index != -1)
            {
                Debug.Log(tmp_text.textInfo.linkInfo[link_index].GetLinkText());
            }
        }
        else
        {
            int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text_ugui, Input.mousePosition, Camera.main);
            if (link_index != -1)
            {
                Debug.Log(tmp_text.textInfo.linkInfo[link_index].GetLinkText());
            }

*/




    //Хороший вопрос
    /*
 private void Update()
 {
     if (Input.GetMouseButtonDown(0))
     {
         if (!tmp_text.Equals(null))
         {
             onLinkPressed<TextMeshPro>(tmp_text);
         }
         else
         {
             onLinkPressed<TextMeshProUGUI>(tmp_text_ugui);
         }
     }
 }

 void onLinkPressed<T>(T tmp_text)
 {
     if (tmp_text.Equals(null))
     {
         int link_index = TMP_TextUtilities.FindIntersectingLink(tmp_text, Input.mousePosition, Camera.main);
         if (link_index != -1)
         {
             Debug.Log(tmp_text.textInfo.linkInfo[link_index].GetLinkText());
         }
     }
 }
 */

}
