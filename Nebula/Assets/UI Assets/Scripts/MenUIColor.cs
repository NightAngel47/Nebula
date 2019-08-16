using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenUIColor : MonoBehaviour
{
    public int selectedColor;

    //12 Colors & Biomes in total, These are also biomes, but for code simplicity I address them all as colors.

   
    public Button But1;       //also 2 Savannah
    public Button But2;       //also 3 Desert
    public Button But3;        //also 4 Forest
    public Button But4;    //Also 5 Plains
    public Button But5;         //Also 6 Ocean
    public Button But6;       //Also 7 Deep Ocean
    public Button But7;       //Also 8 Tundrea
    public Button But8;        //Also 9 Burnt

  

    void Start()
    {
        selectedColor = 0;

        //Creating Listeners for all colors to reduce UI load

        But1.onClick.AddListener(onClick1);
        But2.onClick.AddListener(onClick2);
        But3.onClick.AddListener(onClick3);
        But4.onClick.AddListener(onClick4);
        But5.onClick.AddListener(onClick5);
        But6.onClick.AddListener(onClick6);
        But7.onClick.AddListener(onClick7);
        But8.onClick.AddListener(onClick8);
    }


    //This is a Break Line here to break start from game functions


    void onClick1()
    {
        selectedColor = 1;
    }

    void onClick2()
    {
        selectedColor = 2;
    }

    void onClick3()
    {
        selectedColor = 3;
    }

    void onClick4()
    {
        selectedColor = 4;
    }

    void onClick5()
    {
        selectedColor = 5;
    }

    void onClick6()
    {
        selectedColor = 6;
    }

    void onClick7()
    {
        selectedColor = 7;
    }

    void onClick8()
    {
        selectedColor = 8;
    }

}
