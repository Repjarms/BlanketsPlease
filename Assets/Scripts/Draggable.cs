using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : Interactable
{
    public bool isDragging = false;
    public bool isDraggingAllowed = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDragBegin()
    {
        if (isDraggingAllowed)
        {
            isDragging = true;
        }

        Debug.Log("We are now dragging");
    }

    public void OnMouseDragEnd()
    {
        isDragging = false;

        Debug.Log("We are no longer dragging");
    }

    public void OnMouseDrag()
    {
        if (isDragging)
        {
            this.gameObject.transform.position = Input.mousePosition;
        }
    }


}
