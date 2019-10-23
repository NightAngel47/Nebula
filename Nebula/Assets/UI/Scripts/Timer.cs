using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public GameObject terrainHelp;
    public GameObject biomeHelp;
    public GameObject atmosphereHelp;
    
    public int screenIndex = 0;

    public Button Terrain_Button;
    public Button Biomes_Button;
    public Button Atmosphere_Button;
    public Button Help_Button;


    public GameObject VerifyFinish;
    public GameObject VerifyExit;

    private float inputTimer;
    public float Tmax = 6;

    private AnalyticsEvents ae;

    #region debug timer
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    private Vector3 tempMousePos;
#endif
    #endregion

    void Start()
    {
        #region debug timer
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        tempMousePos = Input.mousePosition;
#endif
        #endregion

        inputTimer = Tmax + 1;
        Terrain_Button.onClick.AddListener(onClickTerrain);
        Biomes_Button.onClick.AddListener(onClickBiomes);
        Atmosphere_Button.onClick.AddListener(onClickAtmosphere);
        Help_Button.onClick.AddListener(onClickHelp);

        ae = FindObjectOfType<AnalyticsEvents>();
    }
    
  
    void FixedUpdate()
    {
        inputTimer += Time.deltaTime;
        if (Input.touchCount > 0 || Input.GetMouseButton(0))
        {
            inputTimer = 0;
            terrainHelp.SetActive(false);
            biomeHelp.SetActive(false);
            atmosphereHelp.SetActive(false);

            ae.SetTutorialActive(false);
        }

        #region debug timer
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (DebugController.DebugEnabled)
        {
            bool mouseMoved = false;

            if (tempMousePos != Input.mousePosition)
            {
                mouseMoved = true;
                tempMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.M) || mouseMoved)
            {
                inputTimer = 0;
                terrainHelp.SetActive(false);
                biomeHelp.SetActive(false);
                atmosphereHelp.SetActive(false);
            }
        }
#endif
        #endregion


        if (VerifyExit.activeSelf == true || VerifyFinish.activeSelf == true)
        {
            
            inputTimer = 0;
        }

    }


    void Update()
    {

        if (screenIndex == 0 && inputTimer >= Tmax)
        {
            terrainHelp.SetActive(true);
            
            ae.SetTutorialActive(true);
        }
        if (screenIndex == 1 && inputTimer >= Tmax)
        {
            biomeHelp.SetActive(true);
            
            ae.SetTutorialActive(true);
        }
        if (screenIndex == 2 && inputTimer >= Tmax)
        {
            atmosphereHelp.SetActive(true);
            
            ae.SetTutorialActive(true);
        }
    }




    void onClickHelp()
    {

        inputTimer = Tmax + 1;

    }






    void onClickTerrain()
    {
        //SI 0
        screenIndex = 0;
        inputTimer = Tmax;
    }
    void onClickBiomes()
    {
        //SI 1
        screenIndex = 1;
        inputTimer = Tmax;

    }
    void onClickAtmosphere()
    {
        //SI 2
        screenIndex = 2;
        inputTimer = Tmax;

    }




    /*

    if (inputTimer >= Tmax && atmosphereCanvas.activeSelf == false)
    {
        tapHands.SetActive(true);
        rotateHands.SetActive(true);
    }
    else if (inputTimer >= Tmax && atmosphereCanvas.activeSelf == true)
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
    */

}


 
