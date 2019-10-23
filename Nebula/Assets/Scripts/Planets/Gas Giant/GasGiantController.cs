using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GasGiantController : MonoBehaviour
{
    public Renderer rend;

    #region debug band editor rotation lock
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    public static bool gasDebugRotationLock;
#endif
    #endregion

    //Color Variables
    Color planetColor;
    Color bandColor;

    bool bandButton = false;

    public Color[] baseColors;
    public Color[] bandColors;

    public float maxBands = 0f;
    public float minBands = 0f;
    public float bandIncrementValue = 0f;

    private AudioSource bandSource;
    public AudioMixer mainMixer;
    public float bandEffectAmount = 500f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        bandSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        #region debug band editor
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (DebugController.debugEnabled)
        {
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow)) && bandButton)
            {
                PlayBandsAudio();
                DebugBandEditor();
                gasDebugRotationLock = true;
            }
            else
            {
                gasDebugRotationLock = false;
            }
        }
#endif
        #endregion

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && bandButton)
        {
            PlayBandsAudio();
            BandEditor();
        }
        else
            bandSource.Stop();
    }

    //Change the base color of planet
    public void ChangeColor(int colorSelected)
    {
        rend.material.SetColor("_Planet_Color", baseColors[colorSelected]);
    }

    //Change the color of bands
    public void ChangeBandColor(int colorSelected)
    {
        rend.material.SetColor("_Color_Bands", bandColors[colorSelected]);
    }

    //Edits band number with touch
    void BandEditor()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        float newBandNumber = rend.material.GetFloat("_Bands");

        float bandsEffect;
        mainMixer.GetFloat("BandsEffect", out bandsEffect);

        if (magFirstTouchPrevPos.y < firstTouch.position.y)
        {
            //Debug.Log(">0");

            newBandNumber += bandIncrementValue;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber > maxBands)
                newBandNumber = maxBands;
            else
                mainMixer.SetFloat("BandsEffect", bandsEffect -= bandIncrementValue * bandEffectAmount);

            rend.material.SetFloat("_Bands", newBandNumber);


            Debug.Log("Band Change" + newBandNumber);
        }
        else if (magFirstTouchPrevPos.y > firstTouch.position.y)
        {
            //Debug.Log("<0");

            newBandNumber -= bandIncrementValue;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber < minBands)
                newBandNumber = minBands;
            else
                mainMixer.SetFloat("BandsEffect", bandsEffect += bandIncrementValue * bandEffectAmount);


            rend.material.SetFloat("_Bands", newBandNumber);


            Debug.Log("Band Change" + newBandNumber);
        }
    }

    #region debug band editor
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    void DebugBandEditor()
    {
        float newBandNumber = rend.material.GetFloat("_Bands");

        float bandsEffect;
        mainMixer.GetFloat("BandsEffect", out bandsEffect);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            newBandNumber += bandIncrementValue;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber > maxBands)
            {
                newBandNumber = maxBands;
            }
            else
            {
                mainMixer.SetFloat("BandsEffect", bandsEffect -= bandIncrementValue * bandEffectAmount);
            }
            rend.material.SetFloat("_Bands", newBandNumber);


            Debug.Log("Band Change" + newBandNumber);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            newBandNumber -= bandIncrementValue;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (newBandNumber < minBands)
            {
                newBandNumber = minBands;
            }
            else
            {
                mainMixer.SetFloat("BandsEffect", bandsEffect += bandIncrementValue * bandEffectAmount);
            }

            rend.material.SetFloat("_Bands", newBandNumber);

            Debug.Log("Band Change" + newBandNumber);
        }

    }
#endif
    #endregion

    void PlayBandsAudio()
    {
        if(!bandSource.isPlaying)
            bandSource.Play();
    }

    public void ActivateBands()
    {
        bandButton = true;
    }

    public void DeactivateBands()
    {
        bandButton = false;
    }
}