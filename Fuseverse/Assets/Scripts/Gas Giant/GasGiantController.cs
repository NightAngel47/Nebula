using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGiantController : MonoBehaviour
{

    public Renderer rend;

    float rValue = 0f;
    float gValue = 0f;
    float bValue = 0f;

    Color planetColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
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

                /*
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
                */

        }

        //shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, 
        //GetComponent<Renderer>().material.color.a);

        planetColor = new Color(rValue, gValue, bValue, 1);
        rend.material.SetColor("_Color_Bands", planetColor);
        rend.material.SetColor("_Color_Storms", planetColor);
    }
}
