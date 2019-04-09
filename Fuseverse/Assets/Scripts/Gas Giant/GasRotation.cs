using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasRotation : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public float rotationSpeed = 0;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RingRotation.canRotate = true;
    }

    // Update is called once per frame
    void Update()
    {
        var xRot = transform.rotation.x;
        var yRot = transform.rotation.y;

        if ((Input.touchCount == 2) && RingRotation.canRotate)
        {

            Touch firstTouch = Input.GetTouch(0);
            //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //rb.AddTorque(transform.up * -touchDeltaPosition.x);
                //rb.AddTorque(transform.right * touchDeltaPosition.y);

                xRot += (touchDeltaPosition.y);
                yRot += (-touchDeltaPosition.x);


                transform.Rotate(xRot, yRot, 0, Space.Self);
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




    /*

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        RingRotation.canRotate = true;

    }

    // Update is called once per frame
    void Update()
    {


        if ((Input.touchCount == 2) && (RingRotation.canRotate))
        {

            Touch firstTouch = Input.GetTouch(0);
            //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                //rb.AddTorque(transform.up * -touchDeltaPosition.x);
               // rb.AddTorque(transform.right * touchDeltaPosition.y);


                var xRot = transform.rotation.x;
                var yRot = transform.rotation.y;

                if (Input.GetTouch(0).deltaPosition.magnitude < 0)
                {
                    xRot += (touchDeltaPosition.x * Time.deltaTime) * -rotationSpeed;
                    yRot += (touchDeltaPosition.y * Time.deltaTime) * -rotationSpeed;


                    transform.Rotate(xRot, yRot, 0, Space.Self);
                    
                }
                else if (Input.GetTouch(0).deltaPosition.magnitude > 0)
                {
                    xRot += (touchDeltaPosition.x * Time.deltaTime) * rotationSpeed;
                    yRot += (touchDeltaPosition.y * Time.deltaTime) * rotationSpeed;

                    transform.Rotate(xRot, yRot, 0, Space.Self);

                }
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
    */
}
