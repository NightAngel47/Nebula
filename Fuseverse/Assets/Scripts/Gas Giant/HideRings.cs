using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRings : MonoBehaviour
{
    bool canViewRings;
    public GameObject rings;

    // Start is called before the first frame update
    void Start()
    {
        canViewRings = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canViewRings == true)
        {
            rings.SetActive(true);
        }
        else if (canViewRings == false)
        {
            rings.SetActive(false);
        }
    }

    public void ViewRings()
    {
        if (canViewRings == true)
        {
            canViewRings = false;
        }
        else if (canViewRings == false)
        {
            canViewRings = true;
        }
    }
}

