using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrestrialRotation : MonoBehaviour
{
    public AudioSource source;
    
    #region debug terrestrial rotation
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    [SerializeField] private float rotationSpeedDebug;
    [SerializeField] private float rotationFastSpeedDebug;
#endif
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            Rotation();
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            source.Stop();
        }
        
#if UNITY_EDITOR || DEVELOPMENT_BUILD
        if (Input.touchCount == 0 && !DebugController.DebugEnabled) return; 
        
        DebugRotation();
#endif
    }

    void Rotation()
    {
        var rotation = Camera.main.transform.rotation;
        var xRot = rotation.x;
        var yRot = rotation.y;
        
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            xRot += touchDeltaPosition.y;
            yRot += -touchDeltaPosition.x;

            if (!source.isPlaying)
            {
                source.Play();
            }

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
    }


    #region debug terrestrial rotation
#if UNITY_EDITOR || DEVELOPMENT_BUILD
    void DebugRotation()
    {
        float rotationSpeed = Input.GetKey(KeyCode.LeftShift) ? rotationFastSpeedDebug : rotationSpeedDebug;

        var rotation = Camera.main.transform.rotation;
        var xRot = rotation.x;
        var yRot = rotation.y;

        if (AtmosphereController.debugRotationLock) return;
        
        Vector3 direction = new Vector3(xRot + Input.GetAxis("Vertical"), yRot + Input.GetAxis("Horizontal"), 0);

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Mathf.Abs(Input.GetAxis("Horizontal")) > 0 )
        {
            transform.Rotate(direction * rotationSpeed, Space.World);
            
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
        else if(Mathf.Abs(Mathf.Abs(Input.GetAxis("Vertical"))) < 0.01f || Math.Abs(Mathf.Abs(Input.GetAxis("Horizontal"))) < 0.01f )
        {
            source.Stop();
        }
    }
#endif
    #endregion
}
