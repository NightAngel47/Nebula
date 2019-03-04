using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    void Start()
    {
        if ((SceneManager.GetActiveScene().name == "TerrestrialCreator" || 
            SceneManager.GetActiveScene().name == "GasCreator") &&
            SceneManager.GetActiveScene().name != "Complete Screen")
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
