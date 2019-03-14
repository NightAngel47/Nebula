using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFeatures : MonoBehaviour
{
    public GameObject[] terrainObjects; // 0 pine, 1 pine snowy, 2 palm, 3 cacti, 4 stump,
                                        // 5 rock, 6 hill, 7 hill snowy, 8 mountains, 9 ice chuck

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
                return terrainObjects[0]; // pine
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
                return terrainObjects[7]; // hills (snowy)
            }
            else
            {
                return terrainObjects[9]; // ice chunk
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
                return terrainObjects[1]; // pines (snowy) (this might change)
            }
        }
        else if (biomeTag == "Sand")
        {
            if (isUp)
            {
                return null; // idk what up is at the moment
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
                return terrainObjects[5]; // rock (this will change)
            }
            else
            {
                return terrainObjects[4]; // stump
            }
        }
        else if (biomeTag == "Water")
        {
            return null; // nothing goes in water you fuck
        }
        else
        {
            return null;
        }
    }
}
