using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fj");

        if ((other.tag != gameObject.tag))
        {
            if (other.tag != "Planet")
            { }
                Destroy(other.gameObject);
            

        }
        
    }

    private void Awake()
    {
        
    }
}
