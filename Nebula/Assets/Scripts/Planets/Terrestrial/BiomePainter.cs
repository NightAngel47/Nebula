using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomePainter : MonoBehaviour
{
    #region References

    /// <summary>
    /// The current planet
    /// </summary>
    private GameObject planet;
    /// <summary>
    /// The main camera of the scene
    /// </summary>
    private Camera mainCam;

    #endregion
    
    #region Mask Variables
    
    /// <summary>
    /// The names of the biome masks
    /// </summary>
    private enum MaskNames {Red, Green, Blue, Master};
    
    [Header("Mask Variables")]
    /// <summary>
    /// The positions of the painting poses for the biome masks. 
    /// </summary>
    [SerializeField, Tooltip("The positions of the painting poses for the biome masks. ")]
    private List<Transform> maskUVPoses = new List<Transform>(4);
    /// <summary>
    /// The painting cameras for the uv masks
    /// </summary>
    [SerializeField, Tooltip("The painting cameras for the uv masks")] 
    private List<Camera> maskPainterCams = new List<Camera>(4);
    /// <summary>
    /// Array of the color prefabs: Red, Green, Blue
    /// </summary>
    [SerializeField, Tooltip("Array of the color prefabs: Red, Green, Blue")]
    private GameObject[] colorPrefabs = new GameObject[3];
    
    #endregion

    #region Biome Select

    /// <summary>
    /// The names of the biomes
    /// </summary>
    private enum BiomeNames
    {
        Plains,
        Savana,
        Tropical,
        Coniferous,
        Taiga,
        Temperate,
        Ice,
        Ocean
    };
    /// <summary>
    /// The selected biome
    /// </summary>
    private BiomeNames selectedBiome;

    #endregion

    private void Awake()
    {
        SetupReferences();
    }

    /// <summary>
    /// Sets up overall reference
    /// </summary>
    private void SetupReferences()
    {
        planet = GameObject.FindGameObjectWithTag("Planet");
        mainCam = Camera.main;
    }

    /// <summary>
    /// Changes the selected biome to the biome passed in. Called by UI button
    /// </summary>
    /// <param name="biomeName">The name of the biome that the selected biome is changing to</param>
    public void ChangeBiome(string biomeName)
    {
        selectedBiome = (BiomeNames) Enum.Parse(typeof(BiomeNames), biomeName);
    }
}
