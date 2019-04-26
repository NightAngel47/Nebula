using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompassRotation : MonoBehaviour
{
    Vector3 originalCompassPos;
    Quaternion originalCompassRot;
    Vector3 originalPlanetPos;
    Quaternion originalPlanetRot;
    GameObject planet;

    void Start()
    {
        originalCompassPos = transform.position;
        originalCompassRot = transform.rotation;

        planet = GameObject.FindGameObjectWithTag("Planet");
        originalPlanetPos = planet.transform.position;
        originalPlanetRot = planet.transform.rotation;

    }

    void Update()
    {
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = Camera.main.ScreenPointToRay(cursorPos);

        // touch
        if ((Input.touchCount == 1) && !(Input.touchCount == 2) || Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(cursorRay.origin, cursorRay.direction);
            if(Physics.Raycast(cursorRay, out hit))
                if (hit.collider.CompareTag("Compass"))
                    ResetRotation();
        }
    }

    private void LateUpdate()
    {
        transform.rotation = planet.transform.rotation;
        transform.Rotate(90, 0, 0);
    }

    public void ResetRotation()
    {
        // reset compass
        transform.position = originalCompassPos;
        transform.rotation = originalCompassRot;

        // reset planet
        planet.transform.position = originalPlanetPos;
        planet.transform.rotation = originalPlanetRot;
    }
}
