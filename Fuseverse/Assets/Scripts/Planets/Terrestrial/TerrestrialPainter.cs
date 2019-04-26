using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://codeartist.mx/dynamic-texture-painting/
public class TerrestrialPainter : MonoBehaviour
{
    public bool isDebug = false;
    public float displacement = 0.0001f;
    public Transform UVPos;
    public Camera sceneCamera, canvasCam; // main camera, uv camera
    private GameObject selectedGO;
    public GameObject terrainEraser;
    private int selectedBiome = 0;
    public GameObject[] biomes; // 0 water, 1 plains, 2 sand, 3 snow, 
                                // 4 forest, 5 artic, 6 badlands, 7 mountain
    private enum tools { none, terrain, biomes }; // main tools
    private tools toolSelected = tools.biomes; // main tool selected
    private enum terrainTools { none, up, plants, erase } // terrain tools
    private terrainTools terrainToolSelected = terrainTools.plants; // terrain tool selected
    private TerrainFeatures tf;  // terrain - biome interactions
    public bool paintingBiome = false; // terrain biome checking
    private GameObject planet;

    public AudioSource placeAudioSource;
    public AudioClip[] placeAudioClips;  // 0 drop-tree, 1 cactus-plant, 2 island-place, 3 snow-good,
                                        // 4 grass-good, 5 rocks-falling, 6 basic-thud, 7 sand-disperse
                                        // 8 digital-water, 9 pouring-sand

    void Start()
    {
        tf = GetComponent<TerrainFeatures>();
        planet = GameObject.FindGameObjectWithTag("Planet");
        selectedGO = biomes[selectedBiome]; // default
    }

    void Update()
    {
        // touch
        if ((Input.touchCount == 1) && !(Input.touchCount == 2) && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            PlayPlacementAudio();
            SpawnGO();
        }
        else
            placeAudioSource.Stop();

        if (toolSelected == tools.none)
            placeAudioSource.Stop();

        #region debug painting controls
        #if UNITY_EDITOR
        // mouse debug
        if (Input.GetMouseButton(0) && isDebug)
        {
            SpawnGO();
        }
        #endif
        #endregion
    }

    void SpawnGO()
    {
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);

