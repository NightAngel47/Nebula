using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Button SwitchTerrestrial;
    public Button SwitchGas;
    public GameObject LoadingFrame;
    public GameObject OtherUI1;
    public GameObject OtherUI2;
    void Start()
    {
        SwitchTerrestrial.onClick.AddListener(TaskOnClickTerrestrial);
        SwitchGas.onClick.AddListener(TaskOnClickGas);
        LoadingFrame.SetActive(false);
        OtherUI1.SetActive(true);
        OtherUI2.SetActive(true);
    }

    // Update is called once per frame
    void TaskOnClickTerrestrial()
    {
        LoadingFrame.SetActive(true);
        OtherUI1.SetActive(false);
        OtherUI2.SetActive(false);
        SceneManager.LoadScene(sceneName: "TerrestrialCreator");
    }
    void TaskOnClickGas()
    {
        LoadingFrame.SetActive(true);
        OtherUI1.SetActive(false);
        OtherUI2.SetActive(false);
        SceneManager.LoadScene(sceneName: "GasCreator");
    }
}
