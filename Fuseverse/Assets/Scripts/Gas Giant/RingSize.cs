using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSize : MonoBehaviour
{
    public ParticleSystem theRings;

    public bool ringButton;
    public float ringDonutRadius;
    public float incrementValue = 0f;

    float ringIncrement;
    float newRingDonutRadius;

    public float maxRingDonutRadius = 0f;
    public float minRingDonutRadius = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 1))
        {
            if (ringButton)
            {
                ChangeRingRadius();
            }
        }
    }

    void ChangeRingRadius()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            Debug.Log(">0");

            ringIncrement += incrementValue;
            newRingDonutRadius = (ringDonutRadius + ringIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newRingDonutRadius > maxRingDonutRadius)
            {
                newRingDonutRadius = maxRingDonutRadius;
            }
            if (newRingDonutRadius < minRingDonutRadius)
            {
                newRingDonutRadius = minRingDonutRadius;
            }

            var main = theRings.shape;
            main.donutRadius = newRingDonutRadius;

            //rend.material.SetFloat("_Bands", newBandNumber);

            //Debug.Log("Band Change" + newBandNumber);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {

            ringIncrement -= incrementValue;
            newRingDonutRadius = (ringDonutRadius + ringIncrement);

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newRingDonutRadius < minRingDonutRadius)
            {
                newRingDonutRadius = minRingDonutRadius;
            }
            if (newRingDonutRadius > maxRingDonutRadius)
            {
                newRingDonutRadius = maxRingDonutRadius;
            }

            var main = theRings.shape;
            main.donutRadius = newRingDonutRadius;

            //rend.material.SetFloat("_Bands", newBandNumber);

            //Debug.Log("Band Change" + newBandNumber);
        }
    }

    public void SetRingButtonTrue()
    {
        ringButton = true;
    }

    public void SetRingButtonFalse()
    {
        ringButton = false;
    }

}
