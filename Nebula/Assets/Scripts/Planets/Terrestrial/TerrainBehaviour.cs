using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBehaviour : MonoBehaviour
{
    #region References

    /// <summary>
    /// The names of the biome masks
    /// </summary>
    private enum MaskNames {Red, Green, Blue, Master};

    /// <summary>
    /// Terrain select for determining terrain
    /// </summary>
    private TerrainSelect ts;
    /// <summary>
    /// Terrain painter reference
    /// </summary>
    private TerrainPainter tp;
    /// <summary>
    /// Reference to the planet
    /// </summary>
    private Transform planet;

    #endregion

    #region Terrain Variables
    
    [SerializeField, Tooltip("Is terrain up or plant")]
    private bool isUp;
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

    #endregion

    private void Awake()
    {
        ts = FindObjectOfType<TerrainSelect>();
        tp = FindObjectOfType<TerrainPainter>();
        planet = FindObjectOfType<TerrestrialRotation>().transform;

        FixErrors();
    }

    /// <summary>
    /// Sets the values of the terrain
    /// </summary>
    /// <param name="master">The master decal tag for this terrain</param>
    /// <param name="mask">The mask decal tag for this terrain</param>
    /// <param name="pos">The uvPos for this terrain</param>
    public void SetTerrainValues(string master, string mask, Vector3 pos)
    {
        masterDecalTag = master;
        maskDecalTag = mask;
        uvPos = pos;
    }

    /// <summary>
    /// Checks biome at terrain uv position and spawns different terrain if needed
    /// </summary>
    public void CheckBiome()
    {
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
        
        // spawns new terrain
        GameObject selectedTerrain = ts.SelectedTerrain(hitMaster.collider.tag, hitMask.collider.tag, isUp);

        // check if same terrain
        if (gameObject.name.Contains(selectedTerrain.name)) return;
        
        // spawn new terrain
        var pos = transform;
        GameObject spawnedTerrain = Instantiate(selectedTerrain, pos.position, pos.rotation, planet);
        spawnedTerrain.GetComponent<TerrainBehaviour>().SetTerrainValues(hitMaster.collider.tag, hitMask.collider.tag, uvPos);
        
        //print("<b>GameObject: </b>" + gameObject.name + " <b>Parent: </b>" + spawnedTerrain.transform.parent.name);
        //Debug.Log("<b>Old Master:</b> " + masterDecalTag + "<b> New Master:</b> " + hitMaster.collider.tag + " <b>Old Mask:</b> " + maskDecalTag + " <b>New Mask:</b> " + hitMask.collider.tag + " <b>Old Terrain:</b> " + gameObject.name + " <b>New Terrain:</b> " + selectedTerrain.name);

        // destroy old
        Destroy(gameObject);
    }

    /// <summary>
    /// Destroys terrain errors where they don't get parented to the planet
    /// </summary>
    private void FixErrors()
    {
        if(gameObject.transform.parent == null)
        {
            Destroy(gameObject);
        }
    }
}
