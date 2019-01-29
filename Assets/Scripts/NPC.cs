using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPC : MonoBehaviour
{
    public enum CharacterName
    {
        Obsidian,
        Addam,
        Wilbur,
        Inky,
        BooBoo,
        Damien
    }

    public bool hasTakenKey;
    public string characterName;
    public Room.RoomType occupiedRoom = Room.RoomType.None;

    public bool hasFinishedWalkingIn = false;
    public MoveIn moveHandler;
    public GameObject objectInRange = null;

    public string CheckInConversationId;
    public string RoomReactionConversationId;

    public delegate void TakeKeyAction(NPC character, Key key);
    public static event TakeKeyAction OnTakeKey;

    // Start is called before the first frame update
    void Start()
    {
        moveHandler = this.gameObject.GetComponent<MoveIn>();
        Key.OnDropKey += OnKeyDropHandler;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key" && moveHandler.hasFinishedWalking == true)
        {
            objectInRange = collision.gameObject;
            Debug.Log("NPC collision detected");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Key" && moveHandler.hasFinishedWalking == true)
        {
            objectInRange = null;
            Debug.Log("NPC collision exited");
        }
    }


    public void OnKeyDropHandler()
    {
        if (objectInRange != null)
        {
            TakeKey(objectInRange);
        }
    }

    public void TakeKey(GameObject keyGameObject)
    {
        Key keyController = keyGameObject.GetComponent<Key>();

        if (hasTakenKey == false)
        {
            hasTakenKey = true;

            // fire the taken key event
            OnTakeKey?.Invoke(this.gameObject.GetComponent<NPC>(), keyController);

            keyGameObject.SetActive(false);
        }
    }

    public void MoveIntoRoom(Room.RoomType roomType)
    {
        occupiedRoom = roomType;
    }
}
