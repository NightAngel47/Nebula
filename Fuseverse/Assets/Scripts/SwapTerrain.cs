using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTerrain : MonoBehaviour
{
    // spawning different terrain
    public bool isUp = false; // up or plant
    private TerrainFeatures tf; // terrain - biome interactions
    private GameObject swapObject;
    private Transform planet;

    void Start()
    {
        tf = FindObjectOfType<TerrainFeatures>();
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
    }

    void Update()
    {
        if ((swapObject.name + "(Clone)" != gameObject.name))
        {
            SpawnNewTerrian();
        }
    }

    // updates terrain object based on biome
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Terrain"))
        {
            swapObject = tf.BiomeCheck(other.tag, isUp);
        }
    }

    void SpawnNewTerrian()
    {
        print("spawning");
        Instantiate(swapObject, transform.position, transform.rotation, planet);
        Destroy(gameObject);
    }
}
