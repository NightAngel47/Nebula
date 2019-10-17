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

    private bool debugCanvasEnabled;

    private bool currentlyEnabled;

    public static DebugController instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            debugEnabled = false;
            currentlyEnabled = false;
            debugCanvasEnabled = true;

            debugCanvas = GameObject.FindGameObjectWithTag("DebugCanvas");
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

        if(Input.GetKeyDown(KeyCode.C))
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
            if(currentlyEnabled)
            {
                debugEnabled = false;
                currentlyEnabled = false;
                mouseRotationEnabled = false;
                DebugText();
                MouseRotationText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            else if(!currentlyEnabled)
            {
                debugEnabled = true;
                currentlyEnabled = true;
                DebugText();
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            
        }

        if(currentlyEnabled)
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
