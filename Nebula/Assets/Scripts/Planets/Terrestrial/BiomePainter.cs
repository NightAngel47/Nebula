using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BiomePainter : MonoBehaviour
{
    #region References

    /// <summary>
    /// The main camera of the scene
    /// </summary>
    private Camera mainCam;
    /// <summary>
    /// Tracks selected tool
    /// </summary>
    private ToolSelect toolSelect;
    /// <summary>
    /// Placement audio
    /// </summary>
    private PlacementAudio placementAudio;
    /// <summary>
    /// Reference to analytics for tracking
    /// </summary>
    private AnalyticsEvents analytics;

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
    [SerializeField, Tooltip("List of uv quads")]
    private MeshRenderer[] maskQuads = new MeshRenderer[4];
    [SerializeField, Tooltip("List of materials colors for uv quads")]
    private Material[] colorMaterials = new Material[3];
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
    [SerializeField, Tooltip("Biome layers to check")] 
    private LayerMask biomeCheckLayers;
    [SerializeField, Tooltip("Terrain layers to check")] 
    private LayerMask terrainCheckLayers;
    
    #endregion

    #region Biome Select

    /// <summary>
    /// The names of the biomes
    /// </summary>
    public enum BiomeNames
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
    [SerializeField, Tooltip("The amount of terrain objects to check per biome placement")]
    private int terrainCheckSize = 10;

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
        mainCam = Camera.main;
        toolSelect = FindObjectOfType<ToolSelect>();
        placementAudio = FindObjectOfType<PlacementAudio>();
        analytics = FindObjectOfType<AnalyticsEvents>();
    }

    private void Start()
    {
        ChangeBiome(startBiome);
        DefaultBiome();
    }

    private void Update()
    {
        if (Input.touchCount == 1 && Input.touchCount != 2 && Input.GetTouch(0).phase == TouchPhase.Moved && toolSelect.toolSelected == ToolSelect.Tools.Biome)
        {
            Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            SpawnBiome(cursorPos);
            placementAudio.PlayPlacementAudio();
        }
        else
        {
            placementAudio.StopPlacementAudio();
        }

        #region debug painting controls
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Input.GetMouseButton(0) && toolSelect.toolSelected == ToolSelect.Tools.Biome && DebugController.DebugEnabled)
        {
            Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
            SpawnBiome(cursorPos);
            placementAudio.PlayPlacementAudio();
        }
        else
        {
            placementAudio.StopPlacementAudio();
        }
        
#endif
        #endregion
    }

    /// <summary>
    /// Changes the selected biome to the biome passed in. Called by UI button
    /// </summary>
    /// <param name="biomeName">The name of the biome that the selected biome is changing to</param>
    public void ChangeBiome(string biomeName)
    {
        selectedBiome = (BiomeNames) Enum.Parse(typeof(BiomeNames), biomeName);
        
        // select audio
        placementAudio.ChooseBiomeAudio(selectedBiome);
        
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
                Debug.LogError("Unrecognized Biome Passed In. Name passed in was: " + selectedBiome);
                break;
        }
    }

    /// <summary>
    /// Changes default biome of planet at start
    /// </summary>
    private void DefaultBiome()
    {
        var biomeValues = Enum.GetValues(typeof(BiomeNames));
        BiomeNames defualtBiome = (BiomeNames) biomeValues.GetValue(Random.Range(0, biomeValues.Length));
        
        // send analytics
        analytics.SetStartingBiome(selectedBiome.ToString());
        
        switch (defualtBiome)
        {
            case BiomeNames.Plains:
                maskQuads[(int) MaskNames.Red].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Red].tag = MaskNames.Red.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Red.ToString();
                
                break;
            case BiomeNames.Savana:
                maskQuads[(int) MaskNames.Red].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Red].tag = MaskNames.Green.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Red.ToString();
                
                break;
            case BiomeNames.Tropical:
                maskQuads[(int) MaskNames.Red].material = colorMaterials[(int) MaskNames.Blue];
                maskQuads[(int) MaskNames.Red].tag = MaskNames.Blue.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Red.ToString();
                
                break;
            case BiomeNames.Temperate:
                maskQuads[(int) MaskNames.Green].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Green].tag = MaskNames.Red.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Green.ToString();
                
                break;
            case BiomeNames.Coniferous:
                maskQuads[(int) MaskNames.Green].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Green].tag = MaskNames.Green.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Green.ToString();
                
                break;
            case BiomeNames.Taiga:
                maskQuads[(int) MaskNames.Green].material = colorMaterials[(int) MaskNames.Blue];
                maskQuads[(int) MaskNames.Green].tag = MaskNames.Blue.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Green.ToString();
                
                break;
            case BiomeNames.Ocean:
                maskQuads[(int) MaskNames.Blue].material = colorMaterials[(int) MaskNames.Red];
                maskQuads[(int) MaskNames.Blue].tag = MaskNames.Red.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Blue];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Blue.ToString();
                
                break;
            case BiomeNames.Ice:
                maskQuads[(int) MaskNames.Blue].material = colorMaterials[(int) MaskNames.Green];
                maskQuads[(int) MaskNames.Blue].tag = MaskNames.Green.ToString();
                
                maskQuads[(int) MaskNames.Master].material = colorMaterials[(int) MaskNames.Blue];
                maskQuads[(int) MaskNames.Master].tag = MaskNames.Blue.ToString();
                
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
        if (!HitTestUVPosition(cursorRay, ref uvWorldPosition)) return;
        
        // paint on mask
        Instantiate(colorPrefabs[(int) maskColorName], maskUVPoses[(int) masterColorName].position + uvWorldPosition, Quaternion.identity, maskQuads[(int) masterColorName].transform);
        // paint on master
        Instantiate(colorPrefabs[(int) masterColorName], maskUVPoses[(int) MaskNames.Master].position + uvWorldPosition, Quaternion.identity, maskQuads[(int) MaskNames.Master].transform);

        PosDisplacement();
        
        // send analytics
        analytics.IncreaseBiomeCount(selectedBiome.ToString());
        
        // check for terrain in sphere radius of hit
        RaycastHit[] hits = new RaycastHit[terrainCheckSize];
        for(int i = 0; i < Physics.SphereCastNonAlloc(cursorRay, 0.05f, hits, 50f, terrainCheckLayers, QueryTriggerInteraction.Collide); ++i)
        {
            if (!(hits[i].point.z <= 0)) continue;
            
            hits[i].collider.GetComponent<TerrainBehaviour>().CheckBiome();
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
        if (Physics.Raycast(cursorRay, out var hit, 1000, biomeCheckLayers))
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
