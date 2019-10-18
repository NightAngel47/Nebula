#region debug controller
#if UNITY_EDITOR

using UnityEngine;
using UnityEngine.UI;


public class DebugController : MonoBehaviour
{
    public static bool debugEnabled;
    public static bool mouseRotationEnabled;

    private GameObject debugCanvas;
    private Text debugText;
    private Text mouseRotationText;

    private GameObject debugPanel;

    private bool debugCanvasEnabled;
    private bool debugPanelEnabled;


    public static DebugController instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            debugEnabled = false;
            debugCanvasEnabled = true;
            debugPanelEnabled = false;

            debugCanvas = GameObject.FindGameObjectWithTag("DebugCanvas");
            debugPanel = GameObject.FindGameObjectWithTag("DebugPanel");
            debugText = GameObject.FindGameObjectWithTag("DebugModeText").GetComponent<Text>();
            mouseRotationText = GameObject.FindGameObjectWithTag("DebugModeRotationText").GetComponent<Text>();

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugCheck();
        HideDebugPanel();

        if (Input.GetKeyDown(KeyCode.C))
        {
            HideDebugCanvas();
        }        
    }

    /// <summary>
    /// Method switches scene between debug mode and play mode.
    /// </summary>
    private void DebugCheck()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(debugEnabled)
            {
                debugEnabled = false;
                mouseRotationEnabled = false;
                debugPanelEnabled = false;
                DebugText();
                MouseRotationText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            else if(!debugEnabled)
            {
                debugEnabled = true;
                DebugText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            
        }

        if(debugEnabled)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(mouseRotationEnabled)
                {
                    mouseRotationEnabled = false;
                    MouseRotationText();
                }
                else if(!mouseRotationEnabled)
                {
                    mouseRotationEnabled = true;
                    MouseRotationText();
                }
            }
        }
    }

    private void DebugText()
    {
        if(debugEnabled)
        {
            debugText.text = "Debug Mode: True";
        }
        else if(!debugEnabled)
        {
            debugText.text = "Debug Mode: False";
        }
    }

    private void HideDebugPanel()
    {
        if(!debugEnabled)
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

    private void MouseRotationText()
    {
        if(mouseRotationEnabled)
        {
            mouseRotationText.text = "Mouse Rotation: True";
        }
        else if(!mouseRotationEnabled)
        {
            mouseRotationText.text = "Mouse Rotation: False";
        }
    }

    private void HideDebugCanvas()
    {
        if(debugCanvasEnabled)
        {
            debugCanvas.SetActive(false);
            debugCanvasEnabled = false;
        }
        else if(!debugCanvasEnabled)
        {
            debugCanvas.SetActive(true);
            debugCanvasEnabled = true;
        }
    }
}
#endif
#endregion
