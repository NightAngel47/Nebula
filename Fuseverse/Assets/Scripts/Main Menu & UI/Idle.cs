using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Idle : MonoBehaviour { 
 
    public float timeOut = 20.0f; // Time Out Setting in Seconds
    private float timeOutTimer = 0.0f;

    public GameObject video;
    public GameObject UI;

    private void Start()
    {
        UI.SetActive(true);
        video.SetActive(false);
    }
    void Update()
    {
        timeOutTimer += Time.deltaTime;
        // If screen is tapped, reset timer
        if (Input.GetMouseButtonDown(0))
        {
             timeOutTimer = 0.0f;
             video.SetActive(false);
            UI.SetActive(true);

        }
        // If timer reaches zero, start screensaver
        if (timeOutTimer > timeOut)
        {
            video.SetActive(true);
            UI.SetActive(false);

        }
    }
}