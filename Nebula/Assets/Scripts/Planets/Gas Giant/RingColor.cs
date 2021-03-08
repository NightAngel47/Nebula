using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingColor : MonoBehaviour
{
    public ParticleSystem theRings;

    public Color[] primaryRingColors;
    public Color[] secondaryRingColors;

    void Start()
    {
        theRings = GetComponent<ParticleSystem>();
    }

    public void ChangeRingColor(int colorSelected)
    {
        var main = theRings.main;
        main.startColor = new ParticleSystem.MinMaxGradient(primaryRingColors[colorSelected], main.startColor.colorMax);
    }

    public void ChangeSecondaryRingColor(int colorSelected)
    {
        var main = theRings.main;
        main.startColor = new ParticleSystem.MinMaxGradient(main.startColor.colorMin, secondaryRingColors[colorSelected]);
    }
}
