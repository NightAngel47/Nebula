using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenUIChanger : MonoBehaviour
{
    //Defining Main UI Buttons
    public Button Terrain_Button;
    public Button Biomes_Button;
    public Button Atmosphere_Button;
    public Button Finish_Button;

    //Defining Canvas Renderers.
    public GameObject terrainCanvas;
    public GameObject colorCanvas;
    public GameObject atmosphereCanvas;
    public GameObject finishCanvas;

    public GameObject RightBlurGas;

    private AnalyticsEvents ae;
    void Start()
    {
        ae = FindObjectOfType<AnalyticsEvents>();
        
        //Setting listeners for each button to reduce UI load
        Terrain_Button.onClick.AddListener(onClickTerrain);
        Biomes_Button.onClick.AddListener(onClickBiomes);
        Atmosphere_Button.onClick.AddListener(onClickAtmosphere);
        Finish_Button.onClick.AddListener(onClickFinish);

        //Setting no menu
        terrainCanvas.SetActive(false);
        colorCanvas.SetActive(true);
        atmosphereCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        RightBlurGas.SetActive(true);
        
        // track feature states
        ae.SetFeatureActiveStates(true, false, false);
    }

    //These Will Switch Right UI presets as well as modifying the Tool Tips

    void onClickTerrain()
    {
        Debug.Log("Changing Menu to Terrain Config");
        terrainCanvas.SetActive(true);
        colorCanvas.SetActive(false);
        atmosphereCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        RightBlurGas.SetActive(true);

        // track feature states
        ae.SetFeatureActiveStates(false, true, false);
    }
    void onClickBiomes()
    {
        Debug.Log("Changing Menu to Biome Config");
        terrainCanvas.SetActive(false);
        colorCanvas.SetActive(true);
        atmosphereCanvas.SetActive(false);
        finishCanvas.SetActive(false);
        RightBlurGas.SetActive(true);

        // track feature states
        ae.SetFeatureActiveStates(true, false, false);
    }
    void onClickAtmosphere()
    {
        Debug.Log("Changing Menu to Atmosphere Config");
        terrainCanvas.SetActive(false);
        colorCanvas.SetActive(false);
        atmosphereCanvas.SetActive(true);
        finishCanvas.SetActive(false);
        RightBlurGas.SetActive(false);
        
        // track feature states
        ae.SetFeatureActiveStates(false, false, true);
    }
    void onClickFinish()
    {
        terrainCanvas.SetActive(false);
        colorCanvas.SetActive(false);
        atmosphereCanvas.SetActive(false);
        finishCanvas.SetActive(true);
        RightBlurGas.SetActive(true);
        
        // track feature states
        ae.SetFeatureActiveStates(false, false, false);
    }

}
