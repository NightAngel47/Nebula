using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeWrapping : MonoBehaviour
{
    [SerializeField, Tooltip("The other edge of the seam")]
    private Transform otherEdge;
    
    private void OnTriggerEnter(Collider other)
    {
        BiomeBehaviour originalBiome = other.GetComponent<BiomeBehaviour>();
        if (originalBiome == null || originalBiome.hasCopy) return;
        
        // spawn copy
        GameObject copy = Instantiate(other.gameObject, otherEdge.position, Quaternion.identity);
        
        // move position
        var thisTransform = transform;
        Vector3 direction = other.transform.position - thisTransform.position;
        direction = Quaternion.Euler(otherEdge.eulerAngles - thisTransform.eulerAngles) * -direction;
        copy.transform.position -= direction;
        
        // change parent
        copy.transform.parent = other.transform.parent;

        // clean up
        Destroy(originalBiome);
        BiomeBehaviour copyData = copy.GetComponent<BiomeBehaviour>();
        if (copyData != null)
        {
            Destroy(copyData);
        }
    }
}
