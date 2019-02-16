using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    public Renderer rend;
    
    Color shaderColor;
    Color originalColor;
    float originalAlpha;

    float alphaIncrement = 0f;
    


    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
 
        originalColor = GetComponent<Renderer>().material.color;
        shaderColor = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);

        originalAlpha = originalColor.a;

        alphaIncrement = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            alphaIncrement += 0.01f;
            float newAlpha = (originalAlpha + alphaIncrement);

            
            if (newAlpha > 1f)
            {
                newAlpha = 1f;
            }
            if (newAlpha < 0f)
            {
                newAlpha = 0f;
            }


            shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);


            rend.material.SetColor("_BaseColor", shaderColor);


            //alphaIncrement += 0.01f;
            Debug.Log("Alpha Change" + newAlpha);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            alphaIncrement -= 0.01f;
            float newAlpha = (originalAlpha + alphaIncrement);


            if (newAlpha < 0f)
            {
                newAlpha = 0f;
            }
            if (newAlpha > 1f)
            {
                newAlpha = 1f;
            }

            shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);


            rend.material.SetColor("_BaseColor", shaderColor);


            Debug.Log("Alpha Change" + newAlpha);
        }

    }
}


