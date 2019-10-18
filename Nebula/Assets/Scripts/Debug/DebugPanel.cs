using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : MonoBehaviour
{
    public static DebugPanel instance;

    // Start is called before the first frame update
    void Start()
    {
        #region debug panel
#if UNITY_EDITOR
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            gameObject.SetActive(false);

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
