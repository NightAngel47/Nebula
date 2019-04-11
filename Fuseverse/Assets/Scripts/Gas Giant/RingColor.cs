using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColor : MonoBehaviour
{
    public ParticleSystem theRings;

    float rValue;
    float gValue;
    float bValue;

    float rValue2;
    float gValue2;
    float bValue2;

    void Start()
    {
    }
    private void Update()
    {
          
    }
    public void ChangeRingColor(string rgbValue)
    {

        string[] splittedParams = rgbValue.Split(',');

        //get the first param
        string FirstParam = splittedParams[0];
        string SecondParam = splittedParams[1];
        string ThirdParam = splittedParams[2];

        //Convert it back to int
        float rValueSet = float.Parse(FirstParam);
        float gValueSet = float.Parse(SecondParam);
        float bValueSet = float.Parse(ThirdParam);


        /* switch (RingColorButton)
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
         }*/

        rValue = (rValueSet / 255);
        gValue = (gValueSet / 255);
        bValue = (bValueSet / 255);

        SetColor();
    }

    public void ChangeSecondaryRingColor(string rgbValue)
    {
        string[] splittedParams = rgbValue.Split(',');

        //get the first param
        string FirstParam = splittedParams[0];
        string SecondParam = splittedParams[1];
        string ThirdParam = splittedParams[2];

        //Convert it back to int
        float rValueSet = float.Parse(FirstParam);
        float gValueSet = float.Parse(SecondParam);
        float bValueSet = float.Parse(ThirdParam);

        /*switch (SecondaryRingColorButton)
        {
            case 1:
                rValue2 = 1f;
                gValue2 = 0f;
                bValue2 = 0f;
                break;

            case 2:
                rValue2 = 0.99215686086f;
                gValue2 = 0.6078431361f;
                bValue2 = 0f;
                break;

            case 3:
                rValue2 = 0.9607843119f;
                gValue2 = 0.94901960604f;
                bValue2 = 0;
                break;

            case 4:
                rValue2 = 0.58431372438f;
                gValue2 = 0.97254901776f;
                bValue2 = 0f;
                break;

            case 5:
                rValue2 = 0.18039215652f;
                gValue2 = 0.79215686124f;
                bValue2 = 0.0784313724f;
                break;

            case 6:
                rValue2 = 0.10980392136f;
                gValue2 = 0.91372548846f;
                bValue2 = 0.588235293f;
                break;

            case 7:
                rValue2 = 0f;
                gValue2 = 0.88627450812f;
                bValue2 = 1f;
                break;

            case 8:
                rValue2 = 0f;
                gValue2 = 0.01568627448f;
                bValue2 = 1f;
                break;
        }*/

        rValue2 = (rValueSet / 255);
        gValue2 = (gValueSet / 255);
        bValue2 = (bValueSet / 255);
        SetColor();
    }

    void SetColor()
    {
        Color color1 = new Color(rValue, gValue, bValue, 1f);
        Color color2 = new Color(rValue2, gValue2, bValue2, 1f);

        var main = theRings.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2); // random between 2 colors

    }

    
    
}
