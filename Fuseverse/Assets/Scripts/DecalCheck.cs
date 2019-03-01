using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalCheck : MonoBehaviour
{
    void Start()
    {
        Invoke("DestroyThis", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag != gameObject.tag) && other.tag != "Terrain")
        {
            if (other.tag != "Planet")
            {
                Destroy(other.gameObject);
            }
        }
    }

    void DestroyThis()
    {
        Destroy(this);
    }
}