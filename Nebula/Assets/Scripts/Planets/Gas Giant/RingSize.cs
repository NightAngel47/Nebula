using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RingSize : MonoBehaviour
{
    public ParticleSystem theRings;

    public bool ringButton;

    public float ringIncrement = 0f;
    public float ringSizeMax;
    public float ringSizeMin;

    private AudioSource ringSource;
    public AudioMixer mainMixer;
    public float effectAmount = 750f;

    void Start()
    {
        theRings = GetComponent<ParticleSystem>();
        ringSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        #region debug ring radius
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (DebugController.debugEnabled)
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && ringButton)
            {
                PlayRingsAudio();
                ChangeRingRadiusDebug();
            }
        }
#endif
        #endregion

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved && ringButton)
        {
            PlayRingsAudio();
            ChangeRingRadius();
        }
        else
            ringSource.Stop();
    }

    void ChangeRingRadius()
    {
        Touch firstTouch = Input.GetTouch(0);
        Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

        float audioEffect;
        mainMixer.GetFloat("RingsEffect", out audioEffect);

        float ringSize = theRings.shape.scale.x;

        if (magFirstTouchPrevPos.x > firstTouch.position.x)
        {
            ringSize += ringIncrement;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (ringSize > ringSizeMax)
                ringSize = ringSizeMax;
            else
                mainMixer.SetFloat("RingsEffect", audioEffect += ringIncrement * effectAmount);
        }
        else if (magFirstTouchPrevPos.x < firstTouch.position.x)
        {
            ringSize -= ringIncrement;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (ringSize < ringSizeMin)
                ringSize = ringSizeMin;
            else
                mainMixer.SetFloat("RingsEffect", audioEffect -= ringIncrement * effectAmount);
        }

        var main = theRings.shape;
        main.scale = new Vector3(ringSize, theRings.shape.scale.y, theRings.shape.scale.z);
    }

    #region debug ring radius
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    void ChangeRingRadiusDebug()
    {
        float audioEffect;
        mainMixer.GetFloat("RingsEffect", out audioEffect);

        float ringSize = theRings.shape.scale.x;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            ringSize += ringIncrement;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (ringSize > ringSizeMax)
            {
                ringSize = ringSizeMax;
            }
            else
            {
                mainMixer.SetFloat("RingsEffect", audioEffect += ringIncrement * effectAmount);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            ringSize -= ringIncrement;

            //Keeps band number from hitting a value above 1 and below 0 on slider 
            if (ringSize < ringSizeMin)
            {
                ringSize = ringSizeMin;
            }
            else
            {
                mainMixer.SetFloat("RingsEffect", audioEffect -= ringIncrement * effectAmount);
            }
        }

        var main = theRings.shape;
        main.scale = new Vector3(ringSize, theRings.shape.scale.y, theRings.shape.scale.z);
    }
#endif
    #endregion

    void PlayRingsAudio()
    {
        if (!ringSource.isPlaying)
            ringSource.Play();
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
