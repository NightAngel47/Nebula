using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels; // terrainErase, hills rocky, pine norm
    public GameObject[] biomeTextures; // grass, artic, sand, forest, badlands, mountain, plains, water
    public enum tools {none, terrain, biomes};
    public tools toolSelected;
    public bool canPaint = true;
    public GameObject planet;

    private enum terrainTools {none, up, plants, erase}
    private terrainTools terrainToolSelected;

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 1) && (!(Input.touchCount == 2)) && (canPaint))
        {
            HandleInput();
        }
    }

    // paints
    void HandleInput()
    {
        canPaint = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // paint raycast
        if (Physics.Raycast(ray, out hitInfo, 100f, 9))
        {
            Debug.DrawLine(transform.position, hitInfo.transform.position);

            // gets terrain mode
            if (toolSelected == tools.terrain)
            {
                ChangeTerrain();
            }

            // paint
            if (toolSelected != tools.none && selectedGO != null)
            {
                PaintGO(hitInfo);
            }
        }
        
        Invoke("ResetCanPaint", 0.01f);
    }

    // spawn gameobjects
    void PaintGO(RaycastHit hitInfo)
    {
        GameObject newGO = Instantiate(selectedGO, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        newGO.transform.SetParent(planet.transform);
    }

    // slows down painting
    void ResetCanPaint()
    {
        canPaint = true;
    }

    // change tool to touch with
    public void ChangeTool(string tool)
    {
        toolSelected = (tools)System.Enum.Parse(typeof(tools), tool);
        print(toolSelected);

        // clear selected to not paint when switching
        selectedGO = null;
    }

    // change terrain paint mode
    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
        print(terrainToolSelected);

        selectedGO = null;
    }

    // change terrain model object to place
    private void ChangeTerrain()
    {
        // terrain objects have a script that will update themselves based on biome so spawns default grass version

        if (terrainToolSelected == terrainTools.erase) // erase
        {
            selectedGO = terrainModels[0]; // terrain ereaser
        }
        else // default grass
        {
            if (terrainToolSelected == terrainTools.up)
            {
                selectedGO = terrainModels[1]; // rocky hills
            }
            else if (terrainToolSelected == terrainTools.plants)
            {
                selectedGO = terrainModels[2]; // norm trees
            }
        }

        print("ChangeTerrain: " + selectedGO);
    }

    // change biome texture object to place
    public void ChangeBiomes(int selectedBiome)
    {
        selectedGO = biomeTextures[selectedBiome];
        print("ChaneBiome: " + selectedBiome);
    }
}
