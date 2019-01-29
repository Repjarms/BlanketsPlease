using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Room
{
    public enum RoomType
    {
        None,
        Riverview,
        Plush,
        Closet,
        KingBed,
        Minimalist,
        Quirky
    }

    public enum RoomCondition
    {
        Filthy,
        Dirty,
        Clean,
        Pristine
    }

    public int id;
    public string name;
    public RoomCondition condition;
}
