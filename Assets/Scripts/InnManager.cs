using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnManager : MonoBehaviour
{

//    public string[] roomDictionary;
    public Character[] roomDwellers = new Character[6];

    private int targetRoom;
    private List<Character> neighbors;
    private List<int> neighborIdx;
    private int room1;

    public void AddNPCToRoom(Room.RoomType roomType, NPC character)
    {

    }

    /*
    public void addDweller(Character dweller, Room.RoomType roomType)
    {

        roomDwellers[room] = dweller;
//        neighbors = findNeighbors(dweller);
        dweller.checkIn(roomDictionary[room], room, neighbors);
    }

    public void switchDwellers(Character char1, Character char2)
    {
        targetRoom = char1.roomIndex;
        addDweller(char1, char2.roomIndex);
        addDweller(char2, targetRoom);
    }
    */

    private List<Character> findNeighbors(Character dweller)
    {
        room1 = dweller.roomIndex;
        if (room1 == 0)
        {
            neighborIdx = new List<int>() { 1, 3 };

        }
        else if (room1 == 1)
        {
            neighborIdx = new List<int>() { 0, 2, 4 };
        }
        else if (room1 == 2)
        {
            neighborIdx = new List<int>() { 1, 5 };
        }
        else if (room1 == 3)
        {
            neighborIdx = new List<int>() { 0, 4 };
        }
        else if (room1 == 4)
        {
            neighborIdx = new List<int>() { 1, 3, 5 };
        }
        else if (room1 == 5)
        {
            neighborIdx = new List<int>() { 2, 4 };
        }

        foreach (int i in neighborIdx)
        {
            neighbors.Add(roomDwellers[i]);
        }
        return neighbors;
    }
}
