using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Innterface : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] roomImages = new GameObject[6];

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
    }
}
