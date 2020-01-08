using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IdleTimer : MonoBehaviour
{
    [SerializeField, Tooltip("The number of seconds that the game will allow to be idle before returning to the main menu.")]
    private float idleWaitTime = 330;
    [SerializeField, Tooltip("The last number of seconds that the game will count down before returning to the main menu. ")]
    private float countDownTime = 30;
    
    [SerializeField, Tooltip("The text to display the count down timer before going back to the main menu.")]
    private TMP_Text countDownText;
    [SerializeField, Tooltip("The message displayed with the count down time.")]
    private string countDownMessage = "Returning to the main menu in: ";
    
    /// <summary>
    /// The current idle time
    /// </summary>
    private float currentIdleTime = 300;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetIdleTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResetIdleTimer();
        }

        RunIdleTimer();
    }

    /// <summary>
    /// Runs the idle timer
    /// </summary>
    void RunIdleTimer()
    {
        currentIdleTime -= Time.deltaTime;
        if (currentIdleTime <= countDownTime)
        {
            RunCountDown();

            if (currentIdleTime <= 0)
            {
                ReturnToMainMenu();
            }
        }
    }

    /// <summary>
    /// Resets the idle timer
    /// </summary>
    void ResetIdleTimer()
    {
        currentIdleTime = idleWaitTime;
        countDownText.gameObject.SetActive(false);
    }

    /// <summary>
    /// Starts the count down timer
    /// </summary>
    void RunCountDown()
    {
        if (!countDownText.gameObject.activeSelf)
        {
            countDownText.gameObject.SetActive(true);
        }
        
        countDownText.text = countDownMessage + (int) currentIdleTime + " seconds.";
    }

    /// <summary>
    /// Returns to the main menu. Called when idle for too long.
    /// </summary>
    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
