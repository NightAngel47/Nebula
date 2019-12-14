using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{

    #region debug rotation lock
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public static bool debugRotationLock;
#endif

    #endregion

    /// <summary>
    /// Tracks selected tool
    /// </summary>
    private ToolSelect toolSelect;

    public Renderer rend;

    public float atmosphereIncrementValue = 0f;
    public float atmosphereMax;
    public float atmosphereMin;

    public Color[] atmosphereColors;

    public AudioSource source;
    private static readonly int Color = Shader.PropertyToID("_color");
    private static readonly int Strength = Shader.PropertyToID("_strength");

    // Start is called before the first frame update
    void Start()
    {
        toolSelect = FindObjectOfType<ToolSelect>();
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1 && toolSelect.toolSelected == ToolSelect.Tools.Atmosphere)
        {
            PlayAudio();
            ControlDensity();
        }
        
        #region debug atmosphere density
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (!DebugController.DebugEnabled || toolSelect.toolSelected != ToolSelect.Tools.Atmosphere) return;
        
        if (Math.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            PlayAudio();
            DebugControlDensity();
        }
        else if(Math.Abs(Input.GetAxis("Horizontal")) < 0.01f)
        {
            debugRotationLock = false;
            source.Stop();
        }
#endif
        #endregion
    }

    void ControlDensity()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        float newAlpha = rend.material.GetFloat(Strength);

        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            //Debug.Log(">0");

            newAlpha += atmosphereIncrementValue;
            if (newAlpha > atmosphereMax)
            {
                newAlpha = atmosphereMax;
            }
            rend.material.SetFloat(Strength, newAlpha);

            source.pitch += atmosphereIncrementValue;

            //Debug.Log("Alpha Change" + newAlpha);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {
            //Debug.Log("<0");

            newAlpha -= atmosphereIncrementValue;
            if (newAlpha < atmosphereMin)
            {
                newAlpha = atmosphereMin;
            }
            rend.material.SetFloat(Strength, newAlpha);

            source.pitch -= atmosphereIncrementValue;

            //Debug.Log("Alpha Change" + newAlpha);
        }
        
        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            source.Stop();
        }
    }

    #region debug atmosphere density
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    void DebugControlDensity()
    {
        float newAlpha = rend.material.GetFloat(Strength);

        if (Input.GetAxis("Horizontal") > 0)
        {
            debugRotationLock = true;

            newAlpha += atmosphereIncrementValue;
            if (newAlpha > atmosphereMax)
            {
                newAlpha = atmosphereMax;
            }
            rend.material.SetFloat(Strength, newAlpha);

            source.pitch += atmosphereIncrementValue;

            //Debug.Log("Alpha Change" + newAlpha);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            debugRotationLock = true;

            newAlpha -= atmosphereIncrementValue;
            if (newAlpha < atmosphereMin)
            {
                newAlpha = atmosphereMin;
            }
            rend.material.SetFloat(Strength, newAlpha);

            source.pitch -= atmosphereIncrementValue;

            //Debug.Log("Alpha Change" + newAlpha);
        }
        else
        {
            debugRotationLock = false;
        }
    }
#endif
    #endregion

    void PlayAudio()
    {
        if (!source.isPlaying)
        {
            source.Play();
        }
    }

    public void ChangeColor(int colorSelected)
    {
        rend.material.SetColor(Color, new Color(atmosphereColors[colorSelected].r, atmosphereColors[colorSelected].g, atmosphereColors[colorSelected].b, rend.material.GetFloat(Strength)));
    }
}