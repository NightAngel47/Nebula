using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColor : MonoBehaviour
{
    public ParticleSystem theRings;
    //public Gradient publicColorGradient;

    //ParticleSystem.ColorOverLifetimeModule colorModule;

    /*
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    public Gradient publicColorGradient2;

    GradientColorKey[] colorKey2;
    GradientAlphaKey[] alphaKey2;
    */

    float rValue;
    float gValue;
    float bValue;
    float aValue;

    float rValue2;
    float gValue2;
    float bValue2;

    void Start()
    {
      
      //  var main = theRings.main;
       // main.startColor = new ParticleSystem.MinMaxGradient(Color.red); // simple color
        //main.startColor = new ParticleSystem.MinMaxGradient(myGradient); // gradient
      //  main.startColor = new ParticleSystem.MinMaxGradient(Color.red, Color.green); // random between 2 colors
        //main.startColor = new ParticleSystem.MinMaxGradient(myGradient, myOtherGradient);
    }
    private void Update()
    {
        //var main = ringColor.main;
       // colorModule = theRings.colorOverLifetime;



    }
    public void ChangeRingColor(float RingColorButton)
    {
        switch (RingColorButton)
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

        SetColor();
    }

    public void ChangeSecondaryRingColor(float SecondaryRingColorButton)
    {
        switch (SecondaryRingColorButton)
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
        }

        SetColor();
    }

    void SetColor()
    {
        Color color1 = new Color(rValue, gValue, bValue);
        Color color2 = new Color(rValue2, gValue2, bValue2);

        var main = theRings.main;
        main.startColor = new ParticleSystem.MinMaxGradient(color1, color2); // random between 2 colors

    }
}
