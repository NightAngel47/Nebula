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

        // biome raycast
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(transform.position, hitInfo.transform.position);

            // check which decal in order to destroy and repaint
            if (toolSelected == tools.biomes)
            {
                GameObject hitGO = hitInfo.transform.gameObject;

                if(!hitGO.CompareTag(selectedGO.tag) && !hitGO.CompareTag("Planet"))
                {
                    Destroy(hitGO);
                }
                else
                {
                    PaintGO();
                }
            }

            // paint terrain
            if (toolSelected == tools.terrain)
            {
                GameObject hitGO = hitInfo.transform.gameObject;
                if (!hitGO.CompareTag(selectedGO.tag))
                {
                    PaintGO();
                }
            }
        }
        
        Invoke("ResetCanPaint", 0.01f);
    }

    // spawn gameobjects
    void PaintGO()
    {
        Ray rayDown = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitDown;

        if (Physics.Raycast(rayDown, out hitDown, 100f, 9))
        {
            Instantiate(selectedGO, hitDown.point, Quaternion.FromToRotation(Vector3.up, hitDown.normal));
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
        tools selectedTool = (tools) System.Enum.Parse(typeof(tools), tool);

        toolSelected = selectedTool;
        print(toolSelected);

        // if terrain selected
        if (toolSelected == tools.terrain)
        {
            // because there is only one model so far
            ChangeTerrain();
        }
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
