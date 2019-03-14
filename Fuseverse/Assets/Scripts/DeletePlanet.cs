using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<SavePlanet>().UpdatePlanets();
    }
}
