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
    public Button selectionTwo;

    private float inputTimer;

    void Start()
    {
        selectionOne.onClick.AddListener(onClickAtmosphere);
        selectionTwo.onClick.AddListener(onClickAtmosphere);

        inputTimer = 11;
    }

    void Update()
    {
        inputTimer += Time.deltaTime;
        //INPUT CHECK
        if (Input.touchCount > 0 || Input.GetKeyDown("space"))
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
            else
            {
            dragHands.SetActive(false);
            }
    }

    void onClickAtmosphere()
    {
        inputTimer = 11;
    }

}