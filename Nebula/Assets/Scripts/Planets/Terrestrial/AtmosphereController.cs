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
        #region debug atmosphere density
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (DebugController.DebugEnabled && toolSelect.toolSelected == ToolSelect.Tools.Atmosphere)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
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

        if (Input.touchCount == 1 && toolSelect.toolSelected == ToolSelect.Tools.Atmosphere)
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
#if UNITY_EDITOR || DEVELOPMENT_BUILD
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