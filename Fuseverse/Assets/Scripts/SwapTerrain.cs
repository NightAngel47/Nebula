using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTerrain : MonoBehaviour
{
    public bool isTree = false; // for extras need for pines
    public bool isSnowy = false; // current state

    // to change hills
    public Texture grass; // grass version
    public Texture snowy; // snow version
    private Material material; // current material

    // to change pines
    public GameObject swapPine; // used to swap to oppisite pine
    private Transform planet;

    void Start()
    {
        if(isTree)
        {

            planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<Transform>();
        }
        else
        {
            material = GetComponent<Renderer>().material;

            // sets current state
            if (material.GetTexture("_BaseColorMap") != grass)
            {
                isSnowy = true;
            }
        }
    }

    // updates terrain object based on biome
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Grass" && 
            other.tag == "Plains" && 
            other.tag == "Forest" &&
            other.tag == "Mountain" &&
            isSnowy) // if snow on grass
        {
            if (isTree)
            {
                SwapPines();
            }
            else
            {
                ChangeTexture(grass);
            }
        }
        else if(other.tag == "Artic" &&
            !isSnowy) // if grass on snow
        {
            if (isTree)
            {
                SwapPines();
            }
            else
            {
                ChangeTexture(snowy);
            }
        }
        else if(other.tag == "Sand" &&
            other.tag == "Badlands" &&
            other.tag == "Water") // if not on grass or snow
        {
            Destroy(gameObject);
        }
    }

    void ChangeTexture(Texture texture)
    {
        isSnowy = !isSnowy;
        material.SetTexture("_BaseColorMap", texture);
    }

    void SwapPines()
    {
        Instantiate(swapPine, transform.position, transform.rotation, planet);
        //Destroy(gameObject);
    }
}