        // biomes
        if (toolSelected == tools.biomes)
        {
            Vector3 uvWorldPosition = Vector3.zero;
            if (HitTestUVPosition(cursorRay, ref uvWorldPosition))
            {
                paintingBiome = true;
                Invoke("ResetPainting", Time.deltaTime);
                GameObject newGO = Instantiate(selectedGO, UVPos.position + uvWorldPosition, Quaternion.identity);
                newGO.transform.Rotate(Vector3.back, Random.Range(0, 360));

                foreach(RaycastHit hit in Physics.SphereCastAll(cursorRay, 0.05f)) // error switching here
                    if (hit.collider.CompareTag("Terrain"))
                    {
                        print("Before: " + hit.collider.gameObject.name + "to " + selectedGO.tag);
                        hit.collider.GetComponent<SwapTerrain>().CheckBiome(selectedGO.tag);
                        print("After: " + hit.collider.gameObject.name  + "to " + selectedGO.tag);
                    }
                        
                
                
                // move back in order to place biomes on top of eachother
                PosDisplacement();
            }
        }
        // terrain
        else if(toolSelected == tools.terrain)
        {
            Vector3 uvWorldPosition = Vector3.zero;
            if (HitTestUVPosition(cursorRay, ref uvWorldPosition))
            {
                Vector3 biomePos = UVPos.position + uvWorldPosition;
                DetermineTerrain(biomePos);
                SpawnTerrain(cursorRay, biomePos);
            }
        }
    }

    void ResetPainting()
    {
        paintingBiome = false;
    }

    public bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(cursorRay, out hit))
        {
            if(!hit.collider.CompareTag("Terrain"))
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
                return false;
        }
        else
            return false;
    }

    void DetermineTerrain(Vector3 biomePos)
    {
        // terrain objects have a script that will update themselves based on biome
        if (terrainToolSelected == terrainTools.erase) // erase
            selectedGO = terrainEraser; // terrain ereaser
        else
        {
            bool isUp = false;
            if (terrainToolSelected == terrainTools.up)
                isUp = true;
            else
                isUp = false;

            Debug.DrawRay(biomePos + new Vector3(0, 0, displacement), Vector3.forward);
            // check uv sprites
            RaycastHit hit;
            if (Physics.Raycast(biomePos + new Vector3(0, 0, displacement), Vector3.forward, out hit))
                selectedGO = tf.BiomeCheck(hit.collider.tag, isUp); // biome check for spawning terrain
            else
                selectedGO = null;
        }
        ChooseTerrainAudio();
    }

    void SpawnTerrain(Ray cursorRay, Vector3 biomePos)
    {
        if (selectedGO != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(cursorRay, out hit, 10000, 9))
            {
                // spawn on planet
                GameObject newGO = Instantiate(selectedGO, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
                if(terrainToolSelected != terrainTools.erase)
                {
                    newGO.transform.Rotate(Vector3.up, Random.Range(0, 45));
                    newGO.transform.SetParent(planet.transform);
                    newGO.GetComponent<SwapTerrain>().SetUVPos(biomePos);
                }
            }
        }
    }

    void PosDisplacement()
    {
        Vector3 displacementVec = new Vector3(0, 0, displacement);

        UVPos.position -= displacementVec;
        canvasCam.transform.position -= displacementVec;
    }

    void ChooseBiomeAudio()
    {
        if (selectedBiome == 1 || selectedBiome == 4)
            placeAudioSource.clip = placeAudioClips[4]; // grass
        else if (selectedBiome == 2 || selectedBiome == 6)
            placeAudioSource.clip = placeAudioClips[7]; // sand
        else if (selectedBiome == 3 || selectedBiome == 5)
            placeAudioSource.clip = placeAudioClips[3]; // snow
        else if (selectedBiome == 0)
            placeAudioSource.clip = placeAudioClips[8]; // water
        else if (selectedBiome == 7)
            placeAudioSource.clip = placeAudioClips[5]; // rocks
    }

    void ChooseTerrainAudio()
    {
        if (selectedGO == terrainEraser)
            placeAudioSource.clip = placeAudioClips[9];
        else if (selectedGO == tf.terrainObjects[0] ||
            selectedGO == tf.terrainObjects[1])
            placeAudioSource.clip = placeAudioClips[0];
        else if (selectedGO == tf.terrainObjects[3])
            placeAudioSource.clip = placeAudioClips[1];
        else if (selectedGO == tf.terrainObjects[14])
            placeAudioSource.clip = placeAudioClips[2];
        else if (selectedGO == tf.terrainObjects[5] ||
                selectedGO == tf.terrainObjects[12])
            placeAudioSource.clip = placeAudioClips[5];
        else if (selectedGO == tf.terrainObjects[7])
            placeAudioSource.clip = placeAudioClips[3];
        else if (selectedGO == tf.terrainObjects[10])
            placeAudioSource.clip = placeAudioClips[4];
        else
            placeAudioSource.clip = placeAudioClips[6];
    }

    void PlayPlacementAudio()
    {
        if (!placeAudioSource.isPlaying)
            placeAudioSource.Play();
    }

    public void ChangeTool(string tool)
    {
        toolSelected = (tools)System.Enum.Parse(typeof(tools), tool);
        print(toolSelected);

        if(toolSelected == tools.biomes)
            selectedGO = biomes[selectedBiome];
    }

    public void ChangeBiome(int selected)
    {
        selectedGO = biomes[selected];
        selectedBiome = selected;
        ChooseBiomeAudio();
    }

    public void TerrainOption(string tool)
    {
        terrainToolSelected = (terrainTools)System.Enum.Parse(typeof(terrainTools), tool);
        selectedGO = null;
    }
}