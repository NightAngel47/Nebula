using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenUIColor : MonoBehaviour
{
    public int selectedColor;

    //12 Colors & Biomes in total, These are also biomes, but for code simplicity I address them all as colors.

    public Button Pink;         //also 0 Tropical 
    public Button Red;          //also 1 Volcanic
    public Button Orange;       //also 2 Savannah
    public Button Yellow;       //also 3 Desert
    public Button Green;        //also 4 Forest
    public Button Turquoise;    //Also 5 Plains
    public Button Blue;         //Also 6 Ocean
    public Button Indigo;       //Also 7 Deep Ocean
    public Button Purple;       //Also 8 Tundrea
    public Button Black;        //Also 9 Burnt
    public Button Brown;        //Also 10 Boreal
    public Button White;        //also 11 Arctic

    void Start()
    {
        selectedColor = 0;

        //Creating Listeners for all colors to reduce UI load

        Pink.onClick.AddListener(onClickPink);
        Red.onClick.AddListener(onClickRed);
        Orange.onClick.AddListener(onClickOrange);
        Yellow.onClick.AddListener(onClickYellow);
        Green.onClick.AddListener(onClickGreen);
        Turquoise.onClick.AddListener(onClickTurquoise);
        Blue.onClick.AddListener(onClickBlue);
        Indigo.onClick.AddListener(onClickIndigo);
        Purple.onClick.AddListener(onClickPurple);
        Black.onClick.AddListener(onClickBlack);
        Brown.onClick.AddListener(onClickBrown);
        White.onClick.AddListener(onClickWhite);
    }


    //This is a Break Line here to break start from game functions

    void onClickPink()
    {
        selectedColor = 0;
    }

    void onClickRed()
    {
        selectedColor = 1;
    }

    void onClickOrange()
    {
        selectedColor = 2;
    }

    void onClickYellow()
    {
        selectedColor = 3;
    }

    void onClickGreen()
    {
        selectedColor = 4;
    }

    void onClickTurquoise()
    {
        selectedColor = 5;
    }

    void onClickBlue()
    {
        selectedColor = 6;
    }

    void onClickIndigo()
    {
        selectedColor = 7;
    }

    void onClickPurple()
    {
        selectedColor = 8;
    }

    void onClickBlack()
    {
        selectedColor = 9;
    }

    void onClickBrown()
    {
        selectedColor = 10;
    }

    void onClickWhite()
    {
        selectedColor = 11;
    }

}
