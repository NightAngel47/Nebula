using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class BiomeClump : MonoBehaviour
{
    public Transform[] biomeTransfroms = new Transform[9];
    public GameObject biomeDecal;
    Transform planet;

    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").transform;
    }

    void Start()
    {
        // nince sections of decal
        for(int i = 0; i < biomeTransfroms.Length; ++i)
        {
            // spawn decal
            Instantiate(biomeDecal, biomeTransfroms[i].position, biomeTransfroms[i].rotation, planet);
            
        }

        Destroy(gameObject);
    }
}
