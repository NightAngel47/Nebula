using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainErase : MonoBehaviour
{
    public bool doneErasing = true;
    
    void Start()
    {
        StartCoroutine(DestroyEraser());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

    private IEnumerator DestroyEraser()
    {
        doneErasing = false;
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}
