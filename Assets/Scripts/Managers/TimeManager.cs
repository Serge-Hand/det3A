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
     * void StopTimer() - остановить отсчёт времени без других действий
     * 
     * void OnTimerStop() - не вызывается, но сюда нужно вставить вызов того что нужно делать в конце дня
     */

    public const int c_magnifierMinutesPlus = 15;
    public const int c_linksMinutesPlus = 10;
    public const int c_deskMinutesPlus = 10;

    void Awake()
    {
        time_text = text_object.GetComponent<TMP_Text>();
        if (time_text == null)
        {
            Debug.LogError("Set TimeTextObject for TimeManager");
            this.enabled = false;
        }
    }

    public void StartTimer(int startHour, int startMinute, int endHour)
    {
        if (startHour < 0 || startHour > 23 || endHour < 0 || endHour > 23) // Часы должны быть между 0 и 23
        {
            Debug.LogError("Wrong hour");
            return;
        }
        if (startMinute < 0 || startMinute >= 60) // Минуты должны быть между 0 и 59
        {
            Debug.LogError("Wrong minute");
            return;
        }
        if (endHour <= startHour) // Если стартовое время больше конечного, то это мгновенная остановка таймера
        {
            Debug.LogWarning("Instant end of timer");
            return;
        }

        //startSecond = Time.time;

        currTime = new DayTime(startHour, startMinute);
        endTime = new DayTime(endHour, 0);

        SetTimerText(currTime);

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

            //Debug.Log(currTime.Minute.ToString() + EndTimerCheck() + timeIsTicking);
        }
        OnTimerEnd();
    }

    bool EndTimerCheck() //true - продолжать работу; false - остановить
    {
        if (currTime.Hour > endTime.Hour)
        {
            return false;
        }
        if (currTime.Hour == endTime.Hour)
        {
            if (currTime.Minute >= endTime.Minute)
            {
                return false;
            }
        }
        return true;
    }

    void SetTimerText(DayTime time)
    {
        string minute = time.Minute.ToString();

        if (time.Minute < 10)
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

            if (!EndTimerCheck())
            {
                StopCoroutine(Timer());
                OnTimerEnd();
            }
        }
    }

    public DayTime GetCurrentDayTime()
    {
        return currTime;
    }

    public void StopTimer()
    {
        if (timeIsTicking == true)
        {
            timeIsTicking = false;
            //StopCoroutine("Timer"); //Не работает, хотя в скрипте выше работает
            StopAllCoroutines();
        }
    }

    void OnTimerEnd()
    {
        if (timeIsTicking == true)
        {
            timeIsTicking = false;
            StartCoroutine(GameObject.Find("GameManager").GetComponent<GameManager>().OnEndOfDay());
        }
    }
}