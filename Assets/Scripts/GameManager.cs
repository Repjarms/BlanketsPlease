using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    // initialize character
    public GameObject[] characterGameObjects = new GameObject[6];
    public Dictionary<NPC.CharacterName, GameObject> characters = new Dictionary<NPC.CharacterName, GameObject>();

    // intialize rooms
    public Room[] roomArray;
    public Dictionary<Room.RoomType, Room> rooms = new Dictionary<Room.RoomType, Room>();

    // initialize event holders
    public GameObject mainSceneCanvas;
    public InnEvent[] dayOneEvents;
    public InnEvent[] dayTwoEvents;
    public Key[] keys;
    public GameObject hallwayLaunchButton;

    // initialize private vars
    private int score;
    private Queue<Queue<InnEvent>> days;
    private Queue<InnEvent> todaysEvents;
    private GameObject checkInCharacter = null;
    private DialogueSystemTrigger checkInCharacterDialogueTrigger = null;
    private static string mainSceneName = "Character__scripting_design";

    private InnEvent nextEvent;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

        // load up the queue of events
        Queue<InnEvent> dayOneQueue = new Queue<InnEvent>(dayOneEvents);
        Queue<InnEvent> dayTwoQueue = new Queue<InnEvent>(dayTwoEvents);
        days = new Queue<Queue<InnEvent>>();
        days.Enqueue(dayOneQueue);
        days.Enqueue(dayTwoQueue);

        // set up event handler
        NPC.OnTakeKey += OnCharacterTakesKey;
        ReturnToDeskBUtton.OnReturn += HandleReturnToHallway;

        // load array into dictionary
        characters.Add(NPC.CharacterName.Addam, characterGameObjects[0]);
        characters.Add(NPC.CharacterName.BooBoo, characterGameObjects[1]);
        characters.Add(NPC.CharacterName.Damien, characterGameObjects[2]);
        characters.Add(NPC.CharacterName.Inky, characterGameObjects[3]);
        characters.Add(NPC.CharacterName.Obsidian, characterGameObjects[4]);
        characters.Add(NPC.CharacterName.Wilbur, characterGameObjects[5]);

        // load room array into dictionary
        rooms.Add(Room.RoomType.Closet, roomArray[0]);
        rooms.Add(Room.RoomType.KingBed, roomArray[1]);
        rooms.Add(Room.RoomType.Minimalist, roomArray[2]);
        rooms.Add(Room.RoomType.Plush, roomArray[3]);
        rooms.Add(Room.RoomType.Riverview, roomArray[4]);
        rooms.Add(Room.RoomType.Quirky, roomArray[5]);

        // hide the hallway button
        hallwayLaunchButton.SetActive(false);

        // start the game :)
        StartNewDay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartNewDay()
    {
        if (days.Count == 0)
        {
            // TODO: end the game
            // and go to the breakfast scene
        }
        else
        {
            todaysEvents = days.Dequeue();
            HandleNextEvent();
        }
    }

    public void HandleNextEvent()
    {


        if (todaysEvents.Count == 0)
        {
            string sceneName = "End Scene";
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else
        {
            hallwayLaunchButton.SetActive(false);

            nextEvent = todaysEvents.Dequeue();

            switch (nextEvent.type)
            {
                case InnEvent.EventType.CheckIn:
                    HandleCheckIn(nextEvent);
                    break;
                case InnEvent.EventType.GuestRequest:
                    break;
                case InnEvent.EventType.HallwayEncounter:
                    HandleHallwayEncounter();
                    break;
                case InnEvent.EventType.ManagementBreak:
                    break;
                default:
                    break;
            }
        }
    }

    public void HandleCheckIn(InnEvent checkInEvent)
    {
        /*
         * TODO:
         * 
         * we should read which character it involves
         * and then find the corresponding gameobject
         * and then animate their entrance
         * 
         * once we've done that, we should trigger their dialog
         * 
         * after the dialog is processed, we should wait to give them
         * a key.
         * 
         * Once they have received a key, we should process their exit
         */

        // we only have one item in involved characters for a checkin
        checkInCharacter = checkInEvent.involvedCharacters[0].gameObject;
        checkInCharacterDialogueTrigger = checkInCharacter.GetComponent<DialogueSystemTrigger>();
        MoveIn moveInHandler = checkInCharacter.GetComponent<MoveIn>();

        MoveIn.OnFinished += MoveInAnimationFinished;
        moveInHandler.StartAnimation();
    }

    public void OnCharacterTakesKey(NPC character, Key key)
    {
        character.MoveIntoRoom(key.correspondingRoom);

        // TODO: on exit animation
        MoveOut moveOut = character.gameObject.GetComponent<MoveOut>();
        moveOut.StartAnimating();

        // we always end by handling the next event
        HandleNextEvent();
    }

    public void MoveInAnimationFinished()
    {
        checkInCharacterDialogueTrigger.OnUse();
    }

    public void HandleGuestRequest()
    {
        // we always end by handling the next event
        HandleNextEvent();
    }

    public void HandleHallwayEncounter()
    {
        hallwayLaunchButton.SetActive(true);
    }

    public void OnHallwayButtonClick()
    {
        LoadHallwayScene(nextEvent);
    }

    private void LoadHallwayScene(InnEvent innEvent)
    {
        Debug.Log(innEvent.sceneName);

        SceneManager.LoadScene(innEvent.sceneName, LoadSceneMode.Additive);
        Scene sceneToLoad = SceneManager.GetSceneByName(innEvent.sceneName);

        StartCoroutine(WaitForSceneLoad(SceneManager.GetSceneByName(innEvent.sceneName)));
        mainSceneCanvas.SetActive(false);
    }

    public IEnumerator WaitForSceneLoad(Scene scene)
    {
        while (!scene.isLoaded)
        {
            yield return null;
        }
        Debug.Log("Setting active scene..");
        SceneManager.SetActiveScene(scene);
    }

    public void HandleReturnToHallway()
    {
        StartCoroutine(ReturnFromHallway(nextEvent.sceneName));
    }

    public IEnumerator ReturnFromHallway(string sceneToUnload)
    {
        Debug.Log(nextEvent.sceneName);
        AsyncOperation sceneUnload = SceneManager.UnloadSceneAsync(sceneToUnload);

        yield return sceneUnload;

        Scene activeScene = SceneManager.GetSceneByName(mainSceneName);
        SceneManager.SetActiveScene(activeScene);
        mainSceneCanvas.SetActive(true);
        // we always end by handling the next event
        HandleNextEvent();

    }

    public void HandleManagementBreak()
    {
        // we always end by handling the next event
        HandleNextEvent();
    }

}