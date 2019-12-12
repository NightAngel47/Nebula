using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeWrapping : MonoBehaviour
{
    public Transform otherEdge;
    
    private void OnTriggerEnter(Collider other)
    {
        BiomeBehaviour originalBiome = other.GetComponent<BiomeBehaviour>();
        if (originalBiome == null || originalBiome.hasCopy) return;
        
        // spawn copy
        GameObject copy = Instantiate(other.gameObject, otherEdge.position, Quaternion.identity, other.transform.parent);
        copy.transform.position += other.transform.position - transform.position;

        // clean up
        Destroy(originalBiome);
        BiomeBehaviour copyData = copy.GetComponent<BiomeBehaviour>();
        if (copyData != null)
        {
            Destroy(copyData);
        }
    }
}
