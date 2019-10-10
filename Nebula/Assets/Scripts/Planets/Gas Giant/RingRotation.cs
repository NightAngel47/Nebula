using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public AudioSource source;
    public float rotationSpeed = 0;
    public static bool canRingRotate = false;

    [SerializeField] private float gasRingRotationSpeedDebug;
    [SerializeField] private float gasRingRotationFastSpeedDebug;

    // Update is called once per frame
    void Update()
    {
        if(DebugController.debugEnabled)
        {
            if((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow)) && canRingRotate)
            {
                RotateRingsDebug();
                PlayAudio();
            }
        }
        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && canRingRotate == true)
        {
            RotateRings();
            PlayAudio();
        }
        else
        {
            source.Stop();
        }
    }

    void RotateRings()
    {
        Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

        var xRot = Camera.main.transform.rotation.x;

        if (Input.GetTouch(0).deltaPosition.magnitude < 0)
        {
            xRot += (touchDeltaPosition.y * Time.deltaTime) * -rotationSpeed;

            transform.Rotate(xRot, 0, 0, Space.World);
        }
        else if (Input.GetTouch(0).deltaPosition.magnitude > 0)
        {
            xRot += (touchDeltaPosition.y * Time.deltaTime) * rotationSpeed;

            transform.Rotate(xRot, 0, 0, Space.World);
        }
    }

    void RotateRingsDebug()
    {
        float ringRotationSpeed = gasRingRotationSpeedDebug;

        if(Input.GetKey(KeyCode.LeftShift))
        {
            ringRotationSpeed = gasRingRotationFastSpeedDebug;
        }
        else
        {
            ringRotationSpeed = gasRingRotationSpeedDebug;
        }

        var xRot = Camera.main.transform.rotation.x;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            xRot -= ringRotationSpeed;

            transform.Rotate(xRot, 0, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            xRot += ringRotationSpeed;
            transform.Rotate(xRot, 0, 0, Space.World);
        }
    }

    void PlayAudio()
    {
        print("here");
        if (!source.isPlaying)
            source.Play();
    }

    public void EnableRingRotation()
    {
        canRingRotate = true;
    }

    public void DisableRingRotation()
    {
        canRingRotate = false;
    }
}
