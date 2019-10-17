using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrestrialRotation : MonoBehaviour
{
    public AudioSource source;

    [SerializeField] private float rotationSpeedDebug;
    [SerializeField] private float rotationFastSpeedDebug;

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Rotation();

        if(DebugController.debugEnabled)
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetMouseButton(0))
            {
                DebugRotation();
            }
        }
    }

    void Rotation()
    {
        //var xRot = Camera.main.transform.rotation.x;
        var yRot = Camera.main.transform.rotation.y;

        if ((Input.touchCount == 2))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //xRot += (touchDeltaPosition.y);
                yRot += (-touchDeltaPosition.x);

                if (!source.isPlaying)
                {
                    source.Play();
                }
                //transform.Rotate(xRot, yRot, 0, Space.World);
                transform.Rotate(0, yRot, 0, Space.World);
            }
        }
        else
        {
            source.Stop();
        }
    }

    void DebugRotation()
    {
        float rotationSpeed = rotationSpeedDebug;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rotationSpeed = rotationFastSpeedDebug;
        }
        else
        {
            rotationSpeed = rotationSpeedDebug;
        }

        var xRot = Camera.main.transform.rotation.x;
        var yRot = Camera.main.transform.rotation.y;
        //var zRot = Camera.main.transform.rotation.z;

        if (DebugController.mouseRotationEnabled)
        {
            //Following mouse rotation code from https://www.youtube.com/watch?v=kplusZYqBok
            if (Input.GetMouseButton(0))
            {
                mPosDelta = Input.mousePosition - mPrevPos;
                if (Vector3.Dot(transform.up, Vector3.up) >= 0)
                {
                    transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }
                else
                {
                    transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
                }

                transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
            }
        }

        mPrevPos = Input.mousePosition;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            yRot -= rotationSpeed;

            if (!source.isPlaying)
            {
                source.Play();
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                xRot += rotationSpeed;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                xRot -= rotationSpeed;
            }

            transform.Rotate(xRot, yRot, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
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
        else if (Input.GetKey(KeyCode.UpArrow))
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
        else if (Input.GetKey(KeyCode.DownArrow))
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
