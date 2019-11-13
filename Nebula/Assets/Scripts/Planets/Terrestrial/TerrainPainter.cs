﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TerrainSelect))]
public class TerrainPainter : MonoBehaviour
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
    /// <summary>
    /// The terrain select script for determining which terrain object to spawn
    /// </summary>
    private TerrainSelect ts;

    #endregion
    
    #region Mask Variables
    
    /// <summary>
    /// The names of the biome masks
    /// </summary>
    private enum MaskNames {Red, Green, Blue, Master};
    
    [Header("Mask Variables")]
    /// <summary>
    /// The painting cameras for the uv masks
    /// </summary>
    [SerializeField, Tooltip("The painting cameras for the uv masks")] 
    private List<Camera> maskPainterCams = new List<Camera>(4);
    
    #endregion

    #region Terrain Variables

    /// <summary>
    /// The terrain options
    /// </summary>
    private enum TerrainTools
    {
        Plants,
        Up,
        Erase
    };
    /// <summary>
    /// The currently selected terrain tool
    /// </summary>
    private TerrainTools terrainToolSelected = TerrainTools.Plants;
    /// <summary>
    /// Selected Terrain object to spawn
    /// </summary>
    private GameObject selectedTerrain;
    /// <summary>
    /// Terrain erase object
    /// </summary>
    [SerializeField, Tooltip("Terrain erase object")]
    private GameObject terrainEraser;

    #endregion

    private void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet");
        mainCam = Camera.main;
        ts = GetComponent<TerrainSelect>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            Ray cursorRay = mainCam.ScreenPointToRay(cursorPos);
            SpawnTerrain(cursorRay);
        }
    }
    
    /// <summary>
    /// Spawn terrain based on biome at inputPos
    /// </summary>
    /// <param name="cursorRay">The ray from input</param>
    private void SpawnTerrain(Ray cursorRay)
    {
        DetermineTerrain(cursorRay);
        SpawnTerrainOnPlanet(cursorRay);
    }

    /// <summary>
    /// Determine the terrain to spawn based on terrain tool selected and biome hit
    /// </summary>
    /// <param name="cursorRay">The position of the hit</param>
    private void DetermineTerrain(Ray cursorRay)
    {
        if (terrainToolSelected == TerrainTools.Erase)
        {
            selectedTerrain = terrainEraser;
        }
        else
        {
            Vector3 uvWorldPosition = Vector3.zero;
            string masterDecalTag = MaskNames.Green.ToString(); // default color of master
            Vector3 checkMaskCamPos;
            if (!HitTestUVPosition(cursorRay, ref uvWorldPosition, maskPainterCams[(int) MaskNames.Master])) return;
            if (!Physics.Raycast(maskPainterCams[(int) MaskNames.Master].transform.position + uvWorldPosition, Vector3.forward, out var hitMaster)) return;
            
            // determine which mask cam to check based on master decal tag
            if (hitMaster.collider.CompareTag(MaskNames.Red.ToString()))
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Red].transform.position;
            }
            else if (hitMaster.collider.CompareTag(MaskNames.Green.ToString()))
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Green].transform.position;
            }
            else 
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Blue].transform.position;
            }
                    
            // check mask cam for mask cam decal tag and send to terrain select
            if (!Physics.Raycast(checkMaskCamPos + uvWorldPosition, Vector3.forward, out var hitMask)) return;
            switch (terrainToolSelected)
            {
                case TerrainTools.Plants:
                    selectedTerrain = ts.SelectedTerrain(masterDecalTag, hitMask.collider.tag, false);
                    break;
                case TerrainTools.Up:
                    selectedTerrain = ts.SelectedTerrain(masterDecalTag, hitMask.collider.tag, true);
                    break;
            }
        }
    }

    /// <summary>
    /// Instantiates selected terrain on the planet at the hit position 
    /// </summary>
    /// <param name="cursorRay"></param>
    private void SpawnTerrainOnPlanet(Ray cursorRay)
    {
        if (Physics.Raycast(cursorRay, out var hit , 1000, 9))
        {
            Instantiate(selectedTerrain, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
        }
    }
    
    /// <summary>
    /// Determines the position on the UV of the hit object
    /// </summary>
    /// <param name="cursorRay">The Ray of the hit on the object</param>
    /// <param name="uvWorldPosition">The Vector3 to track the position of the hit on the UV</param>
    /// <param name="maskCam">The Camera of the UV mask</param>
    /// <returns></returns>
    private bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition, Camera maskCam)
    {
        if (Physics.Raycast(cursorRay, out var hit))
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            var orthographicSize = maskCam.orthographicSize;
            uvWorldPosition.x = pixelUV.x - orthographicSize;//To center the UV on X
            uvWorldPosition.y = pixelUV.y - orthographicSize;//To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Changes Terrain Feature Type
    /// </summary>
    /// <param name="tool">Terrain Feature Type</param>
    public void TerrainOption(string tool)
    {
        terrainToolSelected = (TerrainTools)System.Enum.Parse(typeof(TerrainTools), tool);
    }
}