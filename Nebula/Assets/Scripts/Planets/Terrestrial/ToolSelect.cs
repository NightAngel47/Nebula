using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelect : MonoBehaviour
{
    /// <summary>
    /// Placement audio
    /// </summary>
    private PlacementAudio placementAudio;

    private void Start()
    {
        placementAudio = FindObjectOfType<PlacementAudio>();
    }

    /// <summary>
    /// The different tools for the terrestrial planet
    /// </summary>
    public enum Tools
    {
        None,
        Biome,
        Terrain,
        Atmosphere
    };

    /// <summary>
    /// The currently selected tool
    /// </summary>
    public Tools toolSelected = Tools.Biome;

    /// <summary>
    /// Changes selected tool to None
    /// </summary>
    public void NoneSelected()
    {
        toolSelected = Tools.None;
        placementAudio.StopPlacementAudio();
    }

    /// <summary>
    /// Changes selected tool to Biome
    /// </summary>
    public void BiomeSelected()
    {
        toolSelected = Tools.Biome;
    }
    
    /// <summary>
    /// Changes selected tool to Terrain
    /// </summary>
    public void TerrainSelected()
    {
        toolSelected = Tools.Terrain;
    }
    
    /// <summary>
    /// Changes selected tool to Atmosphere
    /// </summary>
    public void AtmosphereSelected()
    {
        toolSelected = Tools.Atmosphere;
    }
}
