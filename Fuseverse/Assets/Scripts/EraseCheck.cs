using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraseCheck : MonoBehaviour
{
    public bool terrainEraser = false;

    void Start()
    {
        Invoke("DestroyThis", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag != gameObject.tag))
        {
            if (other.tag != "Planet")
            {
                Destroy(other.gameObject);
            }
        }
    }

    void DestroyThis()
    {
        if (terrainEraser)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}