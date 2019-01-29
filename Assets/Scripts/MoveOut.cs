using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOut : MonoBehaviour
{

    private float journeyLength = 280;
    public float speed = 10.0f;
    private float startTime;

    private float canvasXOffset;
    private float canvasYOffset;
    public bool isAnimating = false;

    public float flipTimer = 2.0f;
    private float timer;
    private float timeElapsed = 0f;

    void Start()
    {
        canvasXOffset = this.gameObject.GetComponentInParent<Canvas>().transform.position.x;
        canvasYOffset = this.gameObject.GetComponentInParent<Canvas>().transform.position.y;
    }

    void Update()
    {
        if (isAnimating == true)
        {
            timeElapsed += Time.deltaTime;
            float distcovered = timeElapsed * speed;
            float fracJourney = distcovered / journeyLength;

            transform.position = Vector2.Lerp(
                new Vector2(183f + canvasXOffset, 24f + canvasYOffset),
                new Vector2(900 + canvasXOffset, 24f + canvasYOffset),
                fracJourney);
        }
    }

    public void StartAnimating()
    {
        isAnimating = true;
    }

    public void StopAnimating()
    {
        isAnimating = false;
    }
}
