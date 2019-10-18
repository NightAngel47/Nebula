using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCanvas : MonoBehaviour
{
    public static DebugCanvas instance;

    // Start is called before the first frame update
    void Start()
    {
        #region debug canvas
        #if UNITY_EDITOR
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        #endif
        #endregion

        #if UNITY_STANDALONE && !UNITY_EDITOR
        Destroy(gameObject);
        #endif
    }
}
