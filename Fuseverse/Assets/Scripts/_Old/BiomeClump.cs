using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class BiomeClump : MonoBehaviour
{
    public Transform[] biomeTransfroms = new Transform[9];
    public GameObject biomeTex;

    void Start()
    {
        // nince sections of decal
        for(int i = 0; i < biomeTransfroms.Length; ++i)
        {
            // spawn decal
            GameObject newGO = Instantiate(biomeTex, biomeTransfroms[i].position, Quaternion.identity);
            //newGO.transform.Rotate(Vector3.up, Random.Range(0, 360));
        }

        Destroy(gameObject);
    }
}
