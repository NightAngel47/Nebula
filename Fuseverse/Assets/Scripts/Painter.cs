using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels;
    public GameObject[] biomeTextures;
    public enum tools { terrain, biomes};
    public tools toolSelected;
    public bool canPaint = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && canPaint)
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
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Instantiate(selectedGO, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        }

        Invoke("ResetCanPaint", 0.1f);
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

        // if terrain selected
        if (toolSelected == tools.terrain)
        {
            ChangeTerrain();
        }

        // if biomes selected
        //else if (toolSelected == tools.biomes)
        //{
        //    changeBiones();
        //}
    }

    // change terrain model object to place
    void ChangeTerrain()
    {
        selectedGO = terrainModels[0];
    }

    // change biome texture object to place
    public void ChangeBiones(int selectedBiome)
    {
        selectedGO = biomeTextures[selectedBiome];
    }
}
