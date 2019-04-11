using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGiantController : MonoBehaviour
{
   // public ParticleSystem ringSystem;
    //ParticleSystem.ColorOverLifetimeModule colorModule;

    public Renderer rend;

    //Base Planet Color Values
    float rValue = 0f;
    float gValue = 0f;
    float bValue = 0f;

    //Band Color Values
    float rBandValue = 0f;
    float gBandValue = 0f;
    float bBandValue = 0f;

    //Storm Color Values
    float rStormValue = 0f;
    float gStormValue = 0f;
    float bStormValue = 0f;

    //Color Variables
    Color planetColor;
    Color bandColor;
    Color stormColor;

    bool bandButton = false;
    bool stormSpeedButton = false;

    


    //Band Variables
    float bandNumber = 0f;
    float newBandNumber = 0f;
    float bandIncrement = 0f;

    public float maxBands = 0f;
    public float minBands = 0f;
    public float bandIncrementValue = 0f;

    //Storm Variables
    float stormSpeedNumber = 0f;
    float newStormSpeedNumber = 0f;
    float stormSpeedIncrement = 0f;

    public float maxStormSpeed = 0f;
    public float minStormSpeed = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();

        bandNumber = 0f;
        bandIncrement = 0f;

        stormSpeedNumber = 0f;
        stormSpeedIncrement = 0f;

        // ringSystem = GetComponent<ParticleSystem>();

      //  colorModule = ringSystem.colorOverLifetime;

        //RingColor();

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 1))
        {
            if(bandButton)
            {
                BandEditor();
            }
            else if(stormSpeedButton)
            {
                StormEditor();
            }
        }
       
    }

    //Change the base color of planet
    public void ChangeColor(string rgbValue)
    {
        //float rValueSet, float gValueSet, float bValueSet

        string[] splittedParams = rgbValue.Split(',');

        //get the first param
        string FirstParam = splittedParams[0];
        string SecondParam = splittedParams[1];
        string ThirdParam = splittedParams[2];

        //Convert it back to int
        float rValueSet = float.Parse(FirstParam);
        float gValueSet = float.Parse(SecondParam);
        float bValueSet = float.Parse(ThirdParam);

        /* switch (atmosphereColorButton)
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


         } */

        //shaderColor = new Color(GetComponent<Renderer>().material.color.r, GetComponent<Renderer>().material.color.g, GetComponent<Renderer>().material.color.b, 
        //GetComponent<Renderer>().material.color.a);

        rValue = (rValueSet / 255);
        gValue = (gValueSet / 255);
        bValue = (bValueSet / 255);

        planetColor = new Color(rValue, gValue, bValue, 1);
        rend.material.SetColor("_Planet_Color", planetColor);
    }

    //Change the color of bands
    public void ChangeBandColor(float rValueSet, float gValueSet, float bValueSet)
    {
        /* switch (bandColorButton)
         {
             case 1:
                 rBandValue = 1f;
                 gBandValue = 0f;
                 bBandValue = 0f;
                 break;

             case 2:
                 rBandValue = 0.99215686086f;
                 gBandValue = 0.6078431361f;
                 bBandValue = 0f;
                 break;

             case 3:
                 rBandValue = 0.9607843119f;
                 gBandValue = 0.94901960604f;
                 bBandValue = 0;
                 break;

             case 4:
                 rBandValue = 0.58431372438f;
                 gBandValue = 0.97254901776f;
                 bBandValue = 0f;
                 break;

             case 5:
                 rBandValue = 0.18039215652f;
                 gBandValue = 0.79215686124f;
                 bBandValue = 0.0784313724f;
                 break;

             case 6:
                 rBandValue = 0.10980392136f;
                 gBandValue = 0.91372548846f;
                 bBandValue = 0.588235293f;
                 break;

             case 7:
                 rBandValue = 0f;
                 gBandValue = 0.88627450812f;
                 bBandValue = 1f;
                 break;

             case 8:
                 rBandValue = 0f;
                 gBandValue = 0.01568627448f;
                 bBandValue = 1f;
                 break;
         } */

        rBandValue = (rValueSet / 255);
        gBandValue = (gValueSet / 255);
        bBandValue = (bValueSet / 255);

        bandColor = new Color(rBandValue, gBandValue, bBandValue, 1);
        rend.material.SetColor("_Color_Bands", bandColor);

        
        rend.material.SetColor("_Color_Storms", bandColor);
    }

    //Change the color of storms
    public void ChangeStormColor(float stormColorButton)
    {
        switch (stormColorButton)
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
        }
        

        stormColor = new Color(rStormValue, gStormValue, bStormValue, 1f);
        rend.material.SetColor("_Color_Storms", stormColor);
    }

    //Edits band number with touch
    void BandEditor()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            Debug.Log(">0");

            bandIncrement += bandIncrementValue;
            newBandNumber = (bandNumber + bandIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber > maxBands)
            {
                newBandNumber = maxBands;
            }
            if (newBandNumber < minBands)
            {
                newBandNumber = minBands;
            }

            rend.material.SetFloat("_Bands", newBandNumber);

            Debug.Log("Band Change" + newBandNumber);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {
            Debug.Log("<0");

            bandIncrement -= bandIncrementValue;
            newBandNumber = (bandNumber + bandIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber < minBands)
            {
                newBandNumber = minBands;
            }
            if (newBandNumber > maxBands)
            {
                newBandNumber = maxBands;
            } 
            rend.material.SetFloat("_Bands", newBandNumber);

            Debug.Log("Band Change" + newBandNumber);
        }
    }


    //Edits storm speed with touch
    void StormEditor()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);


        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            Debug.Log(">0");

            stormSpeedIncrement += 0.01f;
            newStormSpeedNumber = (stormSpeedNumber + stormSpeedIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newStormSpeedNumber > maxStormSpeed)
            {
                newStormSpeedNumber = maxStormSpeed;
            }
            if (newStormSpeedNumber < minStormSpeed)
            {
                newStormSpeedNumber = minStormSpeed;
            }

            rend.material.SetFloat("_Rotation_Speed", newStormSpeedNumber);

            Debug.Log("Storm Speed Change" + newStormSpeedNumber);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {
            Debug.Log("<0");

            stormSpeedIncrement -= 0.01f;
            newStormSpeedNumber = (stormSpeedNumber + stormSpeedIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newStormSpeedNumber < minStormSpeed)
            {
                newStormSpeedNumber = minStormSpeed;
            }
            if (newStormSpeedNumber > maxStormSpeed)
            {
                newStormSpeedNumber = maxStormSpeed;
            }


            rend.material.SetFloat("_Rotation_Speed", newStormSpeedNumber);


            Debug.Log("Storm Speed Number" + newStormSpeedNumber);
        }
    }

    public void ActivateBands()
    {
        bandButton = true;
    }

    public void DeactivateBands()
    {
        bandButton = false;
    }

    public void ActivateStormSpeed()
    {
        stormSpeedButton = true;
    }

    public void DeactivateStormSpeed()
    {
        stormSpeedButton = false;
    }

    public void ResetRotation()
    {
        //Resets position to default
        Vector3 defaultPosition = new Vector3(0, 0, 0);
        gameObject.transform.position = defaultPosition;

        //Resets rotation to default
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.rotation = defaultRotation;

    }

}
