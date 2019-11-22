using UnityEngine;

public class SwapTerrain : MonoBehaviour
{
    // spawning different terrain
    public bool isUp = false; // up or plant
    private TerrainFeatures tf; // terrain - biome interactions
    //private TerrestrialPainter tp; // painter
    private Transform canvasCam;
    private GameObject swapObject;
    private Vector3 uvPos;
    private Transform planet;

    void Start()
    {
        //tp = FindObjectOfType<TerrestrialPainter>();
        tf = FindObjectOfType<TerrainFeatures>();
        canvasCam = GameObject.FindGameObjectWithTag("CanvasCam").transform;
        planet = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    public void SetUVPos(Vector3 uvPosition)
    {
        uvPos = uvPosition;
    }

    void Update()
    {
        //if(tp.paintingBiome)
        //   CheckBiome();

        //if ((swapObject != null) && (swapObject.name + "(Clone)" != gameObject.name))
        //    SpawnNewTerrian();
    }
    
    // updates terrain object based on biome
    public void CheckBiome(string biomeTag)
    {
        swapObject = tf.BiomeCheck(biomeTag, isUp);
        if ((swapObject != null) && (!gameObject.name.Contains(swapObject.name)))
        {
            SpawnNewTerrian();
        }










        //Debug.DrawRay(uvPos + new Vector3(0, 0, canvasCam.position.z), Vector3.forward);
        //// check uv sprites

        //var startPos = uvPos;
        //startPos.z += canvasCam.position.z;


        //var raycasts = Physics.RaycastAll(startPos, Vector3.forward);

        //foreach(var hit in raycasts)
        //{
        //    print("Hit by:" + hit.collider.name);

        //    if (!hit.collider.CompareTag("Terrain") && !hit.collider.CompareTag("Planet"))
        //    {
        //        print("GameObject-" + gameObject.name + "'s hit tag: " + hit.collider.tag);
        //        swapObject = tf.BiomeCheck(biomeTag, isUp);
        //        print("swapObject-" + swapObject.name + "'s hit tag: " + hit.collider.tag);

        //        if ((swapObject != null) && (!gameObject.name.Contains(swapObject.name)))
        //        {
        //            SpawnNewTerrian();
        //            break;
        //        }
                    
        //    }
        //}


        //swapObject = tf.BiomeCheck(biomeTag, isUp);

    }

    void SpawnNewTerrian()
    {
        Instantiate(swapObject, transform.position, transform.rotation, planet);
        Destroy(gameObject);
    }
}
