using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencyIngore : MonoBehaviour {

    public Image theButton;

    // Use this for initialization
    void Start()
    {
        theButton.alphaHitTestMinimumThreshold = 0.5f;
    }
}
