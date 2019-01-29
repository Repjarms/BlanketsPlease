using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveIn : MonoBehaviour
{

    public float speed = 10.0f;
    public float flipTimer = 2.0f;
    public GameObject hand;
    public bool hasFinishedWalking = false;

    public delegate void MoveInFinishAction();
    public static event MoveInFinishAction OnFinished;

    public float journeyLength = 20f;
    private Vector2 targetPosition;
    private Vector2 currentPosition;

    public float timeElapsed = 0f;
    private float timer;
    private float startTime;
    private float canvasXOffset;
    private float canvasYOffset;
    public bool isAnimating = false;

    public AudioSource bellSound;

    void Start()
    {

        targetPosition = new Vector2(183f, 24f);
        currentPosition = this.gameObject.transform.position;

        startTime = Time.time;
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

            Debug.Log(fracJourney);

            transform.position = Vector2.Lerp(
                new Vector2(-600f + canvasXOffset, 0f + canvasYOffset),
                new Vector2(300f + canvasXOffset, 24f + canvasYOffset),
                fracJourney);

            if (fracJourney > 1.2)

            {
                hand.SetActive(true);
                bellSound.Play();
                //trigger bell sound here

                isAnimating = false;
                hasFinishedWalking = true;
                OnFinished?.Invoke();
            }

            if (fracJourney > 1.3)
            {
                hand.SetActive(false);
            }

            if (fracJourney > 1.75)
            {

                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    Flip();
                    timer = flipTimer;
                }
            }
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;
    }

    public void StopAnimation()
    {
        isAnimating = false;
    }

    void Flip()
    {
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
