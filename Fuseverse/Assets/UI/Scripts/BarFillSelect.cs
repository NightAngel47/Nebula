using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarFillSelect : MonoBehaviour
{
        public GameObject Tab;
        public Image selection;
        public float fillSpeed = 2.0f;

    // Update is called once per frame
     void Start()
    {
        selection.fillAmount = 0;
    }
    void FixedUpdate()
        {
            if (Tab.activeInHierarchy == true)
            {
                selection.fillAmount += 1.0f / fillSpeed * Time.deltaTime;
            }
            else
            {
                selection.fillAmount -= 1.0f / fillSpeed * Time.deltaTime;
            }
        }
    }