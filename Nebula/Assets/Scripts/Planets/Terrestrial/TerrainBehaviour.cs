using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    
    /// <summary>
    /// The names of the biome masks
    /// </summary>
    private enum MaskNames {Red, Green, Blue, Master};
    
    [SerializeField, Tooltip("Is terrain up or plant")]
    private bool isUp;
    /// <summary>
    /// Terrain select for determining terrain
    /// </summary>
    private TerrainSelect ts;
    /// <summary>
    /// Terrain painter reference
    /// </summary>
    private TerrainPainter tp;
    /// <summary>
    /// The name of the biome this terrain was spawned on
    /// </summary>
    private string biomeName;
    /// <summary>
    /// The master decal tag for the terrain
    /// </summary>
    public string masterDecalTag;
    /// <summary>
    /// The mask decal tag for the terrain
    /// </summary>
    public string maskDecalTag;
    /// <summary>
    /// The uv pos for master and mask cam
    /// </summary>
    private Vector3 uvPos;
    /// <summary>
    /// Reference to the planet
    /// </summary>
    private Transform planet;

    private void Start()
    {
        ts = FindObjectOfType<TerrainSelect>();
        tp = FindObjectOfType<TerrainPainter>();
        planet = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    /// <summary>
    /// Sets the values of the terrain
    /// </summary>
    /// <param name="biome">The biome for this terrain</param>
    /// <param name="master">The master decal tag for this terrain</param>
    /// <param name="mask">The mask decal tag for this terrain</param>
    /// <param name="pos">The uvPos for this terrain</param>
    public void SetTerrainValues(string biome, string master, string mask, Vector3 pos)
    {
        biomeName = biome;
        masterDecalTag = master;
        maskDecalTag = mask;
        uvPos = pos;
    }

    /// <summary>
    /// Checks biome at terrain uv position and spawns different terrain if needed
    /// </summary>
    /// <param name="biome">The name of the biome that is currently being placed</param>
    public void CheckBiome(string biome)
    {
        if(biomeName == biome) return;

        // determine which mask cam to check based on masterDecalTag
        if (!Physics.Raycast(tp.maskPainterCams[(int) MaskNames.Master].transform.position + uvPos, Vector3.forward, out var hitMaster)) return;

        Vector3 checkMaskCamPos;
        if (hitMaster.collider.CompareTag(MaskNames.Red.ToString()))
        {
            checkMaskCamPos = tp.maskPainterCams[(int) MaskNames.Red].transform.position;
        }
        else if (hitMaster.collider.CompareTag(MaskNames.Green.ToString()))
        {
            checkMaskCamPos = tp.maskPainterCams[(int) MaskNames.Green].transform.position;
        }
        else 
        {
            checkMaskCamPos = tp.maskPainterCams[(int) MaskNames.Blue].transform.position;
        }
            
        // check mask cam for mask cam decal tag and send to terrain select
        if (!Physics.Raycast(checkMaskCamPos + uvPos, Vector3.forward, out var hitMask)) return;

        // check if different terrain
        if (hitMaster.collider.tag.Equals(masterDecalTag) && hitMask.collider.tag.Equals(maskDecalTag)) return;
        
        // spawns new terrain
        string newBiome = null;
        GameObject selectedTerrain = ts.SelectedTerrain(ref newBiome, hitMaster.collider.tag, hitMask.collider.tag, isUp);
        selectedTerrain.GetComponent<TerrainBehaviour>().SetTerrainValues(newBiome, hitMaster.collider.tag, hitMask.collider.tag, uvPos);
        
        print(gameObject.name + " to " +  selectedTerrain.name);
        
        var pos = transform;
        Instantiate(selectedTerrain, pos.position, pos.rotation, planet);
        
        // destroy old
        Destroy(gameObject);
    }
}
