﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public GameObject rotatehands;

    private float inputTimer;
    void Start()
    {
        inputTimer = 0;
    }
    void Update()
    {
        inputTimer += Time.deltaTime;
        //INPUT CHECK
        if (Input.touchCount > 0)
        {
            inputTimer = 0;
            rotatehands.SetActive(false);
        }
        if (inputTimer >= 10f)
        {
            rotatehands.SetActive(true);
        }
    }
}