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

        // biome raycast
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Debug.DrawLine(transform.position, hitInfo.transform.position);

            // check which decal in order to destroy and repaint
            if (toolSelected == tools.biomes)
            {
                GameObject hitGO = hitInfo.transform.gameObject;

                // check tag and delete then repaint
                if (!hitGO.CompareTag(selectedGO.tag) && !hitGO.CompareTag("Planet"))
                {
                    print("Destroy: " + hitGO.tag);
                    Destroy(hitGO);
                }
                else
                {
                    print("Other: " + hitGO.tag);
                    PaintGO(hitInfo);
                }
            }

            // paint terrain
            if (toolSelected == tools.terrain)
            {
                GameObject hitGO = hitInfo.transform.gameObject;
                if (!hitGO.CompareTag(selectedGO.tag))
                {
                    PaintGO(hitInfo);
                }
            }
        }

        Invoke("ResetCanPaint", 0.01f);
    }

    // spawn gameobjects
    void PaintGO(RaycastHit hitInfo)
    {

        // 0.5 works -0.5 does the thing might need an offset
        Instantiate(selectedGO, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
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

#endif
