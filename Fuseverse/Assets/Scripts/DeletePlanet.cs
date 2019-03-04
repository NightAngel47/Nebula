using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlanet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SavePlanet.instance)
        {
            SavePlanet.instance.UpdatePlanets();
        }
        print(SavePlanet.instance);
        //GameObject.Find("Planet").GetComponent<SavePlanet>().UpdatePlanets();
        //FindObjectOfType<SavePlanet>().UpdatePlanets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
