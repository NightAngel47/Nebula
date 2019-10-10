using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    public static bool debugEnabled;
    private bool currentlyEnabled;

    public static DebugController instance;

    void OnAwake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            debugEnabled = false;
            currentlyEnabled = false;
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
    }

    /// <summary>
    /// 
    /// </summary>
    private void DebugCheck()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if(currentlyEnabled)
            {
                debugEnabled = false;
                currentlyEnabled = false;
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            else if(!currentlyEnabled)
            {
                debugEnabled = true;
                currentlyEnabled = true;
                Debug.Log("Debug Mode Enabled " + debugEnabled);
            }
            
        }
    }
}
