using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTerrain : MonoBehaviour
{
    public bool isSnowy = false; // current state

    // to change hills
    public Texture grass; // grass version
    public Texture snowy; // snow version
    private Material material; // current material

    void Start()
    {
        material = GetComponent<Renderer>().material;

        // sets current state
        if (material.GetTexture("_BaseColorMap") != grass)
        {
            isSnowy = true;
        }
    }

    // updates terrain object based on biome
    void OnTriggerEnter(Collider other)
    {
        if(
            other.tag == "Plains" || 
            other.tag == "Forest" &&
            isSnowy) // if snow on grass
        {
            ChangeTexture(grass);
        }
        else if(
            other.tag == "Snow" ||
            other.tag == "Artic" ||
            other.tag == "Mountain" &&
            !isSnowy) // if grass on snow
        {
            ChangeTexture(snowy);
        }
        else if(
            other.tag == "Sand" ||
            other.tag == "Badlands" ||
            other.tag == "Water") // if not on grass or snow
        {
            Destroy(gameObject);
        }
    }

    // changes material texture
    void ChangeTexture(Texture texture)
    {
        isSnowy = !isSnowy;
        material.SetTexture("_BaseColorMap", texture);
    }
}
