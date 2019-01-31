using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotation : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;

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
        

            switch(firstTouch.phase)
            {
                case TouchPhase.Began:
                    startPos = firstTouch.position;
                    break;

                case TouchPhase.Moved:
                    direction = firstTouch.position - startPos;
                    if (direction.x < 0)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.down, 20 * Time.deltaTime);
                    }
                    else if(direction.x > 0)
                    {
                        transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
                    }
                    break;

                case TouchPhase.Ended:
                    break;
            }
           
        }

    /*
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
          
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(Vector3.zero, Vector3.down, 20 * Time.deltaTime);
        }
        */


    }
}
