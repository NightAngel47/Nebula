using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GasMenUIChanger : MonoBehaviour
{
    //Defining Main UI Buttons
    public Button Color_Button;
    public Button Storms_Button;
    public Button Rings_Button;

    //Defining Canvas Renderers.
    public GameObject gasColorCanvas;
    public GameObject stormsCanvas;
    public GameObject ringsCanvas;


    void Start()
    {
        //Setting listeners for each button to reduce UI load
        Color_Button.onClick.AddListener(onClickColor);
        Storms_Button.onClick.AddListener(onClickStorms);
        Rings_Button.onClick.AddListener(onClickRings);

        //Setting no menu
        gasColorCanvas.SetActive(false);
        stormsCanvas.SetActive(false);
        ringsCanvas.SetActive(false);
    }

    //These Will Switch Right UI presets as well as modifying the Tool Tips

    void onClickColor()
    {
        Debug.Log("Changing Menu to Color Config");
        gasColorCanvas.SetActive(true);
        stormsCanvas.SetActive(false);
        ringsCanvas.SetActive(false);
    }
    void onClickStorms()
    {
        Debug.Log("Changing Menu to Storms Config");
        gasColorCanvas.SetActive(false);
        stormsCanvas.SetActive(true);
        ringsCanvas.SetActive(false);
    }
    void onClickRings()
    {
        Debug.Log("Changing Menu to Rings Config");
        gasColorCanvas.SetActive(false);
        stormsCanvas.SetActive(false);
        ringsCanvas.SetActive(true);
    }

}
