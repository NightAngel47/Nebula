using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;
    public float rotationSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if ((Input.touchCount == 2))
        {

            Touch firstTouch = Input.GetTouch(0);
            Vector2 distance = firstTouch.deltaPosition * Time.deltaTime;

            switch (firstTouch.phase)
            {
                case TouchPhase.Began:
                    startPos = firstTouch.position;
                    break;

                case TouchPhase.Moved:
                    direction = firstTouch.position - startPos;
                    if (direction.x < 0)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.down, distance.magnitude * rotationSpeed);
                    }
                    else if(direction.x > 0)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.up, distance.magnitude * rotationSpeed);
                    }
                    break;

                case TouchPhase.Ended:
                    break;
            }
           
        }

    }
}
