using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    bool atmosphereButton;

    public Renderer rend;

    public float atmosphereMax = 0f;
    
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
        if ((Input.touchCount == 2) && (atmosphereButton))
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
                if (newAlpha > atmosphereMax)
                {
                    newAlpha = atmosphereMax;
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
                if (newAlpha > atmosphereMax)
                {
                    newAlpha = atmosphereMax;
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
                rValue = 0.99215686086f;
                gValue = 0.6078431361f;
                bValue = 0f;
                break;

            case 3:
                rValue = 0.9607843119f;
                gValue = 0.94901960604f;
                bValue = 0;
                break;

            case 4:
                rValue = 0.58431372438f;
                gValue = 0.97254901776f;
                bValue = 0f;
                break;

            case 5:
                rValue = 0.18039215652f;
                gValue = 0.79215686124f;
                bValue = 0.0784313724f;
                break;

            case 6:
                rValue = 0.10980392136f;
                gValue = 0.91372548846f;
                bValue = 0.588235293f;
                break;

            case 7:
                rValue = 0f;
                gValue = 0.88627450812f;
                bValue = 1f;
                break;

            case 8:
                rValue = 0f;
                gValue = 0.01568627448f;
                bValue = 1f;
                break;

            case 9:
                rValue = 0.7647058809f;
                gValue = 0f;
                bValue = 1f;
                break;

            case 10:
                rValue = 0.4705882344f;
                gValue = 0.4705882344f;
                bValue = 0.4705882344f;
                break;

            case 11:
                rValue = 0.63137254782f;
                gValue = 0.47843137164f;
                bValue = 0.33725490132f;
                break;

            case 12:
                rValue = 0.90588235122f;
                gValue = 0.90588235122f;
                bValue = 0.90588235122f;
                break;

        }

        //shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, GetComponent<Renderer>().material.color.a);
        shaderColor = new Color(rValue, gValue, bValue, newAlpha);
        rend.material.SetColor("_BaseColor", shaderColor);
    }

    public void ActivateAtmosphere()
    {
        atmosphereButton = true;
    }

    public void DeactivateAtmosphere()
    {
        atmosphereButton = false;
    }
}


