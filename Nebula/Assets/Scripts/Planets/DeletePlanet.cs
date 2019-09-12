using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SavePlanet temp = FindObjectOfType<SavePlanet>();
        if(temp != null)
        {
            temp.UpdatePlanets();
        }
        
    }
}
