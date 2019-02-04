using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Button SwitchTerrestrial;
    public Button SwitchGas;


    void Start()
    {
        SwitchTerrestrial.onClick.AddListener(TaskOnClickTerrestrial);
        SwitchGas.onClick.AddListener(TaskOnClickGas);

    }

    // Update is called once per frame
    void TaskOnClickTerrestrial()
    {
        SceneManager.LoadScene(sceneName: "TerrestrialCreator");
    }
    void TaskOnClickGas()
    {
        SceneManager.LoadScene(sceneName: "GasCreator");

    }
}
