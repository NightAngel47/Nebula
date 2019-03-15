using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
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
        

        if ((Input.touchCount == 2) && (RingRotation.canRotate))
        {

            Touch firstTouch = Input.GetTouch(0);
            //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            if(Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                rb.AddTorque(Camera.main.transform.up * -touchDeltaPosition.x);
                rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);
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
