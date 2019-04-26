using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRotation : MonoBehaviour
{
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        RingRotation.canRingRotate = false;
    }

    // Update is called once per frame
    void Update()
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
                    source.Play();

                transform.Rotate(xRot, yRot, 0, Space.World);
            }

        }
        else
            source.Stop();
    }
}
