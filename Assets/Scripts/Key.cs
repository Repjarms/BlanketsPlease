using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : Draggable
{
    public Room.RoomType correspondingRoom;
    public Vector2 originalPosition;

    public delegate void DropKeyAction();
    public static event DropKeyAction OnDropKey;
    public AudioSource keyjingle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (isDragging)
        {
            this.gameObject.transform.localScale = new Vector3(1.7f, 1.7f, 0);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 0);
        }
        */
    }

    new public void OnMouseDragEnd()
    {
        base.OnMouseDragEnd();
        keyjingle.Play();
        OnDropKey?.Invoke();
        ResetToOrigin();
    }

    public void ResetToOrigin()
    {
        RectTransform transform = this.gameObject.GetComponent<RectTransform>();
        transform.anchoredPosition = originalPosition;
    }
}
