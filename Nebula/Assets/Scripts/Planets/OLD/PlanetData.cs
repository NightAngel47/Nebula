using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetData : MonoBehaviour
{
    public Text planetDataText;

    public bool isTerrestrial;

    // terrestrial data
    public string[] biomeList = { "Snow", "Artic", "Sand", "Forest", "Badlands", "Mountain", "Plains", "Water" };
    public string[] terrainList = { "Pine", "Pine (Snowy)", "Palm", "Cacti", "Stump", "Rock", "Hill", "Hill (Snowy)", "Mountains", "Ice Chunk" };
    int[] biomeData; // 0 snow, 1 artic, 2 sand, 3 forest, 4 badlands, 5 mountain, 6 plains, 7 water
    int[] terrainData; // 0 pine, 1 pine snowy, 2 palm, 3 cacti, 4 stump, 5 rock, 6 hill, 7 hill snowy, 8 mountains, 9 ice chuck
    Color atmosphereColor;

    // Start is called before the first frame update
    void Start()
    {
        if (isTerrestrial)
        {
            //GameObject.FindGameObjectWithTag("Planet").GetComponent<LoadTerrestrialTexture>().LoadTexture();
        }

        biomeData = new int[biomeList.Length];
        terrainData = new int[terrainList.Length];

        //CollectData();
    }

    void CollectData()
    {
        // get data for terrestrial planets
        if(isTerrestrial)
        {
            CollectBiomes();
            CollectTerrain();
            CollectAtmosphere();

            // set ui
            FillPlanetDataText();

            // output data to console
            DebugTerrestrialData();
        }
    }

    // print to data to console
    void DebugTerrestrialData()
    {
        // print biomes
        for(int i = 0; i < biomeData.Length; i++)
        {
            print(biomeList[i] + " " + biomeData[i]);
        }

        // print terrain
        for(int i = 0; i < terrainData.Length; i++)
        {
            print(terrainList[i] + " " + terrainData[i]);
        }

        // print atmosphere
        print("Atmosphere color" + atmosphereColor.ToString());
    }

    void FillPlanetDataText()
    {
        planetDataText.text += "\n";

        // set biome data
        for (int i = 0; i < biomeData.Length; i++)
        {
            planetDataText.text += (biomeList[i] + " " + biomeData[i]) + "\n";
        }

        // set terrain data
        for (int i = 0; i < terrainData.Length; i++)
        {
            planetDataText.text += (terrainList[i] + " " + terrainData[i]) + "\n";
        }

        // set atmosphere data
        planetDataText.text += "Red " + atmosphereColor.r + "\n";
        planetDataText.text += "Green " + atmosphereColor.g + "\n";
        planetDataText.text += "Blue " + atmosphereColor.b + "\n";
        planetDataText.text += "Density " + atmosphereColor.a + "\n";
    }

    // gets biome data
    void CollectBiomes()
    {
        // gets count of each biome type
        for(int i = 0; i < biomeData.Length; i++)
        {
            int biomeCount = GameObject.FindGameObjectsWithTag(biomeList[i]).Length;
            if(biomeCount > 0)
            {
                biomeData[i] = biomeCount;
            }
        }
    }

    // gets terrain data
    void CollectTerrain()
    {
        // all terrain objects
        List<GameObject> terrainObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Terrain"));

        if (terrainObjects.Count > 0)
        {
            // count each terrain type and remove as it goes
            for (int i = 0; i < terrainData.Length; i++)
            {
                for (int j = 0; j < terrainObjects.Count; j++)
                {
                    if (terrainObjects[j].name == terrainList[i] + " Variant(Clone)")
                    {
                        terrainData[i]++; // increase data
                    }
                }
            }
        }
    }

    // gets atmosphere data
    void CollectAtmosphere()
    {
        atmosphereColor = FindObjectOfType<AtmosphereController>().GetComponent<Renderer>().material.color;
    }
}
