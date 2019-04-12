using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereController : MonoBehaviour
{
    bool atmosphereButton;

    public Renderer rend;
    public float atmosphereIncrementValue = 0f;
    public float atmosphereMax;
    public float atmosphereMin;
    float newAlpha = 0.1f;

    public Color[] atmosphereColors;
    float rValue;
    float gValue;
    float bValue;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount == 1) && (atmosphereButton))
        {

            Touch firstTouch = Input.GetTouch(0);
            Vector2 magFirstTouchPrevPos = (firstTouch.deltaPosition + firstTouch.position);

            float newAlpha = rend.material.GetFloat("_strength");

            if (magFirstTouchPrevPos.x > firstTouch.position.x)
            {
                Debug.Log(">0");

                newAlpha += atmosphereIncrementValue;
                if (newAlpha > atmosphereMax)
                {
                    newAlpha = atmosphereMax;
                }
                rend.material.SetFloat("_strength", newAlpha);

                Debug.Log("Alpha Change" + newAlpha);
            }
            else if(magFirstTouchPrevPos.x < firstTouch.position.x)
            {
                Debug.Log("<0");

                newAlpha -= atmosphereIncrementValue;
                if (newAlpha < atmosphereMin)
                {
                    newAlpha = atmosphereMin;
                }
                rend.material.SetFloat("_strength", newAlpha);

                Debug.Log("Alpha Change" + newAlpha);
            }
        }
    }

    public void ChangeColor(int colorSelected)
    {
        rValue = atmosphereColors[colorSelected].r;
        gValue = atmosphereColors[colorSelected].g;
        bValue = atmosphereColors[colorSelected].b;

        rend.material.SetColor("_color", new Color(rValue, gValue, bValue, newAlpha));
    }

    public void ActivateAtmosphere()
    {
        atmosphereButton = true;
    }

    public void DeactivateAtmosphere()
    {
        atmosphereButton = false;
    }

    public void ResetRotation()
    {
        //Resets position to default
        Vector3 defaultPosition = new Vector3(0, 0, 0);
        gameObject.transform.position = defaultPosition;

        //Resets rotation to default
        Quaternion defaultRotation = Quaternion.Euler(0, 0, 0);
        gameObject.transform.rotation = defaultRotation;
    }
}