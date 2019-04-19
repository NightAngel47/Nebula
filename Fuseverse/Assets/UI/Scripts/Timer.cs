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

    public Button Terrain_Button;
    public Button Biomes_Button;
    public Button Atmosphere_Button;

    public GameObject VerifyFinish;
    public GameObject VerifyExit;

    private float inputTimer;
    

    void Start()
    {
        inputTimer = 11;
        Terrain_Button.onClick.AddListener(onClickButton);
        Biomes_Button.onClick.AddListener(onClickButton);
        Atmosphere_Button.onClick.AddListener(onClickButton);

    }
    void onClickButton()
    {
        inputTimer = 11;
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

        if (VerifyExit.activeSelf == true || VerifyFinish.activeSelf == true)
        {
            inputTimer = 1;
        }
    }


 
}