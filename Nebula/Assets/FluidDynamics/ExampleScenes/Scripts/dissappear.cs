using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear : MonoBehaviour {
    bool start = false;
	// Use this for initialization
	void Start () {
       StartCoroutine(slsls());
	}
	IEnumerator slsls()
    {
        yield return new WaitForSeconds(3);
        //gameObject.SetActive(false);
        start = true;
    }
	// Update is called once per frame
	void Update () {
        if (start)
        {
            float posz;
            posz = Mathf.Lerp(transform.position.z, 0.4f, Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, posz);
        }
            //transform.position.z = 
	}
}
