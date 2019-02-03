#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingControls : MonoBehaviour
{
    public GameObject selectedGO;
    public GameObject[] terrainModels;
    public GameObject[] biomeTextures;
    public enum tools { none, terrain, biomes };
    public tools toolSelected;
    public bool canPaint = true;
    public GameObject planet;

    // Update is called once per frame
    void Update()
    {
        //Arrow key functionality for painting
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
    }

    // paints
    void HandleInput()
    {
        canPaint = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // paint raycast
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(transform.position, hitInfo.transform.position);

            GameObject hitGO = hitInfo.collider.gameObject;

            // check which decal in order to destroy and repaint
            if (toolSelected == tools.biomes)
            {
                if (!hitGO.CompareTag(selectedGO.tag) && !hitGO.CompareTag("Planet"))
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
            GameObject newGO = Instantiate(selectedGO, hitDown.point, Quaternion.FromToRotation(Vector3.up, hitDown.normal));
            newGO.transform.SetParent(planet.transform);
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
        tools selectedTool = (tools)System.Enum.Parse(typeof(tools), tool);

        toolSelected = selectedTool;
        print(toolSelected);

        // if terrain selected
        if (toolSelected == tools.terrain)
        {
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

#endif
