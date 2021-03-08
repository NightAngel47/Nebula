using System.Collections;
using FluidDynamics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    private Transform planetRenderTexture;
    
    private void Start()
    {
        UpdatePlanets();
    }

    public void UpdatePlanets()
    {
        if ((SceneManager.GetActiveScene().name == "TerrestrialCreator" || 
            SceneManager.GetActiveScene().name == "GasCreatorFluids") &&
            SceneManager.GetActiveScene().name != "Complete Screen")
        {
            DontDestroyOnLoad(gameObject);
        }
        else if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            DestroyRenderTexture();
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Complete Screen")
        {
            DetachRenderTexture();
            // make planet spin
            gameObject.AddComponent<planetSpin>();
        }
    }

    /// <summary>
    /// Attaches render texture to planet
    /// </summary>
    public void AttachRenderTexture()
    {
        planetRenderTexture = SceneManager.GetActiveScene().name == "TerrestrialCreator" ? FindObjectOfType<BiomePainter>().transform : FindObjectOfType<Main_Fluid_Simulation>().transform.parent;

        planetRenderTexture.SetParent(transform);
    }

    /// <summary>
    /// Detaches render texture from planet
    /// </summary>
    private void DetachRenderTexture()
    {
        if (planetRenderTexture == null) return;
        planetRenderTexture.transform.SetParent(null);
    }

    /// <summary>
    /// Destroys render texture
    /// </summary>
    private void DestroyRenderTexture()
    {
        if (planetRenderTexture == null) return;
        Destroy(planetRenderTexture.gameObject);
    }
}
