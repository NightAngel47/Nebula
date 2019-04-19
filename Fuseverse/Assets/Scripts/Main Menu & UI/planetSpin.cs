using UnityEngine;

public class planetSpin : MonoBehaviour
{
    private float speed = 10;

    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * speed);
    }
}
