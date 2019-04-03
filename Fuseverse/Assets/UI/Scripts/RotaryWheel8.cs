using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotaryWheel8 : MonoBehaviour
{
    public Button Ocean;
    public Button Tiaga;
    public Button Desert;
    public Button Forest;
    public Button Rock;
    public Button Plains;
    public Button Savanna;
    public Button Tundra;

    public float speed = 50f;
    public float Zed = 0f;
    public GameObject Rotary;




    // Start is called before the first frame update
    void Start()
    {
        Ocean.onClick.AddListener(tapOcean);
        Tiaga.onClick.AddListener(tapTiaga);
        Desert.onClick.AddListener(tapDesert);
        Forest.onClick.AddListener(tapForest);
        Rock.onClick.AddListener(tapRock);
        Plains.onClick.AddListener(tapPlains);
        Savanna.onClick.AddListener(tapSavanna);
        Tundra.onClick.AddListener(tapTundra);

    } 

// Update is called once per frame
void FixedUpdate()
    {

        var _targetRotation = Quaternion.Euler(0, 0, Zed);

        Rotary.transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, speed * Time.deltaTime);

    }
    void tapOcean()
    {
        Zed = 0f;
    }
    void tapTiaga()
    {
        Zed = 45f;
    }
    void tapDesert()
    {
        Zed = 90f;
    }
    void tapForest()
    {
        Zed = 135f;
    }
    void tapRock()
    {
        Zed = 180f;
    }
    void tapPlains()
    {
        Zed = 225f;
    }
    void tapSavanna()
    {
        Zed = 270f;
    }
    void tapTundra()
    {
        Zed = 315f;
    }
}
