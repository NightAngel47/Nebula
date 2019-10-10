using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRotation : MonoBehaviour
{
    public AudioSource source;

    [SerializeField] private float gasRotationSpeedDebug;
    [SerializeField] private float gasRotationFastSpeedDebug;

    // Start is called before the first frame update
    void Start()
    {
        RingRotation.canRingRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DebugController.debugEnabled && !RingRotation.canRingRotate)
        {
            if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                DebugGasRotator();
            }
        }

        if ((Input.touchCount == 2) && !RingRotation.canRingRotate)
        {
            GasRotator();
        }
    }

    void GasRotator()
    {
        var xRot = Camera.main.transform.rotation.x;
        var yRot = Camera.main.transform.rotation.y;

        if ((Input.touchCount == 2) && !RingRotation.canRingRotate)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                xRot += (touchDeltaPosition.y);
                yRot += (-touchDeltaPosition.x);

                if (!source.isPlaying)
                {
                    source.Play();
                }

                transform.Rotate(xRot, yRot, 0, Space.World);
            }
        }
        else
        {
            source.Stop();
        }
    }

    void DebugGasRotator()
    {
        float rotationSpeed = gasRotationSpeedDebug;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rotationSpeed = gasRotationFastSpeedDebug;
        }
        else
        {
            rotationSpeed = gasRotationSpeedDebug;
        }

        var xRot = Camera.main.transform.rotation.x;
        var yRot = Camera.main.transform.rotation.y;
        //var zRot = Camera.main.transform.rotation.z;

        if (Input.GetKey(KeyCode.RightArrow))
        {
                yRot -= rotationSpeed;

                if (!source.isPlaying)
                {
                    source.Play();
                }

            if(Input.GetKey(KeyCode.UpArrow))
            {
                xRot += rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                xRot -= rotationSpeed;
            }

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {

            yRot += rotationSpeed;

            if (Input.GetKey(KeyCode.UpArrow))
            {
                xRot += rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                xRot -= rotationSpeed;
            }

            if (!source.isPlaying)
            {
                source.Play();
            }

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                yRot += rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                yRot -= rotationSpeed;
            }


            if (!source.isPlaying)
            {
                source.Play();
            }

            xRot += rotationSpeed;

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                yRot += rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                yRot -= rotationSpeed;
            }


            if (!source.isPlaying)
            {
                source.Play();
            }

            xRot -= rotationSpeed;

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
        else
        {
            source.Stop();
        }
    }
}
