using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonScript : MonoBehaviour
{
    public Button ExitButton;
    public Button FinishButton;
    public Button ConfirmExit;
    public Button ConfirmFinish;
    public Button CancelExit;
    public Button CancelFinish;

    public GameObject verifyExit;
    public GameObject verifyFinish;
    public GameObject OtherUI1;
    public GameObject OtherUI2;


    void Start()
    {
        verifyExit.SetActive(false);
        verifyFinish.SetActive(false);

        ExitButton.onClick.AddListener(TaskOnClickExit);
        FinishButton.onClick.AddListener(TaskOnClickFinish);
        ConfirmExit.onClick.AddListener(TaskOnClickConfirmExit);
        ConfirmFinish.onClick.AddListener(TaskOnClickConfirmFinish);
        CancelExit.onClick.AddListener(TaskOnClickCancelExit);
        CancelFinish.onClick.AddListener(TaskOnClickCancelFinish);
    }

    void TaskOnClickExit()
    {
        verifyExit.SetActive(true);
        OtherUI1.SetActive(false);
        OtherUI2.SetActive(false);
    }
    void TaskOnClickFinish()
    {
        verifyFinish.SetActive(true);
        OtherUI1.SetActive(false);
        OtherUI2.SetActive(false);
    }

    void TaskOnClickCancelExit()
    {
        verifyExit.SetActive(false);
        verifyFinish.SetActive(false);
        OtherUI1.SetActive(true);
        OtherUI2.SetActive(true);
    }
    void TaskOnClickCancelFinish()
    {
        verifyExit.SetActive(false);
        verifyFinish.SetActive(false);
        OtherUI1.SetActive(true);
        OtherUI2.SetActive(true);
    }
    void TaskOnClickConfirmFinish()
    {
        SceneManager.LoadScene("Complete Screen");
    }
    void TaskOnClickConfirmExit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
