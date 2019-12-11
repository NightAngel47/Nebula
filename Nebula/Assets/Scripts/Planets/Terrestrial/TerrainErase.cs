using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainErase : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(DestroyThis), 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            Destroy(other.transform.parent.gameObject);
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
