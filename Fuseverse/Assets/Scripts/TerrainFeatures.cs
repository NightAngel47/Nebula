using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFeatures : MonoBehaviour
{
    public GameObject[] terrainObjects; // 0 pine, 1 pine snowy, 2 palm, 3 cacti, 4 stump,
                                        // 5 rock, 6 hill, 7 hill snowy, 8 mountains, 9 ice chuck
                                        // 10 bush, 11 plateau, 12 rock desert, 13 volcano, 14 islands
    private string defaultBiomeTag;

    void Start()
    {
        defaultBiomeTag = FindObjectOfType<DefaultBiome>().defaultBiome;
    }

    // terrain - biome interactions
    public GameObject BiomeCheck(string biomeTag, bool isUp)
    {
        if (biomeTag == "Plains")
        {
            if (isUp)
            {
                return terrainObjects[5]; // rock
            }
            else
            {
                return terrainObjects[10]; // bush
            }
        }
        else if(biomeTag == "Forest")
        {
            if (isUp)
            {
                return terrainObjects[6]; // hill
            }
            else
            {
                return terrainObjects[0]; // pine
            }
        }
        else if (biomeTag == "Artic")
        {
            if (isUp)
            {
                return terrainObjects[9]; // ice chunk
            }
            else
            {
                return terrainObjects[1]; // pines (snowy)
            }
        }
        else if (biomeTag == "Snow")
        {
            if (isUp)
            {
                return terrainObjects[7]; // hills (snowy)
            }
            else
            {
                return terrainObjects[1]; // pines (snowy)
            }
        }
        else if (biomeTag == "Mountain")
        {
            if (isUp)
            {
                return terrainObjects[8]; // mountain
            }
            else
            {
                return terrainObjects[1]; // pines (snowy)
            }
        }
        else if (biomeTag == "Sand")
        {
            if (isUp)
            {
                return terrainObjects[12]; // rock (desert)
            }
            else
            {
                return terrainObjects[3]; // cacti
            }
        }
        else if (biomeTag == "Badlands")
        {
            if (isUp)
            {
                return terrainObjects[11]; // plateau
            }
            else
            {
                return terrainObjects[3]; // cacti
            }
        }
        else if (biomeTag == "Water")
        {
            if (isUp)
            {
                return terrainObjects[13]; // volcano
            }
            else
            {
                return terrainObjects[14]; // islands
            }
        }
        else if (biomeTag == defaultBiomeTag || biomeTag == "UVQuad")
        {
            return BiomeCheck(defaultBiomeTag, isUp);
        }
        else
        {
            print("Tag error - biomtag: " + biomeTag);
            return null;
        }
    }
}
