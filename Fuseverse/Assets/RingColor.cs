using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColor : MonoBehaviour
{
    public ParticleSystem theRings;
    public Gradient publicColorGradient;

    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;

    float rValue;
    float gValue;
    float bValue;
    float aValue;

    void Start()
    {
       
    }
    private void Update()
    {
        //var main = ringColor.main;
        

    }
    public void ChangeRingColor(float RingColorButton)
    {
        switch (RingColorButton)
        {
            case 1:
                rValue = 1f;
                gValue = 0f;
                bValue = 0f;
                aValue = 0f;
                break;

            case 2:
                rValue = 0.99215686086f;
                gValue = 0.6078431361f;
                bValue = 0f;
                aValue = 0f;
                break;

            case 3:
                rValue = 0.9607843119f;
                gValue = 0.94901960604f;
                bValue = 0;
                aValue = 0f;
                break;

            case 4:
                rValue = 0.58431372438f;
                gValue = 0.97254901776f;
                bValue = 0f;
                aValue = 0f;
                break;

            case 5:
                rValue = 0.18039215652f;
                gValue = 0.79215686124f;
                bValue = 0.0784313724f;
                aValue = 0f;
                break;

            case 6:
                rValue = 0.10980392136f;
                gValue = 0.91372548846f;
                bValue = 0.588235293f;
                aValue = 0f;
                break;

            case 7:
                rValue = 0f;
                gValue = 0.88627450812f;
                bValue = 1f;
                aValue = 0f;
                break;

            case 8:
                rValue = 0f;
                gValue = 0.01568627448f;
                bValue = 1f;
                aValue = 0f;
                break;
        }

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = new Color(rValue, gValue, bValue);
        colorKey[0].time = 0.0f;
        colorKey[1].color = new Color(rValue, gValue, bValue);
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[1];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;

        publicColorGradient.SetKeys(colorKey, alphaKey);

        ParticleSystem.MainModule ringColor = theRings.main;
        ringColor.startColor = publicColorGradient.Evaluate(Random.Range(0f, 0f));
    }
}
