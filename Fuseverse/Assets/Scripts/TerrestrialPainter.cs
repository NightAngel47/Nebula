﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://codeartist.mx/dynamic-texture-painting/
public class TerrestrialPainter : MonoBehaviour
{
    public bool isDebug = false;
    public float displacement = 0.0001f;
    public Transform UVQuad;
    public Transform UVPos;
    public Camera sceneCamera, canvasCam; // main camera, uv camera
    private GameObject selectedGO;
    public GameObject terrainEraser;
    public GameObject[] biomes; // 0 snow, 1 artic, 2 sand, 3 forest, 
                                // 4 badlands, 5 mountain, 6 plains, 7 water
    private enum tools { none, terrain, biomes }; // main tools
    private tools toolSelected = tools.none; // main tool selected
    private enum terrainTools { none, up, plants, erase } // terrain tools
    private terrainTools terrainToolSelected = terrainTools.none; // terrain tool selected
    private TerrainFeatures tf;  // terrain - biome interactions
    public List<SwapTerrain> terrainTransforms;

    private GameObject planet;
    void Start()
    {
        tf = GetComponent<TerrainFeatures>();
        planet = GameObject.FindGameObjectWithTag("Planet");
    }

    void Update()
    {
        // touch
        if ((Input.touchCount == 1) && !(Input.touchCount == 2))
        {
            SpawnGO();
        }

        // mouse debug
        if (Input.GetMouseButton(0) && isDebug)
        {
            SpawnGO();
        }
    }

    void SpawnGO()
    {
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);

        if (toolSelected == tools.biomes)
        {
            Vector3 uvWorldPosition = Vector3.zero;
            if (HitTestUVPosition(cursorRay, ref uvWorldPosition))
            {
                GameObject newGO = Instantiate(selectedGO, UVPos.position + uvWorldPosition, Quaternion.identity);
                newGO.transform.Rotate(Vector3.back, Random.Range(0, 360));
                // move back in order to place biomes on top of eachother
                PosDisplacement();
            }
        }
        else if(toolSelected == tools.terrain)
        {
            Vector3 uvWorldPosition = Vector3.zero;
            if (HitTestUVPosition(cursorRay, ref uvWorldPosition))
            {
                Vector3 quadPos = UVQuad.position + uvWorldPosition;
                Vector3 biomePos = UVPos.position + uvWorldPosition;
                DetermineTerrain(quadPos, biomePos);
                SpawnTerrain(cursorRay, biomePos);
            }
        }
    }

    bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(cursorRay, out hit))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            uvWorldPosition.x = pixelUV.x - canvasCam.orthographicSize;//To center the UV on X
            uvWorldPosition.y = pixelUV.y - canvasCam.orthographicSize;//To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    void DetermineTerrain(Vector3 quadPos, Vector3 biomePos)
    {
        // terrain objects have a script that will update themselves based on biome
        if (terrainToolSelected == terrainTools.erase) // erase
        {
            selectedGO = terrainEraser; // terrain ereaser
        }
        else
        {
            bool isUp = false;
            if (terrainToolSelected == terrainTools.up)
            {
                isUp = true;
            }
            else
            {
                isUp = false;
            }

            Debug.DrawLine(quadPos, biomePos + new Vector3(0, 0, -0.1f));
            // check uv sprites
            RaycastHit hit;
            if (Physics.Linecast(quadPos, biomePos + new Vector3(0, 0, -0.1f), out hit))
            {
                selectedGO = tf.BiomeCheck(hit.collider.tag, isUp); // biome check for spawning terrain
            }
            else
            {
                selectedGO = null;
            }
        }
    }

    void SpawnTerrain(Ray cursorRay, Vector3 biomePos)
    {
        RaycastHit hit;
        if (Physics.Raycast(cursorRay, out hit, 100000, 9))
        {
            // spawn on planet
            GameObject newGO = Instantiate(selectedGO, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal), planet.transform);
            newGO.transform.Rotate(Vector3.up, Random.Range(0, 45));

            SwapTerrain newGOST = newGO.GetComponent<SwapTerrain>();
            newGOST.SetUVPos(biomePos);
            terrainTransforms.Add(newGOST);
        }
    }

    void PosDisplacement()
    {
        Vector3 displacementVec = new Vector3(0, 0, displacement);

        UVPos.position -= displacementVec;
        canvasCam.transform.position -= displacementVec;
        foreach (SwapTerrain terrainTransform in terrainTransforms)
        {
            terrainTransform.UVPos -= displacementVec;
        }
    }

    public void ChangeTool(string tool)
    {
        toolSelected = (tools)System.Enum.Parse(typeof(tools), tool);
        print(toolSelected);

        // clear selected to not paint when switching
        selectedGO = null;
    }

    public void ChangeBiome(int selected)
    {
        selectedGO = biomes[selected];
    }

    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
        selectedGO = null;
    }
}