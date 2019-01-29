using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static int numChars = 6;
    public string[] RoomPreferences = new string[numChars];
    public string[] NeighborPrefereences = new string[numChars];
    public List<string> Friends;

    public string Name;
    public int Contentment = 0;
    public string roomName;
    public int roomIndex;

    private int roomContentment = 0;
    private int neighborContentment = 0;


    public void checkIn(string room, int roomNumber, List<Character> neighbors)
    {
        for (int i = numChars; i > 0; i--)
        {
            if (room == RoomPreferences[i])
            {
                this.roomContentment = i;
            }
        }

        for (int i = numChars; i > 0; i--)
        {
            foreach (Character neighbor in neighbors)
            {
                if (neighbor.Name == NeighborPrefereences[i])
                {
                    this.neighborContentment = this.neighborContentment + i;
                }
            }
        }

        this.roomName = room;
        this.roomIndex = roomNumber;
        this.Contentment = roomContentment + neighborContentment;
    }

    public void eventContenttment(int deltaContent)
    {
        this.Contentment = this.Contentment + deltaContent;
    }

    public void setContenttment(int newContentment)
    {
        this.Contentment = newContentment;
    }
    public void eventConversation(Character neighbor)
    {
        this.Friends.Add(neighbor.Name);
    }
}