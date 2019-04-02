using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public GameObject dragHands;
    public GameObject atmosphereCanvas;
    public GameObject tapHands;
    public GameObject rotateHands;
    public Button selectionOne;

    private float inputTimer;

    void Start()
    {
        inputTimer = 11;
        selectionOne.onClick.AddListener(onClickAtmosphere);

    }
    void onClickAtmosphere()
    {
        inputTimer = 11;
        Debug.Log("you clicked the button, good for you");
    }
    void FixedUpdate()
    {
        //Debug.Log(inputTimer);

        inputTimer += Time.deltaTime;
        //INPUT CHECK
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            inputTimer = 0;
            tapHands.SetActive(false);
            dragHands.SetActive(false);
            rotateHands.SetActive(false);
        }
        if (inputTimer >= 10f && atmosphereCanvas.activeSelf == false)
        {
            tapHands.SetActive(true);
            rotateHands.SetActive(true);
        }
        else if (inputTimer >= 10f && atmosphereCanvas.activeSelf == true)
        {
            dragHands.SetActive(true);
            rotateHands.SetActive(true);
        }


        if (atmosphereCanvas.activeSelf == true)
        {
            tapHands.SetActive(false);
        }
        if (atmosphereCanvas.activeSelf == false)
            {
            dragHands.SetActive(false);
            }
    }


 
}