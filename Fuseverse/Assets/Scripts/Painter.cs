using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public bool DebugOn = false;

    public GameObject terrainEraser;
    public GameObject[] biomeTextures;  // 0 snow, 1 artic, 2 sand, 3 forest, 
                                        // 4 badlands, 5 mountain, 6 plains, 7 water

    private enum tools { none, terrain, biomes }; // main tools
    private tools toolSelected; // main tool selected

    private enum terrainTools { none, up, plants, erase } // terrain tools
    private terrainTools terrainToolSelected; // terrain tool selected
    private TerrainFeatures tf;  // terrain - biome interactions

    private GameObject selectedGO; // game object to paint
    private bool canPaint = true;
    private GameObject planet;

    void Start()
    {
        tf = GetComponent<TerrainFeatures>();
        planet = GameObject.FindGameObjectWithTag("Planet");    
    }

    // Update is called once per frame
    void Update()
    {
        // touch input
        if ((Input.touchCount == 1) && (!(Input.touchCount == 2)) && (canPaint))
        {
            HandleInput();
        }

        // debug controls
        #if UNITY_EDITOR
        if(DebugOn)
        {
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
                ChangeTerrain(ray);
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
            newGO.transform.Rotate(Vector3.up, Random.Range(0, 180));
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
    private void ChangeTerrain(Ray ray)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f))
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

                selectedGO = tf.BiomeCheck(hitInfo.collider.tag, isUp); // biome check for spawning terrain
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
