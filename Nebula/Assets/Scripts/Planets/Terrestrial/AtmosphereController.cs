using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    public static bool debugRotationLock;

    bool atmosphereButton;

    public Renderer rend;

    public float atmosphereIncrementValue = 0f;
    public float atmosphereMax;
    public float atmosphereMin;

    public Color[] atmosphereColors;

    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        #region debug atmosphere density
        #if UNITY_EDITOR
        if (DebugController.debugEnabled && atmosphereButton)
        {
            if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                PlayAudio();
                DebugControlDensity();
            }
            else
            {
                debugRotationLock = false;
                source.Stop();
            }
        }
        #endif
        #endregion

        if (Input.touchCount == 1 && atmosphereButton)
        {
            PlayAudio();
            ControlDensity();
        }
        else
        {
            source.Stop();
        }
    }

    void ControlDensity()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        float newAlpha = rend.material.GetFloat("_strength");

        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            //Debug.Log(">0");

            newAlpha += atmosphereIncrementValue;
            if (newAlpha > atmosphereMax)
            {
                newAlpha = atmosphereMax;
            }
            rend.material.SetFloat("_strength", newAlpha);

            source.pitch += atmosphereIncrementValue;

            Debug.Log("Alpha Change" + newAlpha);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {
            //Debug.Log("<0");

            newAlpha -= atmosphereIncrementValue;
            if (newAlpha < atmosphereMin)
            {
                newAlpha = atmosphereMin;
            }
            rend.material.SetFloat("_strength", newAlpha);

            source.pitch -= atmosphereIncrementValue;

            Debug.Log("Alpha Change" + newAlpha);
        }
    }

    #region debug atmosphere density
    #if UNITY_EDITOR
    void DebugControlDensity()
    {
        float newAlpha = rend.material.GetFloat("_strength");

        if (Input.GetKey(KeyCode.RightArrow))
        {
            debugRotationLock = true;

            newAlpha += atmosphereIncrementValue;
            if (newAlpha > atmosphereMax)
            {
                newAlpha = atmosphereMax;
            }
            rend.material.SetFloat("_strength", newAlpha);

            source.pitch += atmosphereIncrementValue;

            Debug.Log("Alpha Change" + newAlpha);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            debugRotationLock = true;

            newAlpha -= atmosphereIncrementValue;
            if (newAlpha < atmosphereMin)
            {
                newAlpha = atmosphereMin;
            }
            rend.material.SetFloat("_strength", newAlpha);

            source.pitch -= atmosphereIncrementValue;

            Debug.Log("Alpha Change" + newAlpha);
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
            source.Play();
    }

    public void ChangeColor(int colorSelected)
    {
        rend.material.SetColor("_color", new Color(atmosphereColors[colorSelected].r, atmosphereColors[colorSelected].g, atmosphereColors[colorSelected].b, rend.material.GetFloat("_strength")));
    }

    public void ActivateAtmosphere()
    {
        atmosphereButton = true;
    }

    public void DeactivateAtmosphere()
    {
        atmosphereButton = false;
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