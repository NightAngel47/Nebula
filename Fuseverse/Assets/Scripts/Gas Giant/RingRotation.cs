using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public float rotationSpeed = 0;
    public Rigidbody rb;

    public GameObject planet;

    public static bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 2))
        {

            Touch firstTouch = Input.GetTouch(0);
            //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                
               if(canRotate == true)
               {
                    //    Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    /*
                    var rot = Camera.main.gameObject.transform.rotation;
                    var vectorRot = rot.eulerAngles;

                    vectorRot.x += touchDeltaPosition.x * rotationSpeed;
                    vectorRot.y += touchDeltaPosition.y * rotationSpeed;
                    vectorRot.z += touchDeltaPosition.z * rotationSpeed;
                    */
                    //    Vector3 planetPos = new Vector3(planet.transform.position.x, planet.transform.position.y, planet.transform.position.z);
                    //   transform.Rotate(planetPos, rotationSpeed);



                    //  rb.MoveRotation(Quaternion.Euler(vectorRot));

                    //Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

                    
                   //     Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                    //    rb.AddTorque(Camera.main.transform.up * -touchDeltaPosition.x);
                    //    rb.AddTorque(Camera.main.transform.right * touchDeltaPosition.y);
                    
                }
            
                if(canRotate == false)
                {
                    Vector3 touchDeltaPosition = Input.GetTouch(0).deltaPosition;



                    //var rot = transform.rotation;
                    //var vectorRot = rot.eulerAngles;

                    var xRot = Camera.main.transform.rotation.x;
                    //var zRot = transform.rotation.z;

                    //vectorRot.x += (touchDeltaPosition.x * Time.deltaTime) * rotationSpeed;
                    //vectorRot.y += (touchDeltaPosition.y * Time.deltaTime) * rotationSpeed;
                   

                    if (Input.GetTouch(0).deltaPosition.magnitude < 0)
                    {
                        xRot += (touchDeltaPosition.y * Time.deltaTime) * -rotationSpeed;
                        //zRot += (touchDeltaPosition.x * Time.deltaTime) * -rotationSpeed;


                        transform.Rotate(xRot, 0, 0, Space.World);

                        //transform.rotation = Quaternion.Euler(vectorRot);

                        /*
                        Vector3 planetPos = new Vector3(planet.transform.position.x, planet.transform.position.y, planet.transform.position.z);
                        Vector3 spin = new Vector3(vectorRot.x, 0, 0);
                        transform.RotateAround(planetPos, spin, rotationSpeed);
                        */
                    }
                    else if(Input.GetTouch(0).deltaPosition.magnitude > 0)
                    {
                        xRot += (touchDeltaPosition.y * Time.deltaTime) * rotationSpeed;
                        //zRot += (touchDeltaPosition.x * Time.deltaTime) * rotationSpeed;

                        transform.Rotate(xRot, 0, 0, Space.World);

                        //transform.rotation = Quaternion.Euler(vectorRot);

                        /*
                        Vector3 planetPos = new Vector3(planet.transform.position.x, planet.transform.position.y, planet.transform.position.z);
                        Vector3 spin = new Vector3(vectorRot.x, 0, 0);
                        transform.RotateAround(planetPos, spin, rotationSpeed);
                        */
                    }



                    //transform.rotation = Quaternion.Euler(vectorRot);

                    // rb.MoveRotation(Quaternion.Euler(vectorRot));


                    //transform.Rotate(planetPos, rotationSpeed);

                    //transform.Rotate(vectorRot.x, vectorRot.y, vectorRot.z, Space.Self);


                }
            }

        }
    }

    public void EnablePlanetRotation()
    {
        canRotate = true;
    }

    public void DisablePlanetRotation()
    {
        canRotate = false;
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
