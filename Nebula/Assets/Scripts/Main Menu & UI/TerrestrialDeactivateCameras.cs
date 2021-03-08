using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrestrialDeactivateCameras : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Invoke(nameof(DeactivateProjection), 1f);
    }

    private void DeactivateProjection()
    {
        gameObject.SetActive(false);
    }
}
