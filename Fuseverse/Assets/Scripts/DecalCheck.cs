using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger");

        if ((other.tag != gameObject.tag))
        {
            if (other.tag != "Planet")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
