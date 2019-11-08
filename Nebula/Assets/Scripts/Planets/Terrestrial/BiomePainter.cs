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
    /// <summary>
    /// The amount that the UVPos and Mask Cam displace after placing a color prefab
    /// </summary>
    private float displacement = 0.001f;
    
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
    /// The color prefab to be painted
    /// </summary>
    private GameObject maskColorToPaint;
    /// <summary>
    /// The uv pos to paint prefab at
    /// </summary>
    private Transform uvPosToPaint;
    /// <summary>
    /// The camera of the mask for painting
    /// </summary>
    private Camera maskCamToPaint;

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
                maskColorToPaint = colorPrefabs[(int) MaskNames.Red];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Red];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Red];
                break;
            case BiomeNames.Savana:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Green];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Red];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Red];
                break;
            case BiomeNames.Tropical:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Blue];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Red];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Red];
                break;
            case BiomeNames.Temperate:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Red];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Green];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Green];
                break;
            case BiomeNames.Coniferous:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Green];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Green];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Green];
                break;
            case BiomeNames.Taiga:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Blue];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Green];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Green];
                break;
            case BiomeNames.Ocean:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Red];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Blue];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Blue];
                break;
            case BiomeNames.Ice:
                maskColorToPaint = colorPrefabs[(int) MaskNames.Green];
                uvPosToPaint = maskUVPoses[(int) MaskNames.Blue];
                maskCamToPaint = maskPainterCams[(int) MaskNames.Blue];
                break;
            default:
                Debug.LogError("Unrecognized Biome Passed In. Name passed in was : " + selectedBiome);
                break;
        }
    }

    private void SpawnBiome(Vector2 inputPos)
    {
        Ray cursorRay = mainCam.ScreenPointToRay(inputPos);

        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(cursorRay, ref uvWorldPosition, maskCamToPaint))
        {
            Instantiate(maskColorToPaint, uvPosToPaint.position + uvWorldPosition, Quaternion.identity);
            PosDisplacement(uvPosToPaint, maskCamToPaint);
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
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Moves UV Pos and Mask Cam back in order to not run out of room
    /// </summary>
    /// <param name="uvPos">The UV Pos to move back</param>
    /// <param name="maskCam">The Mask Cam to move back</param>
    private void PosDisplacement(Transform uvPos, Camera maskCam)
    {
        Vector3 displacementVec = new Vector3(0, 0, displacement);
        uvPos.position -= displacementVec;
        maskCam.transform.position -= displacementVec;
    }
}
