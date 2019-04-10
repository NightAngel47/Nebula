using UnityEngine;

public class DecalCheck : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //print(other.name);
        if ((other.tag != gameObject.tag) && other.tag != "Terrain")
        {
            if (other.tag != "Planet")
            {
                Destroy(other.gameObject);
            }
        }
        Destroy(this);
    }
}