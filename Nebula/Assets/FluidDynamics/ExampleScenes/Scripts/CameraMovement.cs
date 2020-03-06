using UnityEngine;
using FluidDynamics;
public class CameraMovement : MonoBehaviour {
    public Main_Fluid_Simulation sim;
    public KeyCode plpus;
    public KeyCode minus;
    public KeyCode plpusS;
    public KeyCode minusS;
    public float camplusspeed;
    public float speed;
    Vector3 prevpos;
    Vector3 mpos;
    Bounds _bounds;
    Bounds _cam_bounds;
    public Vector3 boundsSize;
    public Vector2 Camerabounds;
    Camera cam;
    Vector2 camSize;
    float defaultcamsize;
    // Use this for initialization
    void Start () {
        cam = GetComponent<Camera>();
        float size = cam.orthographicSize * 2;
        float width = size * (float)Screen.width / Screen.height;
        float height = size;
        camSize = new Vector2(width, height)/2;
        _cam_bounds.center = transform.position;
        _cam_bounds.size = Camerabounds;
        defaultcamsize = cam.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 0, defaultcamsize);
        if (Input.GetKey(plpus))
            cam.orthographicSize+= camplusspeed*Time.deltaTime;
        if (Input.GetKey(minus))
            cam.orthographicSize -= camplusspeed * Time.deltaTime;
        if (Input.GetKey(plpusS))
            sim.Vorticity += camplusspeed * Time.deltaTime;
        if (Input.GetKey(minusS))
            sim.Vorticity -= camplusspeed * Time.deltaTime;
        _bounds.size = boundsSize;
        _bounds.center = transform.position;

        mpos = Input.mousePosition;
        prevpos = new Vector3(Camera.main.ScreenToWorldPoint(mpos).x, Camera.main.ScreenToWorldPoint(mpos).y,transform.position.z);
        if (!_bounds.Contains(prevpos))
        {
            transform.position = Vector3.Lerp(transform.position, prevpos, speed*Time.deltaTime);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _cam_bounds.min.x + camSize.x, _cam_bounds.max.x - camSize.x), Mathf.Clamp(transform.position.y, _cam_bounds.min.y + camSize.y, _cam_bounds.max.y- camSize.y),transform.position.z);
	}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector2(boundsSize.x , boundsSize.y));
        Gizmos.DrawWireCube(transform.position, new Vector2(Camerabounds.x, Camerabounds.y));
    }
}
