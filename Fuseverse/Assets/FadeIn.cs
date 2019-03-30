using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public Image HandRight;
    public Image HandLeft;

    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if (gameObject.activeSelf == true)
        {
            HandRight.CrossFadeAlpha(1, 2.0f, false);
            HandLeft.CrossFadeAlpha(1, 2.0f, false);
        }
        else
        {
            HandRight.CrossFadeAlpha(0, 2.0f, false);
            HandLeft.CrossFadeAlpha(0, 2.0f, false);
        }
    }
}
