using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlanet : MonoBehaviour
{
    [SerializeField, Tooltip("Reference to biome painter for terrestrial texture")] 
    private BiomePainter biomePainter;
    
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
            DestroyBiomePainter();
            Destroy(gameObject);
        }

        if (SceneManager.GetActiveScene().name == "Complete Screen")
        {
            DetachBiomePainter();
            // make planet spin
            gameObject.AddComponent<planetSpin>();
        }
    }

    /// <summary>
    /// Attaches biome painter to planet
    /// </summary>
    public void AttachBiomePainter()
    {
        if (biomePainter == null) return;
        biomePainter.transform.SetParent(transform);
    }

    /// <summary>
    /// Detaches biome painter from planet
    /// </summary>
    private void DetachBiomePainter()
    {
        if (biomePainter == null) return;
        biomePainter.transform.SetParent(null);
    }

    /// <summary>
    /// Destroys biome painter
    /// </summary>
    private void DestroyBiomePainter()
    {
        if (biomePainter == null) return;
        Destroy(biomePainter.gameObject);
    }
}
