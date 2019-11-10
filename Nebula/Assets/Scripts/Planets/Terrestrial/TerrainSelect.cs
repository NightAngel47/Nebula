using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelect : MonoBehaviour
{
    /// <summary>
    /// The names of the terrainObjects in order.
    /// WILL NEED TO CHANGE WITH FINAL ART!
    /// </summary>
    private enum terrainNames
    {
        Pine,
        PineSnowy,
        Palm,
        Cacti,
        Stump,
        Rock,
        Hill,
        HillSnowy,
        Mountain,
        IceChunk,
        Bush,
        Plateau,
        RockDesert,
        Volcano,
        Island
    };

    /// <summary>
    /// Array of terrain objects that can be painted. 
    /// </summary>
    [SerializeField, Tooltip("Array of terrain objects that can be painted.")]
    private GameObject[] terrainObjects;

    
    public GameObject SelectedTerrain(string masterDecalTag, string maskDecalTag, bool isUp)
    {
        return null;
    }
}
