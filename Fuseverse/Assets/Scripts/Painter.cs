using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels;
    public GameObject[] biomeTextures;
    public enum tools {none, terrain, biomes};
    public tools toolSelected;
    public bool canPaint = true;
    public GameObject planet;

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
        tools selectedTool = (tools) System.Enum.Parse(typeof(tools), tool);

        toolSelected = selectedTool;
        print(toolSelected);

        // clear selected to not paint when switching
        selectedGO = null;
    }

    // change terrain model object to place
    public void ChangeTerrain(int selectedTerrain)
    {
        selectedGO = terrainModels[selectedTerrain];
        print("ChangeTerrain: " + selectedTerrain);
    }

    // change biome texture object to place
    public void ChangeBiones(int selectedBiome)
    {
        selectedGO = biomeTextures[selectedBiome];
        print("ChaneBiome: " + selectedBiome);
    }
}
