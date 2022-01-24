// Adapted from a timer solution from the following website:
// https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/#:~:text=Making%20a%20countdown%20timer%20in,need%20to%20be%20calculated%20individually.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    float timeRemaining = 2;
    public static bool gameTimerIsRunning = false;
    public static bool tutorialTimerIsRunning = false;
    public static bool endConditionIsRunning = false;
    public static bool playerWin = false;
    public static bool playerLost = false;

    public Text timeText;
    public Text endConditionText;
    public Text tutorialText;

    public GameObject crusherObject;
    public GameObject contractPaper;
    public GameObject tutorialBackground;
    GameObject can;
    
    AudioSource audioSource;
    public AudioClip gameMusic;
    public AudioClip failedSound;
    public AudioClip succeedSound;

    public float verticalSpeed = 1;
    
    private void Start()
    {
        // initialize audio source and play clip after tutorial ends
        audioSource = crusherObject.GetComponent<AudioSource>();
        audioSource.clip = gameMusic;

        can = GameObject.Find("can");

        // Starts the tutotial automatically
        tutorialTimerIsRunning = true;

        // holds the game timer
        gameTimerIsRunning = false;

        //holds the end condition timer
        endConditionIsRunning = false;

    }

    void Update()
    {
        if (tutorialTimerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                tutorialText.text = "Spacebar - crush cans\n\nDon't miss!";
            }
            else
            {
                Debug.Log("Tutorial is over!");
                tutorialText.text = "";
                Destroy(tutorialBackground);
                timeRemaining = 10;
                audioSource.Play();

                //ends tutorial timer and starts game timer
                tutorialTimerIsRunning = false;
                gameTimerIsRunning = true;
            }
        }

        if (gameTimerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Game time has run out!");
                timeRemaining = 0;

                //ends game timer and starts end condition
                gameTimerIsRunning = false;
                playerWin = true;
                endConditionIsRunning = true;
            }
        }

        if (endConditionIsRunning)
        {
            Debug.Log("Game is over!");
            Destroy(can);

            if (playerWin)
            {
                //play success sounds
                audioSource.clip = succeedSound;
                audioSource.Play();

                //initiate success screen
                endConditionText.color = Color.green;
                endConditionText.text = "Success";
                MoveSpriteUp(contractPaper);
                playerWin = false;
            }
            
            if (playerLost)
            {
                //play failed sounds
                audioSource.clip = failedSound;
                audioSource.Play();

                // Initiate failed screeen
                endConditionText.color = Color.red;
                endConditionText.text = "You're\nfired!";
                MoveSpriteUp(contractPaper);
                playerLost = false;
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

    private void MoveSpriteUp(GameObject other) 
    { 
        other.transform.position = new Vector3(0, -2f, 0);
    }
}