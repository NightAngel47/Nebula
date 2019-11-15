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
    /// The master painting camera
    /// </summary>
    [SerializeField, Tooltip("The master painting camera")] 
    private Camera masterPaintCamera;
    /// <summary>
    /// Array of the color prefabs: Red, Green, Blue
    /// </summary>
    [SerializeField, Tooltip("Array of the color prefabs: Red, Green, Blue")]
    private GameObject[] colorPrefabs = new GameObject[3];
    /// <summary>
    /// The amount that the UVPos and Mask Cam displace after placing a color prefab
    /// </summary>
    private float displacement = 0.001f;
    [SerializeField, Tooltip("Layers to check")] 
    private LayerMask checkLayers;
    
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
    /// <summary>
    /// The starting selected biome
    /// </summary>
    [SerializeField, Tooltip("The starting selected biome")] 
    private string startBiome;

    #endregion

    #region To Paint Variables
    
    /// <summary>
    /// The name of the mask color
    /// </summary>
    private MaskNames maskColorName;
    /// <summary>
    /// The name of the master color
    /// </summary>
    private MaskNames masterColorName;
    
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
        
        ChangeBiome(startBiome);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            SpawnBiome(cursorPos);
        }
    }

    /// <summary>
    /// Changes the selected biome to the biome passed in. Called by UI button
    /// </summary>
    /// <param name="biomeName">The name of the biome that the selected biome is changing to</param>
    public void ChangeBiome(string biomeName)
    {
        selectedBiome = (BiomeNames) Enum.Parse(typeof(BiomeNames), biomeName);
        
        // Sets ToPaint variables based on selected biome
        switch (selectedBiome)
        {
            case BiomeNames.Plains:
                maskColorName = MaskNames.Red;
                masterColorName = MaskNames.Red;
                break;
            case BiomeNames.Savana:
                maskColorName = MaskNames.Green;
                masterColorName = MaskNames.Red;
                break;
            case BiomeNames.Tropical:
                maskColorName = MaskNames.Blue;
                masterColorName = MaskNames.Red;
                break;
            case BiomeNames.Temperate:
                maskColorName = MaskNames.Red;
                masterColorName = MaskNames.Green;
                break;
            case BiomeNames.Coniferous:
                maskColorName = MaskNames.Green;
                masterColorName = MaskNames.Green;
                break;
            case BiomeNames.Taiga:
                maskColorName = MaskNames.Blue;
                masterColorName = MaskNames.Green;
                break;
            case BiomeNames.Ocean:
                maskColorName = MaskNames.Red;
                masterColorName = MaskNames.Blue;
                break;
            case BiomeNames.Ice:
                maskColorName = MaskNames.Green;
                masterColorName = MaskNames.Blue;
                break;
            default:
                Debug.LogError("Unrecognized Biome Passed In. Name passed in was : " + selectedBiome);
                break;
        }
    }

    /// <summary>
    /// Spawns color prefab at mask and master
    /// </summary>
    /// <param name="inputPos">The position of the hit</param>
    private void SpawnBiome(Vector2 inputPos)
    {
        Ray cursorRay = mainCam.ScreenPointToRay(inputPos);
        
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(cursorRay, ref uvWorldPosition))
        {
            // paint on mask
            Instantiate(colorPrefabs[(int) maskColorName], maskUVPoses[(int) masterColorName].position + uvWorldPosition, Quaternion.identity);
            // paint on master
            Instantiate(colorPrefabs[(int) masterColorName], maskUVPoses[(int) MaskNames.Master].position + uvWorldPosition, Quaternion.identity);
            
            PosDisplacement();
        }
    }
    
    /// <summary>
    /// Determines the position on the UV of the hit object
    /// </summary>
    /// <param name="cursorRay">The Ray of the hit on the object</param>
    /// <param name="uvWorldPosition">The Vector3 to track the position of the hit on the UV</param>
    /// <param name="maskCam">The Camera of the UV mask</param>
    /// <returns></returns>
    private bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition)
    {
        if (Physics.Raycast(cursorRay, out var hit, 1000, checkLayers))
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            var orthographicSize = masterPaintCamera.orthographicSize;
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
    /// Moves mask and master cam uv pos
    /// </summary>
    private void PosDisplacement()
    {
        Vector3 displacementVec = new Vector3(0, 0, displacement);

        foreach (var uvPos in maskUVPoses)
        {
            uvPos.position -= displacementVec;
        }
    }
}
