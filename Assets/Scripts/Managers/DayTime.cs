public class DayTime
{
    private int hour;
    private int minute;

    public int Hour { get => hour; 
        set { 
            if (value > 0)
                hour = value % 24;
        } 
    }
    public int Minute { get => minute; 
        set { 
            if (value >= 0) 
                minute = value;
            if (value >= 60)
            {
                minute = value % 60;
                hour += (value - minute) / 60;
            }
        } 
    }

    public DayTime(int h, int m)
    {
        Hour = h;
        Minute = m;
    }
}
