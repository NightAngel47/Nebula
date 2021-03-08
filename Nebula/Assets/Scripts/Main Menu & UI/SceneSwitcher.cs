using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Button SwitchTerrestrial;
    public Button SwitchGas;
    public Button SwitchCredits;
    public GameObject LoadingFrame;
    public GameObject OtherUI1;
    public GameObject OtherUI2;
    void Start()
    {
        SwitchTerrestrial.onClick.AddListener(TaskOnClickTerrestrial);
        SwitchGas.onClick.AddListener(TaskOnClickGas);
        SwitchCredits.onClick.AddListener(TaskOnClickCredits);
        OtherUI1.SetActive(true);
        OtherUI2.SetActive(true);
    }

    // Update is called once per frame
    void TaskOnClickTerrestrial()
    {
        StartLoadingScreen();
        GameManager.SceneToLoad = "TerrestrialCreator";
    }
    void TaskOnClickGas()
    {
        StartLoadingScreen();
        GameManager.SceneToLoad = "GasCreatorFluids";
    }
    
    void TaskOnClickCredits()
    {
        SceneManager.LoadScene("Credits Screen");
    }

    void StartLoadingScreen()
    {
        OtherUI1.SetActive(false);
        OtherUI2.SetActive(false);
        LoadingFrame.SetActive(true);
        SceneManager.LoadScene(sceneName: "Loading Screen");
    }
}
