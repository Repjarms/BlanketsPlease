using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToDeskBUtton : MonoBehaviour
{

    public delegate void ReturnToDeskAction();
    public static event ReturnToDeskAction OnReturn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDeskReturnClick()
    {
        OnReturn?.Invoke();
    }

    public void EndGame()
    {
        string sceneName = "EndScene";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
