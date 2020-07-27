using System;
using System.Collections;
using System.Collections.Generic;
using FluidDynamics;
using UnityEngine;

public class StormsController : MonoBehaviour
{
    [SerializeField] private GameObject stormEmitter;
    
    public Main_Fluid_Simulation m_fluid;
    private Collider m_tempCol;
    private Ray ray;
    private RaycastHit hitInfo;
    private Vector3 m_mousePos;
    
    [SerializeField] private Camera fluidRTCam;
    private Camera _mainCamera;
    [SerializeField] private float wrapOffset = 0.4f;
    private GasMode _gasMode;
    private bool _placingStorm;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        m_tempCol = m_fluid.GetComponent<Collider>();
        _gasMode = FindObjectOfType<GasGiantFluidController>().gasMode;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _gasMode.CurrentInteractMode == GasMode.InteractMode.Storms && !_placingStorm)
        {
            StartCoroutine(PlaceStorm());
        }
    }

    /// <summary>
    /// Places a storm emitters and adds delay between placements.
    /// </summary>
    private IEnumerator PlaceStorm()
    {
        if (_placingStorm)
            yield break;
        SpawnStorm();
        yield return new WaitWhile(() => _placingStorm);
        
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// Spawns storm fluid dynamic emitters on fluid plane at mouse position.
    /// </summary>
    private void SpawnStorm()
    {
        _placingStorm = true;
        
        m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        ray = _mainCamera.ScreenPointToRay(m_mousePos);
        //Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
                
        Vector3 uvWorldPosition = Vector3.zero;
        if (!HitTestUVPosition(ray, ref uvWorldPosition)) return;
                
        Ray fluidRay = new Ray(fluidRTCam.transform.position + uvWorldPosition, Vector3.forward);
        //Debug.DrawRay(fluidRay.origin, fluidRay.direction, Color.green, 1f);
        
        if (!Physics.SphereCast(fluidRay.origin, 5f, fluidRay.direction, out var hit, 100, LayerMask.GetMask("Storms")))
        {
            if (m_tempCol.Raycast(fluidRay, out hitInfo, 100))
            {
                print("Placed storm");
                Instantiate(stormEmitter, hitInfo.point, Quaternion.identity, transform);
                Instantiate(stormEmitter, hitInfo.point + new Vector3(wrapOffset, 0, 0), Quaternion.identity, transform);
            }
        }
        else
        {
            print("Hit storm");
        }
        
        _placingStorm = false;
    }

    private bool HitTestUVPosition(Ray cursorRay, ref Vector3 uvWorldPosition)
    {
        if (Physics.Raycast(cursorRay, out var hit, 1000, LayerMask.GetMask("Default")))
        {
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            var orthographicSize = fluidRTCam.orthographicSize;
            uvWorldPosition.x = -(pixelUV.y * (orthographicSize * 2) - orthographicSize); //To center the UV on X
            uvWorldPosition.y = pixelUV.x * (orthographicSize * 2) - orthographicSize; //To center the UV on Y
            return true;
        }
        else
        {
            return false;
        }
    }
}
