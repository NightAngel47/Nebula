using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private enum terrainTools
    {
        none,
        up,
        plants,
        erase
    };
    /// <summary>
    /// The currently selected terrain tool
    /// </summary>
    private terrainTools terrainToolSelected = terrainTools.plants;
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
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            SpawnTerrain(cursorPos);
        }
    }
    
    /// <summary>
    /// Spawn terrain based on biome at inputPos
    /// </summary>
    /// <param name="inputPos">The position of the input</param>
    private void SpawnTerrain(Vector2 inputPos)
    {
        Ray cursorRay = mainCam.ScreenPointToRay(inputPos);
        
        Vector3 uvWorldPosition = Vector3.zero;
        string masterDecalTag = MaskNames.Green.ToString(); // default color of master
        if (HitTestUVPosition(cursorRay, ref uvWorldPosition, maskPainterCams[(int) MaskNames.Master]))
        {
            // determine which mask cam to check based on master decal tag
            Vector3 checkMaskCamPos = Vector3.zero;
            if (masterDecalTag == MaskNames.Red.ToString())
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Red].transform.position;
            }
            else if (masterDecalTag == MaskNames.Green.ToString())
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Green].transform.position;
            }
            else 
            {
                checkMaskCamPos = maskPainterCams[(int) MaskNames.Blue].transform.position;
            }

            // check mask cam for mask cam decal tag and send to terrain select
            if (Physics.Raycast(checkMaskCamPos, Vector3.forward, out var hit))
            {
                //selectedTerrain = ts.SelectedTerrain(masterDecalTag, hit.collider.tag, true);
            }
        }
    }
    
    /// <summary>
    /// Determines the position on the UV of the hit object
    /// </summary>
    /// <param name="cursorRay">The Ray of the hit on the object</param>
    /// <param name="uvWorldPosition">The Vector3 to track the position of the hit on the UV</param>
    /// <param name="maskCam">The Camera of the UV mask</param>
    /// <param name="decalTag">The tag of the color decal hit</param>
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
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// Changes Terrain Feature Type
    /// </summary>
    /// <param name="tool">Terrain Feature Type</param>
    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
    }
}
