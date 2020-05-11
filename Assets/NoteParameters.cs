
public class NoteParameters
{
    private int value;
    public enum Row
    {
        Alibi,
        Motive,
        Clue,
        Relationship
    }
    Row row;
    private int column;

    public NoteParameters(int value, Row row, int column)
    {
        this.value = value;
        this.row = row;
        this.column = column;
    }

    public int GetColumn()
    {
        return column;
    }
    public Row GetRow()
    {
        return row;
    }
    public int GetValue()
    {
        return value;
    }
}
