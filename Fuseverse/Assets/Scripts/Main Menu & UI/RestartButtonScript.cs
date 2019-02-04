using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartButtonScript : MonoBehaviour
{
    public Button ExitButton;


    void Start()
    {
        ExitButton.onClick.AddListener(TaskOnClickExit);

    }

    // Update is called once per frame
    void TaskOnClickExit()
    {
        SceneManager.LoadScene(sceneName: "Main Menu");
    }
}
