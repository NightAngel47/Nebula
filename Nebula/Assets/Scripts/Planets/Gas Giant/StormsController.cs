using System;
using System.Collections;
using System.Collections.Generic;
using FluidDynamics;
using UnityEngine;

public class StormsController : MonoBehaviour
{
    [SerializeField] private GameObject largeStormEmitter, smallStormEmitter;
    
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
    [SerializeField, Min(0.01f)] private float placementSpacing = 0.5f;

    private StormMode _stormMode;
    
    private List<GameObject> _placedStorms;

    private void Awake()
    {
        _stormMode = new StormMode();
        SwitchToSmallStormMode();
    }

    private void Start()
    {
        _mainCamera = Camera.main;
        m_tempCol = m_fluid.GetComponent<Collider>();
        _gasMode = FindObjectOfType<GasGiantFluidController>().gasMode;
        
        _placedStorms = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _gasMode.CurrentInteractMode == GasMode.InteractMode.Storms && !_placingStorm)
        {
            StartCoroutine(PerformStormAction());
        }
    }

    /// <summary>
    /// Handles the action of the player for the storms
    /// </summary>
    private IEnumerator PerformStormAction()
    {
        if (_placingStorm)
            yield break;
        CheckStormActionArea();
        yield return new WaitWhile(() => _placingStorm);
        
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// Checks the area of the player's action and then does the correct storm mode action
    /// </summary>
    private void CheckStormActionArea()
    {
        _placingStorm = true;
        
        m_mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        ray = _mainCamera.ScreenPointToRay(m_mousePos);
        //Debug.DrawRay(ray.origin, ray.direction, Color.magenta);
                
        Vector3 uvWorldPosition = Vector3.zero;
        if (HitTestUVPosition(ray, ref uvWorldPosition))
        {
            Ray fluidRay = new Ray(fluidRTCam.transform.position + uvWorldPosition, Vector3.forward);
            //Debug.DrawRay(fluidRay.origin, fluidRay.direction, Color.green, 1f);

            switch (_stormMode.CurrentInteractMode)
            {
                case StormMode.InteractMode.LargeStorm:
                    PlaceStorm(ref fluidRay, largeStormEmitter);
                    break;
                case StormMode.InteractMode.SmallStorm:
                    PlaceStorm(ref fluidRay, smallStormEmitter);
                    break;
                case StormMode.InteractMode.Erase:
                    EraseStorm(ref fluidRay);
                    break;
                case StormMode.InteractMode.None:
                    break;
                default:
                    print("Trying to access unimplemented StormMode.InteractMode");
                    break;
            }
        }
        
        _placingStorm = false;
    }

    /// <summary>
    /// Places storms at the position the player hit
    /// </summary>
    /// <param name="fluidRay">Ray use to find the position on the fluid plane</param>
    /// <param name="placementStorm">The storm prefab to place.</param>
    private void PlaceStorm(ref Ray fluidRay, GameObject placementStorm)
    {
        if (!Physics.SphereCast(fluidRay, placementSpacing, 100, LayerMask.GetMask("Storms")))
        {
            if (m_tempCol.Raycast(fluidRay, out hitInfo, 100))
            {
                _placedStorms.Add(Instantiate(placementStorm, hitInfo.point, Quaternion.identity, transform));
                _placedStorms.Add(Instantiate(placementStorm, hitInfo.point + new Vector3(wrapOffset, 0, 0), Quaternion.identity, transform));
            }
        }
    }
    
    /// <summary>
    /// Erases storms at the position the player hit
    /// </summary>
    /// <param name="fluidRay">Ray use to find the position on the fluid plane</param>
    private void EraseStorm(ref Ray fluidRay)
    {
        if (Physics.SphereCast(fluidRay, placementSpacing, out var hit, 100, LayerMask.GetMask("Storms")))
        {
            GameObject stormToDestroy = hit.transform.gameObject;
            GameObject otherStormToDestroy = _placedStorms[_placedStorms.IndexOf(stormToDestroy) + 1];

            _placedStorms.Remove(stormToDestroy);
            _placedStorms.Remove(otherStormToDestroy);
            
            Destroy(stormToDestroy);
            Destroy(otherStormToDestroy);
        }
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

    public void SwitchToLargeStormMode()
    {
        _stormMode.ChangeStormMode(StormMode.InteractMode.LargeStorm);
    }

    public void SwitchToSmallStormMode()
    {
        _stormMode.ChangeStormMode(StormMode.InteractMode.SmallStorm);
    }
    
    public void SwitchToStormEraseMode()
    {
        _stormMode.ChangeStormMode(StormMode.InteractMode.Erase);
    }
    
    public void SwitchToStormNoneMode()
    {
        _stormMode.ChangeStormMode(StormMode.InteractMode.None);
    }
}
