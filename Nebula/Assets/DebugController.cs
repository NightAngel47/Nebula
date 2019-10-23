using UnityEngine;
using UnityEngine.UI;


public class DebugController : MonoBehaviour
{
    public static bool debugEnabled;

    private GameObject debugCanvas;

    private GameObject debugTextObj;

    private GameObject debugPanel;

    private bool debugCanvasEnabled;
    private bool debugPanelEnabled;


    public static DebugController instance;

    // Start is called before the first frame update
    void Start()
    {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            debugEnabled = false;
            debugCanvasEnabled = true;
            debugPanelEnabled = false;

            debugCanvas = GameObject.FindGameObjectWithTag("DebugCanvas");
            debugPanel = GameObject.FindGameObjectWithTag("DebugPanel");
            debugTextObj = GameObject.FindGameObjectWithTag("DebugModeText");

            debugTextObj.SetActive(false);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
#else
        Destroy(gameObject);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        DebugCheck();
        HideDebugPanel();

        if (Input.GetKeyDown(KeyCode.C) && debugEnabled)
        {
            HideDebugCanvas();
        }
    }

    /// <summary>
    /// Method switches scene between debug mode and play mode.
    /// </summary>
    private void DebugCheck()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (debugEnabled)
            {
                debugEnabled = false;
                debugPanelEnabled = false;
                DebugText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            else if (!debugEnabled)
            {
                debugEnabled = true;

                debugCanvasEnabled = true;
                debugCanvas.SetActive(true);

                DebugText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }

        }
    }

    private void DebugText()
    {
        if (debugEnabled)
        {
            debugTextObj.SetActive(true);
        }
        else if (!debugEnabled)
        {
            debugTextObj.SetActive(false);
        }
    }

    private void HideDebugPanel()
    {
        if (!debugEnabled)
        {
            debugPanel.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.F2) && debugEnabled)
        {
            if (debugPanelEnabled)
            {
                debugPanelEnabled = false;
                debugPanel.SetActive(false);
            }
            else if (!debugPanelEnabled)
            {
                debugPanelEnabled = true;
                debugPanel.SetActive(true);
            }
        }
    }

    private void HideDebugCanvas()
    {
        if (debugCanvasEnabled)
        {
            debugCanvas.SetActive(false);
            debugCanvasEnabled = false;
        }
        else if (!debugCanvasEnabled)
        {
            debugCanvas.SetActive(true);
            debugCanvasEnabled = true;
        }
    }
}