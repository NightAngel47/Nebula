using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public GameObject decalBradley;
    public bool toolSelected = false;
    public bool canDecal = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && canDecal && toolSelected)
        {
            HandleInput();
        }
    }

    void HandleInput()
    {
        canDecal = false;


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f))
        {
            Instantiate(decalBradley, hitInfo.point, Quaternion.FromToRotation(Vector3.up, hitInfo.normal));
        }

        Invoke("ResetCanDecal", 0.1f);
    }

    void ResetCanDecal()
    {
        canDecal = true;
    }

    public void ToolSelected()
    {
        toolSelected = !toolSelected;
    }
}
