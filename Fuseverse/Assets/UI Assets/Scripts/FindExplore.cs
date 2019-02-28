using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FindExplore : MonoBehaviour
{
    //Defining Main UI Buttons
    public Button Find;
    public Button Explore;
    public Button Exit;
    public Button Confirm;


    //Defining Canvas Renderers.
    public GameObject Code;


    void Start()
    {
        //Setting listeners for each button to reduce UI load
        Find.onClick.AddListener(TaskOnClickFind);
        Explore.onClick.AddListener(TaskOnClickExplore);
        Exit.onClick.AddListener(TaskOnClickExit);

        //Setting no menu
        Code.SetActive(false);
 
    }

    //These Will Switch Right UI presets as well as modifying the Tool Tips

    void TaskOnClickFind()
    {
        Debug.Log("Changing Menu Code");
        Code.SetActive(true);
    }
    void TaskOnClickExplore()
    {
        SceneManager.LoadScene(sceneName: "UniverseView");
    }

    void TaskOnClickExit()
    {
        Code.SetActive(false);
    }
    void TaskOnClickFinish()
    {
        SceneManager.LoadScene(sceneName: "UniverseView");
    }

}
