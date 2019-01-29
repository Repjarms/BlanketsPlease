using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InnEvent
{
    public enum EventType
    {
        CheckIn,
        GuestRequest,
        ManagementBreak,
        HallwayEncounter
    }

    public EventType type;
    public List<NPC> involvedCharacters;
    public string sceneName;
}
