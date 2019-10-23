using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugController : MonoBehaviour
{
    /// <summary>
    /// The Instance of the Debug Controller
    /// </summary>
    public static DebugController Instance;
    /// <summary>
    /// The state of the debug controls. True is on, False is off.
    /// </summary>
    public static bool DebugEnabled;
    
    /// <summary>
    /// The Debug UI Canvas
    /// </summary>
    private GameObject debugCanvas;
    /// <summary>
    /// The Debug Mode Text
    /// </summary>
    private GameObject debugModeText;
    /// <summary>
    /// The Debug Controls Panel
    /// </summary>
    private GameObject debugPanel;

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Instance == null)
        {
            // Sets instance settings
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DebugEnabled = false;

            // Gets the references
            debugCanvas = GetComponentInChildren<Canvas>().gameObject; //gets first canvas (debug canvas)
            debugPanel = debugCanvas.GetComponentInChildren<Image>().gameObject; // gets first image (debug panel)
            debugModeText = debugCanvas.GetComponentInChildren<TMP_Text>().gameObject; // gets first text (debug mode text)
            
            // Sets Debug UI to false
            debugPanel.SetActive(false);
            debugModeText.SetActive(false);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #else
        // Destroys debug manager and children if not in UNITY_EDITOR or DEVELOPMENT_BUILD
        Destroy(gameObject);
        #endif
    }

    // Update is called once per frame
    void Update()
    {
        DebugCheck();
        ShowHideDebugPanel();
        ShowHideDebugCanvas();
    }

    /// <summary>
    /// Toggles Debug Mode
    /// </summary>
    private void DebugCheck()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            DebugEnabled = !DebugEnabled;
            debugModeText.SetActive(!debugModeText.activeSelf);
            debugPanel.SetActive(false);
            Debug.Log("Debug Mode Enabled " + DebugEnabled);
        }
    }

    /// <summary>
    /// Toggles Debug Panel state
    /// </summary>
    private void ShowHideDebugPanel()
    {
        if (DebugEnabled && Input.GetKeyDown(KeyCode.F2))
        {
            debugPanel.SetActive(!debugPanel.activeSelf);
        }
    }
    
    /// <summary>
    /// Toggles Debug Canvas
    /// </summary>
    private void ShowHideDebugCanvas()
    {
        if (DebugEnabled && Input.GetKeyDown(KeyCode.C))
        {
            debugCanvas.SetActive(!debugCanvas.activeSelf);
        }
    }
}