using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels; // terrainErase, hills rocky, hills snowy, pine norm, pine snowy
    public GameObject[] biomeTextures; 
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

            // get selected GO for terrain
            if (toolSelected == tools.terrain)
            {
                ChangeTerrain(ray);
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

    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
        print(terrainToolSelected);

        selectedGO = null;
    }

    // change terrain model object to place
    private void ChangeTerrain(Ray ray)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            if (terrainToolSelected == terrainTools.erase) // erase
            {
                selectedGO = terrainModels[0]; // terrain ereaser
            }
            else if (hitInfo.collider.CompareTag("Snow")) // snow biome
            {
                if (terrainToolSelected == terrainTools.up)
                {
                    selectedGO = terrainModels[2]; // snowy hills
                }
                else if (terrainToolSelected == terrainTools.plants)
                {
                    selectedGO = terrainModels[4]; // snowy trees
                }
            }
            else if (hitInfo.collider.CompareTag("Sand")) // sand biome
            {
                selectedGO = null; // nothing
            }
            else // default grass
            {
                if (terrainToolSelected == terrainTools.up)
                {
                    selectedGO = terrainModels[1]; // rocky hills
                }
                else if (terrainToolSelected == terrainTools.plants)
                {
                    selectedGO = terrainModels[3]; // norm trees
                }
            }
        }

        print("ChangeTerrain: " + selectedGO);
    }

    // change biome texture object to place
    public void ChangeBiones(int selectedBiome)
    {
        selectedGO = biomeTextures[selectedBiome];
        print("ChaneBiome: " + selectedBiome);
    }
}
