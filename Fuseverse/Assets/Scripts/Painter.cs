using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels; // 1 terrainErase, 2 pine, 3 palm, 4 cacti, 5 stump, 6 hill, 7 mountains, 8 ice chunk, 9 rock
    public GameObject[] biomeTextures; // 1 snow, 2 artic, 3 sand, 4 forest, 5 badlands, 6 mountain, 7 plains, 8 water
    public enum tools {none, terrain, biomes};
    public tools toolSelected;
    public bool canPaint = true;
    public GameObject planet;

    private enum terrainTools {none, up, plants, erase}
    private terrainTools terrainToolSelected;

    // Update is called once per frame
    void Update()
    {
        // touch input
        if ((Input.touchCount == 1) && (!(Input.touchCount == 2)) && (canPaint))
        {
            HandleInput();
        }

        #if UNITY_EDITOR
        // mouse input
        if (Input.GetMouseButton(0) && canPaint)
        {
            HandleInput();
        }

        //Arrow key functionality for rotating planet
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.down, 20 * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.left, 20 * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.right, 20 * Time.deltaTime);
        }
        #endif
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
        
        Invoke("ResetCanPaint", Time.deltaTime);
    }

    // spawn gameobjects
    void PaintGO(RaycastHit hitInfo)
    {
        GameObject newGO = Instantiate(selectedGO, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal), planet.transform);
        if(toolSelected == tools.biomes)
        {
            newGO.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }
        if(toolSelected == tools.terrain)
        {
            newGO.transform.Rotate(Vector3.up, Random.Range(0, 45));
        }
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
                selectedGO = terrainModels[5]; // hill
            }
            else if (terrainToolSelected == terrainTools.plants)
            {
                selectedGO = terrainModels[1]; // pine
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
