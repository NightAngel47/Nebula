using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    public Renderer rend;
    
    //Replacement color of shader
    Color shaderColor;

    //Current shader color
    Color originalColor;

    //Original alpha channel value
    float originalAlpha;

    //Value to increment alpha value by
    float alphaIncrement = 0f;
    


    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        
        //Set shaderColor equal to original color
        originalColor = GetComponent<Renderer>().material.color;
        shaderColor = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);

        originalAlpha = originalColor.a;

        //alphaIncrement starts at value 0 on start
        alphaIncrement = 0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            //Get current touch positions
            Touch firstTouch = Input.GetTouch(0);
            Touch secondTouch = Input.GetTouch(1);

            //Find previous touch positions
            Vector2 firstTouchPrevPos = firstTouch.position - firstTouch.deltaPosition;
            Vector2 secondTouchPrevPos = secondTouch.position - secondTouch.deltaPosition;

            //Find magnitude of previous touch positions
            float prevTouchDeltaMag = (firstTouchPrevPos - secondTouchPrevPos).magnitude;

            //Find magnitude of current touch positions
            float touchDeltaMag = (firstTouch.position - secondTouch.position).magnitude;

            //Find difference between the current and previous magnitude
            float deltaMagDif = touchDeltaMag - prevTouchDeltaMag;

            if(deltaMagDif > 0)
            {
                Debug.Log(">0");

                alphaIncrement += 0.01f;
                float newAlpha = (originalAlpha + alphaIncrement);

                //Keeps alpha from hitting a value above 1 and below 0 on slider 
                if (newAlpha > 1f)
                {
                    newAlpha = 1f;
                }
                if (newAlpha < 0f)
                {
                    newAlpha = 0f;
                }

                //Creates color with new alpha and sets material to color
                shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);
                rend.material.SetColor("_BaseColor", shaderColor);


                Debug.Log("Alpha Change" + newAlpha);
            }
            else if(deltaMagDif < 0)
            {
                Debug.Log("<0");

                alphaIncrement -= 0.01f;
                float newAlpha = (originalAlpha + alphaIncrement);

                //Keeps alpha from hitting a value above 1 and below 0 on slider 
                if (newAlpha < 0f)
                {
                    newAlpha = 0f;
                }
                if (newAlpha > 1f)
                {
                    newAlpha = 1f;
                }

                //Creates color with new alpha and sets material to color
                shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, newAlpha);
                rend.material.SetColor("_BaseColor", shaderColor);


                Debug.Log("Alpha Change" + newAlpha);
            }

        }

    }
}


