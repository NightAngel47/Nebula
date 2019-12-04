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
    /// The names of the terrainObjects in order.
    /// </summary>
    public enum TerrainNames
    {
        IceSheet,
        Iceberg,
        TundraIsland,
        ConiferousBoulder,
        ConiferousMountain,
        ConiferousTree,
        BirchTree,
        DeciduousRock,
        OakTree,
        Cactus,
        JoshuaTree,
        TemperateShrub,
        Flower1,
        Flower2,
        Flower3,
        Grass,
        AbalShrub,
        AcaciaTree,
        ElephantGrass,
        Palm,
        TopicalRock,
        TropicalShrub,
        Algae,
        Island,
        Volcano
    };

    /// <summary>
    /// Array of terrain objects that can be painted. 
    /// </summary>
    [SerializeField, Tooltip("Array of terrain objects that can be painted.")]
    public GameObject[] terrainObjects;

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
                if (isUp)
                {
                    int randNum = Random.Range(0, 3);
                    {
                        switch (randNum)
                        {
                            case 0:
                                return terrainObjects[(int) TerrainNames.Flower1];
                            case 1:
                                return terrainObjects[(int) TerrainNames.Flower2];
                            case 2:
                                return terrainObjects[(int) TerrainNames.Flower3];
                        }
                    }
                }
                else
                {
                    return terrainObjects[(int) TerrainNames.Grass];
                }
            }
            else if (maskDecalTag == MaskNames.Green.ToString()) // savanna
            {
                if (isUp)
                {
                    return terrainObjects[(int) TerrainNames.AcaciaTree];
                }
                else
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.AbalShrub] : terrainObjects[(int) TerrainNames.ElephantGrass];
                }
            }
            else // tropical desert
            {
                if (isUp)
                {
                    return terrainObjects[(int) TerrainNames.TopicalRock];
                }
                else
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.Palm] : terrainObjects[(int) TerrainNames.TropicalShrub];
                }
            }
        }
        else if (masterDecalTag == MaskNames.Green.ToString())
        {
            if (maskDecalTag == MaskNames.Red.ToString()) // temperate desert
            {
                if (isUp)
                {
                    return terrainObjects[(int) TerrainNames.JoshuaTree];
                }
                else
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.Cactus] : terrainObjects[(int) TerrainNames.TemperateShrub];
                }
            }
            else if (maskDecalTag == MaskNames.Green.ToString()) // coniferous forest
            {
                if (isUp)
                {
                    float randNum = Random.value;
                    return randNum >= 0.75f ? terrainObjects[(int) TerrainNames.ConiferousMountain] : terrainObjects[(int) TerrainNames.ConiferousBoulder];
                }
                else
                {
                    return terrainObjects[(int) TerrainNames.ConiferousTree];
                }
            }
            else // taiga (taiga is mislabeled, represents deciduous)
            {
                if (isUp)
                {
                    return terrainObjects[(int) TerrainNames.DeciduousRock];
                }
                else
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.BirchTree] : terrainObjects[(int) TerrainNames.OakTree];
                }
            }
        }
        else if (masterDecalTag == MaskNames.Blue.ToString())
        {
            if (maskDecalTag == MaskNames.Red.ToString()) // ocean
            {
                if (isUp)
                {
                    return terrainObjects[(int) TerrainNames.Volcano];
                }
                else
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.Algae] : terrainObjects[(int) TerrainNames.Island];
                }
            }
            else // ice
            {
                if (isUp)
                {
                    float randNum = Random.value;
                    return randNum >= 0.5f ? terrainObjects[(int) TerrainNames.IceSheet] : terrainObjects[(int) TerrainNames.Iceberg];
                }
                else
                {
                    return terrainObjects[(int) TerrainNames.TundraIsland];
                }
            }
        }
        
        Debug.LogError("Could not select terrain. MasterDecalTag: " + masterDecalTag + 
                       " MaskDecalTag: " + maskDecalTag + 
                       " isUp: " + isUp);
        return null;
    }
}
