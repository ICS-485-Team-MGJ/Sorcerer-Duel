using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{ 
    [SerializeField]private float timeRemaining;
    private bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
        timeRemaining = timeRemaining * 60;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public bool timerHasExpired() {
        if (timerIsRunning == false) {
            return true;
        } else {
            return false;
        }
    }
}
