using UnityEngine;

public class SwapTerrain : MonoBehaviour
{
    // spawning different terrain
    public bool isUp = false; // up or plant
    private TerrainFeatures tf; // terrain - biome interactions
    private TerrestrialPainter tp; // painter
    private GameObject swapObject;
    private Transform planet;
    public Vector3 UVPos;

    void Start()
    {
        tp = FindObjectOfType<TerrestrialPainter>();
        tf = FindObjectOfType<TerrainFeatures>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
    }

    void Update()
    {
        CheckBiome();

        if ((swapObject != null) && (swapObject.name + "(Clone)" != gameObject.name))
        {
            SpawnNewTerrian();
        }
    }

    public void SetUVPos(Vector3 UVPosistion)
    {
        UVPos = UVPosistion;
    }

    // updates terrain object based on biome
    void CheckBiome()
    {
        Debug.DrawLine(UVPos + new Vector3(0, 0, -0.1f), UVPos + new Vector3(0, 0, 0.1f));
        RaycastHit hit;
        if (Physics.Linecast(UVPos + new Vector3(0, 0, -0.1f), UVPos + new Vector3(0, 0, 0.1f), out hit))
        {
            swapObject = tf.BiomeCheck(hit.collider.tag, isUp);
        }
    }

    void SpawnNewTerrian()
    {
        GameObject newGO = Instantiate(swapObject, transform.position, transform.rotation, planet);
        tp.terrainTransforms.Remove(this);
        tp.terrainTransforms.Add(newGO.GetComponent<SwapTerrain>());
        Destroy(gameObject);
    }
}
