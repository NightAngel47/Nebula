using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    public Transform rings;

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
            // move planet
            transform.position = GameObject.FindGameObjectWithTag("FinishPos").transform.position;
            transform.localScale = GameObject.FindGameObjectWithTag("FinishPos").transform.localScale;

            // make planet spin
            gameObject.AddComponent<planetSpin>();

            // destroy unnessessary componets
            Destroy(gameObject.GetComponent<Rigidbody>());
            Destroy(gameObject.GetComponent<CameraRotation>());
            Destroy(gameObject.GetComponent<MeshCollider>());
            Destroy(gameObject.GetComponent<GasGiantController>());
            Destroy(gameObject.GetComponent<HideRings>());
            Destroy(gameObject.GetComponent<GasRotation>());

            if (rings != null)
            {
                rings.localScale = GameObject.FindGameObjectWithTag("FinishPos").transform.localScale;
            }
        }
    }
}
