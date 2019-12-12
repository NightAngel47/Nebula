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
        var otherEdgePosition = otherEdge.position;
        Vector3 newPos = otherEdgePosition + new Vector3(0, otherEdgePosition.y - other.transform.position.y, 0);
        GameObject copy = Instantiate(other.gameObject, newPos, Quaternion.identity, other.transform.parent);
            
        // clean up
        Destroy(originalBiome);
        BiomeBehaviour copyData = copy.GetComponent<BiomeBehaviour>();
        if (copyData != null)
        {
            Destroy(copyData);
        }
    }
}
