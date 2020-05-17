using System.Collections;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    [SerializeField] GameObject text_object = null;
    TMP_Text time_text;

    //float startSecond;
    DayTime currTime;
    DayTime endTime;

    bool timeIsTicking = false;

    /* API
     * void StartTimer(int startHour, int endHour) - запустить течение времени с остановкой в endHour
     * void AddMinutesToTime(int minutes) - работает только когда течение времени запущено
     * 
     * void OnTimerStop() - не вызывается, но сюда нужно вставить вызов того что нужно делать в конце дня
     */

    public const int c_magnifierMinutesPlus = 15;
    public const int c_linksMinutesPlus = 10;
    public const int c_deskMinutesPlus = 10;

    void Awake()
    {
        time_text = text_object.GetComponent<TMP_Text>();
        if(time_text == null)
        {
            Debug.LogError("Set TimeTextObject for TimeManager");
            this.enabled = false;
        }
    }

    void Start()
    {
        time_text.text = "8:00";
    }

    /*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddMinutesToTime(15);
        }
    }*/

    public void StartTimer(int startHour, int endHour)
    {
        if (startHour < 0 || startHour > 23 || endHour < 0 || endHour > 23)
        {
            Debug.LogError("Wrong hour");
            return;
        }
        if (endHour <= startHour)
        {
            Debug.LogWarning("Instant end of timer");
            return;
        }
        time_text.text = startHour + ":00";
        //startSecond = Time.time;

        currTime = new DayTime(startHour, 0);
        endTime = new DayTime(endHour, 0);

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        timeIsTicking = true;
        while (EndTimerCheck())
        {
            yield return new WaitForSeconds(6f);
            currTime.Minute++;
            SetTimerText(currTime);

            //Debug.Log(currTime.Minute.ToString() + EndTimerCheck());
        }
        OnTimerStop();
    }

    bool EndTimerCheck() //true - продолжать работу; false - остановить
    {
        if(currTime.Hour > endTime.Hour)
        {
            return false;
        }
        if(currTime.Hour == endTime.Hour)
        {
            if(currTime.Minute >= endTime.Minute)
            {
                return false;
            }
        }
        return true;
    }

    void SetTimerText(DayTime time)
    {
        string minute = time.Minute.ToString();

        if(time.Minute < 10)
        {
            minute = minute.Insert(0, "0");
        }
        time_text.text = time.Hour + ":" + minute;
    }

    public void AddMinutesToTime(int minutes)
    {
        if (timeIsTicking)
        {
            currTime.Minute += minutes;
            SetTimerText(currTime);
            /*
            if (!EndTimerCheck())
            {
                StopCoroutine(Timer());
                OnTimerStop();
            }*/
        }
    }

    void OnTimerStop()
    {
        timeIsTicking = false;
        //Debug.Log("Stop Timer");

        GameObject.Find("GameManager").GetComponent<ChoiceScript>().Initialize();
        GameObject.Find("GameManager").GetComponent<GameManager>().CameraSwitch();
    }
}