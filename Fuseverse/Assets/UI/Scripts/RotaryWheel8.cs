using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotaryWheel8 : MonoBehaviour
{
    public Button But1;
    public Button But2;
    public Button But3;
    public Button But4;
    public Button But5;
    public Button But6;
    public Button But7;
    public Button But8;

    private float speed = 125f;
    public float Zed = 0f;
    public GameObject Rotary;

    public float Z1 = 0f;
    public float Z2 = 0f;
    public float Z3 = 0f;
    public float Z4 = 0f;
    public float Z5 = 0f;
    public float Z6 = 0f;
    public float Z7 = 0f;
    public float Z8 = 0f;

    public float smoothing = 2f;

    void Start()
    {
        if (But1 != null)
            But1.onClick.AddListener(TapBut1);
        if (But2 != null)
            But2.onClick.AddListener(TapBut2);
        if (But3 != null)
            But3.onClick.AddListener(TapBut3);
        if (But4 != null)
            But4.onClick.AddListener(TapBut4);
        if (But5 != null)
            But5.onClick.AddListener(TapBut5);
        if (But6 != null)
            But6.onClick.AddListener(TapBut6);
        if (But7 != null)
            But7.onClick.AddListener(TapBut7);
        if (But8 != null)
            But8.onClick.AddListener(TapBut8);

    } 

    void FixedUpdate()
    {

        var _targetRotation = Quaternion.Euler(0, 0, Zed);

        Rotary.transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, speed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, smoothing * Time.deltaTime);
    }
    void TapBut1()
    {
        Zed = Z1;
    }
    void TapBut2()
    {
        Zed = Z2;
    }
    void TapBut3()
    {
        Zed = Z3;
    }
    void TapBut4()
    {
        Zed = Z4;
    }
    void TapBut5()
    {
        Zed = Z5;
    }
    void TapBut6()
    {
        Zed = Z6;
    }
    void TapBut7()
    {
        Zed = Z7;
    }
    void TapBut8()
    {
        Zed = Z8;
    }
}
