using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainErase : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            if (other.tag != "Planet")
            {
                Destroy(other.gameObject);
            }
        }
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
