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
    /// <summary>
    /// The positions of the painting poses for the biome masks. 
    /// </summary>
    private List<Transform> maskUVPoses;
    /// <summary>
    /// The painting cameras for the uv masks
    /// </summary>
    private List<Camera> maskPainterCams;

    #endregion

    private void Awake()
    {
        SetupReferences();
        SetupMasks();
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
    /// Sets up the masks for the terrain shader
    /// </summary>
    private void SetupMasks()
    {
        // Gets the transforms of the UVPoses
        foreach (var uvPos in GameObject.FindGameObjectsWithTag("UVPos"))
        {
            maskUVPoses.Add(uvPos.transform);
        }
        
        // Gets painter cams
        foreach (var cam in GetComponentsInChildren<Camera>())
        {
            maskPainterCams.Add(cam);
        }
    }
}
