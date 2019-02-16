using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    public Renderer rend;

    //Color originalColor;
    // public Shader theColor;
    Color test;
    float alphaIncrement = 0f;
    Color originalColour;
    float originalA;


    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();

        originalColour = GetComponent<Renderer>().material.color;

        test = new Color(originalColour.r, originalColour.g, originalColour.b, originalColour.a);

        originalA = originalColour.a;



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //rend.sharedMaterial.color = new Color(1, 1, 1, alphaIncrement);

            float anotherTest = (originalA + alphaIncrement);

            
            if (anotherTest > 1f)
            {
                anotherTest = 1f;
            }
            

            test = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, anotherTest);

           // rend.material.SetColor("_BaseColor", Color.green);

            rend.material.SetColor("_BaseColor", test);

            //_BaseColor("BaseColor", Color) = (1, 1, 1, 1)

            alphaIncrement += 0.01f;
            //alphaIncrement++;
            

            Debug.Log("Alpha Change" + anotherTest);
        }

    }
}


