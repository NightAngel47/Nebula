using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrestrialRotation : MonoBehaviour
{
    public AudioSource source;

    // Update is called once per frame
    void Update()
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
                    source.Play();

                //transform.Rotate(xRot, yRot, 0, Space.World);
                transform.Rotate(0, yRot, 0, Space.World);
            }
        }
        else
            source.Stop();
    }
}
