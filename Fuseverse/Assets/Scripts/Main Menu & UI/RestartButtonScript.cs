using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public Button ExitButton;
    public Button FinishButton;

    void Start()
    {
        ExitButton.onClick.AddListener(TaskOnClickExit);
        FinishButton.onClick.AddListener(TaskOnClickFinish);

    }

    // Update is called once per frame
    void TaskOnClickExit()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
    void TaskOnClickFinish()
    {
        SceneManager.LoadScene(sceneName: "Complete Screen");
    }
}
