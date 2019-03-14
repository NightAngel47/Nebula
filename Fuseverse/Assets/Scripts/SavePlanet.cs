using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    private void Start()
    {
        UpdatePlanets();
    }

    public void UpdatePlanets()
    {
        if ((SceneManager.GetActiveScene().name == "TerrestrialCreator" || 
            SceneManager.GetActiveScene().name == "GasCreator") &&
            SceneManager.GetActiveScene().name != "Complete Screen")
        {
            DontDestroyOnLoad(gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Complete Screen")
        {
            transform.position = GameObject.FindGameObjectWithTag("FinishPos").transform.position;
            transform.localScale = GameObject.FindGameObjectWithTag("FinishPos").transform.localScale;
            gameObject.AddComponent<planetSpin>();
        }
    }
}
