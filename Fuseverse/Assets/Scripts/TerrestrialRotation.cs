using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrestrialRotation : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public float rotationSpeed = 0;

    // Update is called once per frame
    void Update()
    {
        var xRot = Camera.main.transform.position.x;
        var yRot = Camera.main.transform.position.y;

        if ((Input.touchCount == 2))
        {

            Touch firstTouch = Input.GetTouch(0);
            //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //rb.AddTorque(Camera.main.transform.up * -touchDeltaPosition.x);
                //rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);

                 xRot += (touchDeltaPosition.y);
                 yRot += (-touchDeltaPosition.x);


                 transform.Rotate(xRot, yRot, 0, Space.World);
            }
        }
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
