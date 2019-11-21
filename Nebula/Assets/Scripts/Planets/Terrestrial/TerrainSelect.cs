using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSelect : MonoBehaviour
{
    /// <summary>
    /// The names of the biome masks
    /// </summary>
    private enum MaskNames {Red, Green, Blue};
    
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
    /// The names of the terrainObjects in order.
    /// WILL NEED TO CHANGE WITH FINAL ART!
    /// </summary>
    private enum TerrainNames
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

    /// <summary>
    /// Selects terrain to spawn based on masterDecalTag, maskDecalTag, and isUp
    /// </summary>
    /// <param name="masterDecalTag">The color of the masterDecal</param>
    /// <param name="maskDecalTag">The color of the maskDecal</param>
    /// <param name="isUp">Is the terrain up or plant</param>
    /// <returns></returns>
    public GameObject SelectedTerrain(string masterDecalTag, string maskDecalTag, bool isUp)
    {
        if (masterDecalTag == MaskNames.Red.ToString())
        {
            if (maskDecalTag == MaskNames.Red.ToString()) // plains
            {
                return isUp ? terrainObjects[(int) TerrainNames.Rock] : terrainObjects[(int) TerrainNames.Bush];
            }
            else if (maskDecalTag == MaskNames.Green.ToString()) // savanna
            {
                return isUp ? terrainObjects[(int) TerrainNames.RockDesert] : terrainObjects[(int) TerrainNames.Cacti];
            }
            else // tropical
            {
                return isUp ? terrainObjects[(int) TerrainNames.Plateau] : terrainObjects[(int) TerrainNames.Cacti];
            }
        }
        else if (masterDecalTag == MaskNames.Green.ToString())
        {
            if (maskDecalTag == MaskNames.Red.ToString()) // temperate
            {
                return isUp ? terrainObjects[(int) TerrainNames.Mountain] : terrainObjects[(int) TerrainNames.PineSnowy];
            }
            else if (maskDecalTag == MaskNames.Green.ToString()) // coniferous
            {
                return isUp ? terrainObjects[(int) TerrainNames.Hill] : terrainObjects[(int) TerrainNames.Pine];
            }
            else // taiga
            {
                return isUp ? terrainObjects[(int) TerrainNames.HillSnowy] : terrainObjects[(int) TerrainNames.PineSnowy];
            }
        }
        else if (masterDecalTag == MaskNames.Blue.ToString())
        {
            if (maskDecalTag == MaskNames.Red.ToString()) // ocean
            {
                return isUp ? terrainObjects[(int) TerrainNames.Volcano] : terrainObjects[(int) TerrainNames.Island];
            }
            else // ice
            {
                return isUp ? terrainObjects[(int) TerrainNames.IceChunk] : terrainObjects[(int) TerrainNames.PineSnowy];
            }
        }
        
        Debug.LogError("Could not select terrain. MasterDecalTag: " + masterDecalTag + 
                       " MaskDecalTag: " + maskDecalTag + 
                       " isUp: " + isUp);
        return null;
    }
}
