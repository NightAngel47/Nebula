using UnityEngine;
namespace FluidDynamics
{
    [AddComponentMenu("Fluid Dynamics/Obstacles/Fluid Polygon Obstacle")]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Fluid_Dynamics_Polygon_Obstacle : MonoBehaviour
    {
        states updateMode = states.LateUpdate;
        public Main_Fluid_Simulation m_fluid;
        public float distance = 10;
        private bool m_bInitialised = false;
        public bool m_isStatic = false;
        private PolygonCollider2D m_collider;
        private Collider m_tempCol;
        private Ray ray1;
        private RaycastHit h1;
        private Ray ray2;
        private RaycastHit h2;
        private Ray ray3;
        private RaycastHit h3;
        private Vector2[] points;

        private void Start()
        {
            m_tempCol = m_fluid.GetComponent<Collider>();
            m_collider = GetComponent<PolygonCollider2D>();
        }
        private void Update()
        {
            if (updateMode == states.Update)
                StartBlock();
        }
        private void FixedUpdate()
        {
            if (updateMode == states.FixedUpdate)
                StartBlock();
        }
        private void LateUpdate()
        {
            if (updateMode == states.LateUpdate)
                StartBlock();
        }
        private void StartBlock()
        {
            if (!m_bInitialised)
            {
                if (m_isStatic)
                {
                    Block(true);
                    Debug.Log("sdf");
                }
                m_bInitialised = true;
            }
            if (!m_isStatic)
            {
                Block(false);
            }
        }
        private void Block(bool bStatic)
        {
            if (m_collider && m_fluid)
            {
                points = m_collider.points;
                int size = points.Length;
                if (size >= 3)
                {
                    ray1 = new Ray(transform.TransformPoint(points[0]), Vector3.forward);
                    if (m_tempCol.Raycast(ray1, out h1, distance))
                    {
                        ray2 = new Ray(transform.TransformPoint(points[1]), Vector3.forward);
                        if (m_tempCol.Raycast(ray2, out h2, distance))
                        {
                            for (int i = 2; i < size; ++i)
                            {
                                ray3 = new Ray(transform.TransformPoint(points[i]), Vector3.forward);
                                if (m_tempCol.Raycast(ray3, out h3, distance))
                                {
                                    m_fluid.AddObstacleTriangle(h1.textureCoord, h2.textureCoord, h3.textureCoord, bStatic);
                                }
                                h2 = h3;
                            }
                        }
                    }
                }
            }
        }
    }
}