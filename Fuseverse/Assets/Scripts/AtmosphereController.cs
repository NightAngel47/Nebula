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

    float newAlpha;

    float rValue;
    float gValue;
    float bValue;
    


    // Start is called before the first frame update
    void Start()
    {

        rend = GetComponent<Renderer>();
        
        //Set shaderColor equal to original color
        originalColor = GetComponent<Renderer>().material.color;
        shaderColor = new Color(originalColor.r, originalColor.g, originalColor.b, originalColor.a);

        originalAlpha = originalColor.a;

        rValue = originalColor.r;
        gValue = originalColor.g;
        bValue = originalColor.b;
        
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
                newAlpha = (originalAlpha + alphaIncrement);

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
                shaderColor = new Color(rValue, gValue, bValue, newAlpha);
                rend.material.SetColor("_BaseColor", shaderColor);


                Debug.Log("Alpha Change" + newAlpha);
            }
            else if(deltaMagDif < 0)
            {
                Debug.Log("<0");

                alphaIncrement -= 0.01f;
                newAlpha = (originalAlpha + alphaIncrement);

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
                shaderColor = new Color(rValue, gValue, bValue, newAlpha);
                rend.material.SetColor("_BaseColor", shaderColor);


                Debug.Log("Alpha Change" + newAlpha);
            }

        }

    }

    public void ChangeColor(int atmosphereColorButton)
    {
        switch (atmosphereColorButton)
        {
            case 1:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 2:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 3:
                rValue = 1f;
                gValue = 0.92f;
                bValue = 0.016f;
                break;

            case 4:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 5:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 6:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 7:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 8:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 9:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 10:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 11:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

            case 12:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                break;

        }

        //shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, GetComponent<Renderer>().material.color.a);
        shaderColor = new Color(rValue, gValue, bValue, newAlpha);
        rend.material.SetColor("_BaseColor", shaderColor);
    }
}


