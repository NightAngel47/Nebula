﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public float rotationSpeed = 0;
    public Rigidbody rb;

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
                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

                rb.AddTorque(gameObject.transform.up * touchDeltaPosition.x);
                rb.AddTorque(gameObject.transform.right * touchDeltaPosition.y);
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


}
